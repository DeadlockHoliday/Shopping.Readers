using SoftCircuits.HtmlMonkey;
using Shopping.Readers.MT.Data;

using Shopping.Readers.MT.Helpers;
using Shopping.Readers.Common.Supplies;

namespace Shopping.Readers.MT.Html;

internal static class ProductReader
{
    public static ISupplyPackagePosition[] Read(HtmlElementNode orderNode)
        => orderNode.Children
            .Find(".history-order-good")
            .Select(x =>
            {
                return new SupplyPosition
                {
                    Product = new UnprocessedProduct()
                    {
                        CategoryName = x.Children.GetText(".main-name"),
                        Name = x.Children.GetText(".main-text > p"),
                    },
                    Url = x.Children.GetHrefValue(".main-link"),
                    Price = x.Children.GetText(".main-price .price-num")?.AsSum()?.AsMoney() ?? new NMoneys.Money(0),
                    Quantity = x.Children.GetText(".main-quantity > .item-count").AsLong() ?? 0,
                };
            })
            .DistinctBy(x => x.Product.Name)
            .Cast<ISupplyPackagePosition>()
            .ToArray();
}