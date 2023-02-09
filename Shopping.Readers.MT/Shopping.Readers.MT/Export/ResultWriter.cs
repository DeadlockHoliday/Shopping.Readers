using CsvHelper;
using CsvHelper.Configuration;
using Shopping.Readers.Common.Contracts;
using Shopping.Readers.MT.Data;
using System.Globalization;

namespace Shopping.Readers.MT.Export;

internal class ResultWriter
{
    private static readonly CsvConfiguration csvConfig = new(CultureInfo.InvariantCulture)
    {
        PrepareHeaderForMatch = args => args.Header.ToLower(),
    };

    internal static void Write(IOrder[] orders, string writeFolder)
    {
        Parallel.ForEach(orders, (order, i) 
            => Write(writeFolder, order));
    }

    private static void Write(string writeFolder, IOrder order)
    {
        var fileName = order.ToCsvFileName();
        var writePath = Path.Combine(writeFolder, fileName);
        using var writer = new StreamWriter(writePath);
        using var csvWriter = new CsvWriter(writer, csvConfig);

        var items = order.Positions.Cast<OrderPosition>();
        csvWriter.WriteRecords(items);
    }
}