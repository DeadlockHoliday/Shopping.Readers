using SoftCircuits.HtmlMonkey;
using OrderReader.Data;

namespace OrderReader.Html;

internal class OrderParser
{
    public static Product[] Parse(HtmlDocument doc)
        => doc.Find(".history-order")
            .Select(ParseProducts)
            .SelectMany(x => x)
            .ToArray();

    internal static Product[] Parse(string html)
        => Parse(HtmlDocument.FromHtml(html));

    private static Product[] ParseProducts(HtmlElementNode orderNode)
    {
        var date = ParseDate(orderNode);
        return ProductParser.Parse(orderNode)
            .Select(x => x with { OrderDate = date })
            .ToArray();
    }

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