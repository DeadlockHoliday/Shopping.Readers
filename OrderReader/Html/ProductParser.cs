using SoftCircuits.HtmlMonkey;
using OrderReader.Data;

namespace OrderReader.Html;
internal static class ProductParser
{
    public static Product[] Parse(HtmlElementNode orderNode)
        => orderNode.Children
            .Find(".history-order-good")
            .Select(x =>
            {
                var prices = x.Children.GetPrices();
                return new Product
                {
                    CategoryName = x.Children.GetText(".main-name").Trim(),
                    FullName = x.Children.GetText(".main-text > p").Trim(),
                    Url = x.Children.GetAttr(".main-link", "href"),
                    UnitPrice = prices[0],
                    TotalPrice = prices[1]
                };
            })
            .ToArray();

    internal static Product[] Parse(string html)
    {
        var nodeCollection = HtmlDocument.FromHtml(html).RootNodes;
        var parentNode = new HtmlElementNode("div", new HtmlAttributeCollection(), nodeCollection);
        return Parse(parentNode);
    }

    private static string GetAttr(
        this IEnumerable<HtmlNode> children, 
        string selector,
        string name)
        => children.Find(selector).First().Attributes[name].Value;

    private static string GetText(this IEnumerable<HtmlNode> children, string selector)
        => children.Find(selector).First().Text;

    private static decimal[] GetPrices(this IEnumerable<HtmlNode> children)
        => children.Find(".price-num")
                .Select(x => x.Text.Trim()
                    .Split(' ')
                    .First())
                .Select(x => decimal.Parse(x))
                .ToArray();
}