using CsvHelper;
using CsvHelper.Configuration;
using Shopping.Readers.Common.Data;
using Shopping.Readers.Common.Data.Products;
using System.Globalization;

namespace Shopping.Readers.MT.Export;

internal class ResultWriter
{
    private static readonly CsvConfiguration csvConfig = new(CultureInfo.InvariantCulture)
    {
        PrepareHeaderForMatch = args => args.Header.ToLower(),
    };

    internal static void Write(SupplyPackagePosition<UnprocessedProduct>[] positions, string writeFolder)
    {
        var first = positions.First();
        var fileName = first.ToCsvFileName();
        var writePath = Path.Combine(writeFolder, fileName);
        using var writer = new StreamWriter(writePath);
        using var csvWriter = new CsvWriter(writer, csvConfig);

        csvWriter.WriteRecords(positions);
    }
}