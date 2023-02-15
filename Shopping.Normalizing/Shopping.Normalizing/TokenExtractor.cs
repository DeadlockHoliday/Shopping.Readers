using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace Shopping.Normalizing;

internal static class TokenExtractor
{
    internal record struct MeasureToken(ulong Number, string Measure);

    private static RegexOptions regexOptions = RegexOptions.IgnoreCase 
        | RegexOptions.CultureInvariant 
        | RegexOptions.Compiled 
        | RegexOptions.Multiline
        | RegexOptions.IgnorePatternWhitespace;

    private static readonly Regex regex = new(@"(\d+[\.|,]?\d?) \s? ([a-zA-Zа-яА-Я]+)", regexOptions);

    internal static IReadOnlyCollection<MeasureToken> ExtractTokens(string line)
        => regex.Matches(line)
            .Select(x =>
            {
                var number = decimal.Parse(x.Groups[1].ValueSpan);
                var measure = x.Groups[2].Value;
                return new MeasureToken((ulong)number, measure);
            })
            .ToImmutableArray();
}