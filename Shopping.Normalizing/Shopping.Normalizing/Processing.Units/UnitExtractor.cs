using System.Collections.Immutable;
using System.Globalization;
using System.Text.RegularExpressions;
using Shopping.Normalizing.Data;
using UnitsNet;
using UnitsNet.Units;

namespace Shopping.Normalizing.Processing.Units;

internal static class UnitExtractor
{
    private static readonly RegexOptions regexOptions = RegexOptions.IgnoreCase
        | RegexOptions.CultureInvariant
        | RegexOptions.Compiled
        | RegexOptions.Multiline
        | RegexOptions.IgnorePatternWhitespace;

    /// <summary>
    /// A regex for unit extraction.
    /// </summary>
    /// <remarks>
    /// 123 g
    /// 12.3kg
    /// </remarks>
    private static readonly Regex regex = new(@"(\d+(?:[.\,]\d+)?) \s? ([a-zA-Zа-яА-Я]+)\b", regexOptions);

    internal static IReadOnlyCollection<Unit> Extract(string line)
        => regex.Matches(line)
            .Select(x =>
            {
                var numberStr = x.Groups[1].Value.Replace(',', '.');
                var number = decimal.Parse(numberStr, CultureInfo.InvariantCulture);
                var measure = x.Groups[2].Value;
                return new Unit(number, measure);
            })
            .ToImmutableArray();
}