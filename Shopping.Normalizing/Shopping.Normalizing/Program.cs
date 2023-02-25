using CsvHelper;
using CsvHelper.Configuration;
using Shopping.Normalizing;
using Shopping.Readers.Common.Data;
using Shopping.Readers.Common.Data.Products;
using System.Text.RegularExpressions;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Shopping.Normalizing.Tests")]

#if DEBUG
args = new string[]
{
    @"C:\Users\IT\Documents\#Coding\Pets\Shopping.Samples\Shopping.Samples.Readers.Input",
    @".*[0-9]+?\.csv",
};
#endif

var inputFolder = args[0];
var targetFilesSelector = new Regex(args[1], RegexOptions.Compiled);
var culture = System.Globalization.CultureInfo.InvariantCulture;

Directory.GetFiles(inputFolder)
    .Where(x => targetFilesSelector.IsMatch(x))
    .AsParallel()
    .ForAll(f =>
    {
        var positions = LoadPositions(f);
        var newPositions = NormalizePositions(positions);
        WritePositions(f, newPositions);
    });

void WritePositions(string file, SupplyPackagePosition<ProcessedProduct>[] positions)
{
    var newFile = new FileInfo(file + ".processed.csv");
    if (newFile.Exists)
    {
        newFile.Delete();
    }

    using var streamWriter = new StreamWriter(newFile.OpenWrite());
    using var csvWriter = new CsvWriter(streamWriter, culture);
    csvWriter.WriteRecords(positions);
}

SupplyPackagePosition<UnprocessedProduct>[] LoadPositions(string file)
{
    using var streamReader = new StreamReader(file);
    using var csvReader = new CsvReader(streamReader, new CsvConfiguration(culture)
    {
        PrepareHeaderForMatch = args => args.Header.ToLower()
    });

    // MissingMethodException: Constructor 'Shopping.Readers.Common.Products.IProduct()' was not found.
    return csvReader.GetRecords<SupplyPackagePosition<UnprocessedProduct>>().ToArray();
}

SupplyPackagePosition<ProcessedProduct>[] NormalizePositions(SupplyPackagePosition<UnprocessedProduct>[] positions)
    => positions.Select(position => new SupplyPackagePosition<ProcessedProduct>()
    {
        Price = position.Price,
        Product = SupplyNormalizer.NormalizeLine(position.Product.Info, position.Product.CategoryName),
        Quantity = position.Quantity
    }).ToArray();
