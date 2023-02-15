using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Shopping.Normalizing.Data;

namespace Shopping.Normalizing.Processing;

internal static class UnitExtractor
{
    private static RegexOptions regexOptions = RegexOptions.IgnoreCase
        | RegexOptions.CultureInvariant
        | RegexOptions.Compiled
        | RegexOptions.Multiline
        | RegexOptions.IgnorePatternWhitespace;

    private static readonly Regex regex = new(@"(\d+[\.|,]?\d+) \s? ([a-zA-Zа-яА-Я]+)", regexOptions);

    internal static IReadOnlyCollection<Unit> Extract(string line)
        => regex.Matches(line)
            .Select(x =>
            {
                var number = decimal.Parse(x.Groups[1].ValueSpan);
                var measure = x.Groups[2].Value;
                return new Unit(number, measure);
            })
            .ToImmutableArray();
}