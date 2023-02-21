using SoftCircuits.HtmlMonkey;
using Shopping.Readers.MT.Data;
using Shopping.Readers.Common.Contracts;
using Shopping.Readers.MT.Helpers;

namespace Shopping.Readers.MT.Html;

internal static class ProductReader
{
    public static ISupplyPosition[] Read(HtmlElementNode orderNode)
        => orderNode.Children
            .Find(".history-order-good")
            .Select(x =>
            {
                return new SupplyPosition
                {
                    Product = new Product()
                    {
                        CategoryName = x.Children.GetText(".main-name"),
                        Info = x.Children.GetText(".main-text > p"),
                    },
                    Url = x.Children.GetHrefValue(".main-link"),
                    Price = x.Children.GetText(".main-price .price-num")?.AsSum()?.AsMoney() ?? new NMoneys.Money(0),
                    Quantity = x.Children.GetText(".main-quantity > .item-count").AsLong() ?? 0,
                };
            })
            .DistinctBy(x => x.Product.Info)
            .Cast<ISupplyPosition>()
            .ToArray();
}