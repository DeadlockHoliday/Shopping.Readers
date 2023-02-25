using SoftCircuits.HtmlMonkey;
using System.Collections.Immutable;
using Shopping.Readers.Common.Data;
using Shopping.Readers.Common.Data.Products;

namespace Shopping.Readers.MT.Html;

internal class SupplyReader
{
    public static SupplyPackagePosition<UnprocessedProduct>[] Parse(HtmlDocument doc)
        => doc.Find(".history-order")
            .Select(x => new
            {
                x,
                date = ParseDate(x) ?? DateOnly.MinValue,
            })
            .SelectMany(x => ProductReader.Read(x.x, x.date))
            .ToArray();

    internal static SupplyPackagePosition<UnprocessedProduct>[] Parse(string html)
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