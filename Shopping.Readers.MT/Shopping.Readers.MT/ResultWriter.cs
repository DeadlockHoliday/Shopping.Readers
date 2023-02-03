using CsvHelper;
using CsvHelper.Configuration;
using Shopping.Readers.MT.Data;
using System.Globalization;

namespace Shopping.Readers.MT;

internal static class ResultWriter
{
    internal static void WriteResult(IEnumerable<Product> items, string writePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower(),
        };

        using var writer = new StreamWriter(writePath);
        using var csvWriter = new CsvWriter(writer, config);
        csvWriter.WriteRecords(items);
    }
}
