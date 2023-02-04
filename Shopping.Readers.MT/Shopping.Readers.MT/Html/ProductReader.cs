using SoftCircuits.HtmlMonkey;
using Shopping.Readers.MT.Data;

namespace Shopping.Readers.MT.Html;

internal static class ProductReader
{
    public static Product[] Read(HtmlElementNode orderNode)
        => orderNode.Children
            .Find(".history-order-good")
            .Select(x =>
            {
                var prices = x.Children.GetPrices();
                return new Product
                {
                    CategoryName = x.Children.GetText(".main-name"),
                    FullName = x.Children.GetText(".main-text > p"),
                    Url = x.Children.GetHrefValue(".main-link"),
                    UnitPrice = prices[0],
                    TotalPrice = prices[1]
                };
            })
            .DistinctBy(x => x.FullName)
            .ToArray();

    internal static Product[] Read(string html)
    {
        // TODO: replace this method by mock.
        var rootNodes = HtmlDocument.FromHtml(html).RootNodes;
        var parentRootNode = new HtmlElementNode("div", new HtmlAttributeCollection(), rootNodes);
        return Read(parentRootNode);
    }
}