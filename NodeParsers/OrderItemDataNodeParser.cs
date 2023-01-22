using SoftCircuits.HtmlMonkey;

internal static class OrderItemDataNodeParser
{
    public static OrderItem ParseOrderItem(HtmlElementNode node, DateOnly? date, decimal sum)
    {
        var children = node.Children
                .Where(x => !string.IsNullOrWhiteSpace(x.Text))
                .ToArray();

        return new()
        {
            CategoryName = GetText(children, ".main-name"),
            ProductName = GetText(children, ".main-text > p"),
            OrderQuantity = GetDecimal(children, ".item-count"),
            UnitPrice = GetPrices(children)[0],
            TotalPrice = GetPrices(children)[1],
            OrderDate = date ?? DateOnly.MinValue,
            OrderSum = sum
        };
    }

    private static decimal GetDecimal(HtmlNode[] children, string selector)
        => decimal.Parse(GetText(children, selector));

    private static string GetText(HtmlNode[] children, string selector)
        => children.Find(selector).First().Text;

    private static decimal[] GetPrices(HtmlNode[] children)
        => children.Find(".price-num")
                .Select(x => x.Text.Trim()
                    .Split(' ')
                    .First())
                .Select(x => decimal.Parse(x))
                .ToArray();
}