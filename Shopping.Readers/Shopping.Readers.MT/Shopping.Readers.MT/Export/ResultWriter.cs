using CsvHelper;
using CsvHelper.Configuration;
using Shopping.Common.Data.Supply;

namespace Shopping.Readers.MT.Export;

internal class ResultWriter
{
    private static readonly CsvConfiguration csvConfig = new(Config.CultureInfo)
    {
        PrepareHeaderForMatch = args => args.Header.ToLower(),
    };

    internal static void Write(string vendor, SupplyPosition[] positions, string writeFolder)
    {
        var dateStamp = DateOnly
            .FromDateTime(DateTime.Now)
            .ToStamp();

        var filename = $"{dateStamp}.{vendor}.csv";
        var fileInfo = new FileInfo(Path.Combine(writeFolder, filename));
        if (fileInfo.Exists)
        {
            fileInfo.Delete();
        }

        using var writer = new StreamWriter(fileInfo.OpenWrite());
        using var csvWriter = new CsvWriter(writer, csvConfig);

        csvWriter.WriteRecords(positions);
    }
}