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
                return new OrderPosition
                {
                    CategoryName = x.Children.GetText(".main-name"),
                    Info = x.Children.GetText(".main-text > p"),
                    Url = x.Children.GetHrefValue(".main-link"),
                    Price = x.Children.GetText(".main-price .price-num").AsSum() ?? 0,
                    Quantity = x.Children.GetText(".main-quantity > .item-count").AsDecimal() ?? 0,
                    TotalPrice = x.Children.GetText(".main-summa .price-num").AsSum() ?? 0
                };
            })
            .DistinctBy(x => x.Info)
            .Cast<IOrderPosition>()
            .ToArray();
}