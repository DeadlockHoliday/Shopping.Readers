using SoftCircuits.HtmlMonkey;

internal class OrderDataNodeParser
{
    internal static OrderItem[] ReadOrderItems(string filePath)
        => HtmlDocument.FromFile(filePath)
            .Find(".history-order")
            .Select(ParseOrderItems)
            .SelectMany(x => x)
            .ToArray();

    private static OrderItem[] ParseOrderItems(HtmlElementNode orderNode)
    {
        var date = ParseDate(orderNode);
        return OrderItemDataNodeParser.ParseOrderItems(orderNode)
            // .Select(x => x with { OrderDate = date })
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