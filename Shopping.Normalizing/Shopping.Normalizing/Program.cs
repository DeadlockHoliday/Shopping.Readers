using CsvHelper;
using CsvHelper.Configuration;
using Shopping.Normalizing;
using Shopping.Readers.Common.Data;
using Shopping.Readers.Common.Static;
using System.Text.RegularExpressions;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Shopping.Normalizing.Tests")]

#if DEBUG
args = new string[]
{
    Environment.CurrentDirectory,
    @"MT.Sample.csv",
};
#endif

var inputFolder = args[0];
var targetFilesSelector = new Regex(args[1], RegexOptions.Compiled);

Directory.GetFiles(inputFolder)
    .Where(x => targetFilesSelector.IsMatch(x))
    .AsParallel()
    .ForAll(f =>
    {
        var positions = LoadPositions(f);
        var newPositions = NormalizePositions(positions);
        WritePositions(f, newPositions);
    });

void WritePositions(string file, SupplyPackagePosition[] positions)
{
    var newFile = new FileInfo(file + ".processed.csv");
    if (newFile.Exists)
    {
        newFile.Delete();
    }

    using var streamWriter = new StreamWriter(newFile.OpenWrite());
    using var csvWriter = new CsvWriter(streamWriter, Config.CultureInfo);
    csvWriter.WriteRecords(positions);
}

UnprocessedSupplyPackagePosition[] LoadPositions(string file)
{
    using var streamReader = new StreamReader(file);
    using var csvReader = new CsvReader(streamReader, new CsvConfiguration(Config.CultureInfo)
    {
        PrepareHeaderForMatch = args => args.Header.ToLower()
    });

    // MissingMethodException: Constructor 'Shopping.Readers.Common.Products.IProduct()' was not found.
    return csvReader.GetRecords<UnprocessedSupplyPackagePosition>().ToArray();
}

SupplyPackagePosition[] NormalizePositions(UnprocessedSupplyPackagePosition[] positions)
    => positions.Select(position => new SupplyPackagePosition()
    {
        Price = position.Price,
        Product = ProductProcessor.Process(position.Product.Info, position.Product.CategoryName),
        Quantity = position.Quantity
    }).ToArray();
