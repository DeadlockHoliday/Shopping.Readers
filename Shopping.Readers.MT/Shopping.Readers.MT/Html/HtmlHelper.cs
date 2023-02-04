﻿using SoftCircuits.HtmlMonkey;

namespace Shopping.Readers.MT.Html;

internal static class HtmlHelper
{
    internal static string GetHrefValue(
        this IEnumerable<HtmlNode> children,
        string selector)
            => children.GetAttributeValue(selector, "href");

    internal static string GetAttributeValue(
        this IEnumerable<HtmlNode> children,
        string selector,
        string name)
            => children.Find(selector).First().Attributes[name]?.Value
                ?? string.Empty;

    internal static string GetText(this IEnumerable<HtmlNode> children, string selector)
        => children.Find(selector)
            .First()
            .Text
            .Trim();

    internal static decimal[] GetPrices(this IEnumerable<HtmlNode> children)
        => children.Find(".price-num")
                .Select(x => x.Text.Trim()
                    .Split(' ')
                    .First())
                .Select(x => decimal.Parse(x))
                .ToArray();
}