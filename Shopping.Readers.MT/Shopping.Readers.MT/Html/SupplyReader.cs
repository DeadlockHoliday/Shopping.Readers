using SoftCircuits.HtmlMonkey;
using Shopping.Readers.MT.Data;
using Shopping.Readers.Common.Contracts;
using System.Collections.Immutable;

namespace Shopping.Readers.MT.Html;

internal class SupplyReader
{
    public static ISupply[] Parse(HtmlDocument doc)
        => doc.Find(".history-order")
            .Select(x => new Supply()
                {
                    Positions = ProductReader.Read(x).ToImmutableArray(),
                    Date = ParseDate(x) ?? DateOnly.MinValue,
                })
            .Cast<ISupply>()
            .ToArray();

    internal static ISupply[] Parse(string html)
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