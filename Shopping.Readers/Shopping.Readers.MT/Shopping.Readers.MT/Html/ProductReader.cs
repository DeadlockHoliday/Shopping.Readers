using SoftCircuits.HtmlMonkey;
using Shopping.Common.Helpers;
using Shopping.Readers.MT.Helpers;
using Shopping.Common.Data.Supply;
using System.Linq;
using Shopping.Common.Data;

namespace Shopping.Readers.MT.Html;

internal static class ProductReader
{
    public static SupplyPosition[] Read(HtmlElementNode orderNode, DateOnly date)
        => orderNode.Children
            .Find(".history-order-good")
            .Select(x =>
            {
                return new SupplyPosition
                {
                    Product = new Product
                    {
                        Info = x.Children.GetText(".main-text > p"), 
                        Category = x.Children.GetText(".main-name"),
                    },
                    Invoice = new()
                    {
                        Price = x.Children.GetText(".main-price .price-num").ToSum().ToMoney(),
                        Quantity = x.Children.GetText(".main-quantity > .item-count").ToDecimal().ToInt64(),
                        Date = date
                    }
                };
            })
            .ToArray();
}