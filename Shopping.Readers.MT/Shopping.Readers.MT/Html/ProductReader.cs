using SoftCircuits.HtmlMonkey;
using Shopping.Readers.MT.Helpers;
using Shopping.Readers.Common.Data.Products;
using Shopping.Readers.Common.Data;

namespace Shopping.Readers.MT.Html;

internal static class ProductReader
{
    public static SupplyPackagePosition<UnprocessedProduct>[] Read(HtmlElementNode orderNode, DateOnly date)
        => orderNode.Children
            .Find(".history-order-good")
            .Select(x =>
            {
                return new SupplyPackagePosition<UnprocessedProduct>
                {
                    Product = new()
                    {
                        CategoryName = x.Children.GetText(".main-name"),
                        Info = x.Children.GetText(".main-text > p"),
                    },
                    //Url = x.Children.GetHrefValue(".main-link"),
                    Price = x.Children.GetText(".main-price .price-num")?.AsSum()?.AsMoney() ?? new NMoneys.Money(0),
                    Quantity = x.Children.GetText(".main-quantity > .item-count").AsLong() ?? 0,
                    Date = date
                };
            })
            .ToArray();
}