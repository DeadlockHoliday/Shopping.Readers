using CsvHelper;
using CsvHelper.Configuration;
using Shopping.Readers.Common.Contracts;
using System.Globalization;

namespace Shopping.Readers.MT.Export;

internal class ResultWriter
{
    private readonly CsvConfiguration csvConfig;
    private readonly string writeFolder;

    public ResultWriter(string writeFolder)
    {
        this.writeFolder = writeFolder;
        csvConfig = new(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower(),
        };
    }

    internal static void Write(IOrder[] orders, string writeFolder)
    {
        Parallel.ForEach(orders, (order, i) 
            => new ResultWriter(writeFolder).Write(order));
    }

    private void Write(IOrder order)
    {
        var fileName = order.ToCsvFileName();
        var writePath = Path.Combine(writeFolder, fileName);
        using var writer = new StreamWriter(writePath);
        using var csvWriter = new CsvWriter(writer, csvConfig);

        csvWriter.WriteRecords(order.Positions);
    }
}