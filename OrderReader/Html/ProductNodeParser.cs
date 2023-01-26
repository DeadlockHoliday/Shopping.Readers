using SoftCircuits.HtmlMonkey;

internal static class ProductNodeParser
{
    public static Product[] Parse(HtmlElementNode orderNode)
        => orderNode.Children
            .Find(".history-order-good")
            .Select(x =>
            {
                var children = x.Children
                    .Where(x => !string.IsNullOrWhiteSpace(x.Text))
                    .ToArray();

                var prices = children.GetPrices();
                return new Product
                {
                    CategoryName = children.GetText(".main-name").Trim(),
                    FullName = children.GetText(".main-text > p").Trim(),
                    Url = children.GetAttr(".main-link", "href"),
                    UnitPrice = prices[0],
                    TotalPrice = prices[1]
                };
            })
            .ToArray();

    internal static Product[] Parse(string html)
    {
        // TODO: decide, is this method needs to be here just for tests?
        // or such method should be in tests?
        var nodeCollection = HtmlDocument.FromHtml(html).RootNodes;
        var parentNode = new HtmlElementNode("div", new HtmlAttributeCollection(), nodeCollection);
        return Parse(parentNode);
    }

    private static string GetAttr(this HtmlNode[] children, string selector, string name)
        => children.Find(selector).First().Attributes[name].Value;

    private static string GetText(this HtmlNode[] children, string selector)
        => children.Find(selector).First().Text;

    private static decimal[] GetPrices(this HtmlNode[] children)
        => children.Find(".price-num")
                .Select(x => x.Text.Trim()
                    .Split(' ')
                    .First())
                .Select(x => decimal.Parse(x))
                .ToArray();
}