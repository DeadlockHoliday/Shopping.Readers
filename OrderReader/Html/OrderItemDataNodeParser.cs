﻿using SoftCircuits.HtmlMonkey;

internal static class OrderItemDataNodeParser
{
    public static OrderItem[] ParseOrderItems(HtmlElementNode orderNode)
        => orderNode.Children
            .Find(".history-order-good")
            .Select(x =>
            {
                var children = x.Children
                    .Where(x => !string.IsNullOrWhiteSpace(x.Text))
                    .ToArray();

                var prices = children.GetPrices();
                return new OrderItem
                {
                    CategoryName = children.GetText(".main-name").Trim(),
                    ProductFullName = children.GetText(".main-text > p").Trim(),
                    UnitPrice = prices[0],
                    TotalPrice = prices[1]
                };
            })
            .ToArray();

    internal static OrderItem[] ParseOrderItems(string html)
    {
        // TODO: decide, is this method needs to be here just for tests?
        // or such method should be in tests?
        var nodeCollection = HtmlDocument.FromHtml(html).RootNodes;
        var parentNode = new HtmlElementNode("div", new HtmlAttributeCollection(), nodeCollection);
        return ParseOrderItems(parentNode);
    }

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