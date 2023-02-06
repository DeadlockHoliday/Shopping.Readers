using SoftCircuits.HtmlMonkey;
using Shopping.Readers.MT.Data;
using Shopping.Readers.Common.Contracts;
using System.Collections.Immutable;

namespace Shopping.Readers.MT.Html;

internal class OrderReader
{
    public static IOrder[] Parse(HtmlDocument doc)
        => doc.Find(".history-order")
            .Select(x => new Order()
                {
                    Positions = ProductReader.Read(x).ToImmutableArray(),
                    Date = ParseDate(x) ?? DateOnly.MinValue,
                    Id = ParseId(x)
                })
            .Cast<IOrder>()
            .ToArray();

    internal static IOrder[] Parse(string html)
        => Parse(HtmlDocument.FromHtml(html));

    private static DateOnly? ParseDate(HtmlElementNode node)
    {
        const int dateElementHtmlIndex = 1;
        var dateEntry = GetOrderDataEntry(node, dateElementHtmlIndex);
        if (string.IsNullOrWhiteSpace(dateEntry))
        {
            return null;
        }

        return DateOnly.ParseExact(dateEntry, "dd-MM-yyyy");
    }

    private static int ParseId(HtmlElementNode node)
    {
        const int dateElementHtmlIndex = 0;
        var idEntry = GetOrderDataEntry(node, dateElementHtmlIndex);

        if (string.IsNullOrWhiteSpace(idEntry))
        {
            return 0;
        }

        return int.Parse(idEntry);
    }

    private static string? GetOrderDataEntry(HtmlElementNode node, int index)
    {
        var orderData = ParseOrderData(node);
        var entry = orderData?.Skip(index).FirstOrDefault();
        return entry?.Split(' ').FirstOrDefault();
    }

    private static string[] ParseOrderData(HtmlElementNode node)
    {
        var orderDataRow = node.Children
            .Find(".order-data")
            .First();

        return orderDataRow.Children
            .Find("div")
            .Select(x => x.Text.Trim())
            .ToArray();
    }
}