using SoftCircuits.HtmlMonkey;
using Shopping.Readers.Common.Data;
using Shopping.Readers.Common.Helpers;
using Shopping.Readers.MT.Helpers;

namespace Shopping.Readers.MT.Html;

internal static class ProductReader
{
    public static UnprocessedSupplyPackagePosition[] Read(HtmlElementNode orderNode, DateOnly date)
        => orderNode.Children
            .Find(".history-order-good")
            .Select(x =>
            {
                return new UnprocessedSupplyPackagePosition
                {
                    Product = new()
                    {
                        CategoryName = x.Children.GetText(".main-name"),
                        Info = x.Children.GetText(".main-text > p"),
                    },
                    Price = x.Children.GetText(".main-price .price-num").ToSum().ToMoney(),
                    Quantity = x.Children.GetText(".main-quantity > .item-count").ToDecimal().ToInt64(),
                    Date = date
                };
            })
            .ToArray();
}