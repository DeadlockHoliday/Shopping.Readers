using SoftCircuits.HtmlMonkey;
using Shopping.Readers.MT.Data;
using Shopping.Readers.Common.Contracts;
using Shopping.Readers.MT.Helpers;

namespace Shopping.Readers.MT.Html;

internal static class ProductReader
{
    public static IOrderPosition[] Read(HtmlElementNode orderNode)
        => orderNode.Children
            .Find(".history-order-good")
            .Select(x =>
            {
                var prices = x.Children.GetPrices();
                return new OrderPosition
                {
                    CategoryName = x.Children.GetText(".main-name"),
                    Info = x.Children.GetText(".main-text > p"),
                    Url = x.Children.GetHrefValue(".main-link"),
                    Price = prices[0],
                    Quantity = x.Children.GetText(".main-quantity > .item-count").AsDecimal() ?? 0,
                };
            })
            .DistinctBy(x => x.Info)
            .Cast<IOrderPosition>()
            .ToArray();

    internal static IOrderPosition[] Read(string html)
    {
        // TODO: replace this method by mock.
        var rootNodes = HtmlDocument.FromHtml(html).RootNodes;
        var parentRootNode = new HtmlElementNode("div", new HtmlAttributeCollection(), rootNodes);
        return Read(parentRootNode);
    }
}