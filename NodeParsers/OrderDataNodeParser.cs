using SoftCircuits.HtmlMonkey;

internal static class OrderDataNodeParser
{
    internal static DateOnly? ParseDate(HtmlElementNode node)
    {
        var dateElementHtmlIndex = 1;
        var entry = GetOrderDataEntry(node, dateElementHtmlIndex);
        if (string.IsNullOrWhiteSpace(entry))
        {
            return null;
        }

        return DateOnly.ParseExact(entry, "dd-mm-yyyy");
    }

    internal static decimal ParseSum(HtmlElementNode node)
    {
        var sumElementHtmlIndex = 2;
        var entry = GetOrderDataEntry(node, sumElementHtmlIndex);
        if (decimal.TryParse(entry, out var result))
        {
            return result;
        }
        else
        {
            return 0;
        }
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