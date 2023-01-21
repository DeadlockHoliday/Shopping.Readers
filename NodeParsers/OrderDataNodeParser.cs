using SoftCircuits.HtmlMonkey;

internal static class OrderDataNodeParser
{
    internal static DateOnly ParseDate(HtmlElementNode node)
    {
        var orderData = ParseOrderData(node);
        return DateOnly.ParseExact(orderData[1].Split(' ').First(), "dd-mm-yyyy");
    }

    internal static decimal ParseSum(HtmlElementNode node)
    {
        var orderData = ParseOrderData(node);
        return decimal.Parse(orderData[2].Split(' ').First());
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