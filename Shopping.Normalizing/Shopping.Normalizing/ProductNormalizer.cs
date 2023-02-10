using Shopping.Normalizing.Data;
using System.Text.RegularExpressions;

namespace Shopping.Normalizing;

internal class ProductNormalizer
{
    internal static SupplyItem ParseSupplyItem(string line)
    {
        throw new Exception();
    }

    internal static ulong ExtractMassGramms(string line)
    {
        var matches = new Regex("(\\S+)+?")
            .Split(line)
            .Where(token => !string.IsNullOrWhiteSpace(token))
            .ToList();

        var pieces = 0UL;
        var gramms = 0UL;

        matches.Prepend(string.Empty);
        for (int i = matches.Count - 1; i > 0; i--)
        {
            // 1 
            var token = matches[i].TrimEnd('.');
            if (token.EndsWith("шт"))
            {
                pieces = ExtractNumber(matches[i], matches[i - 1]);
                continue;
            }

            // 2
            var volumes = new[] { 'г', 'л' };
            var iVolume = 0;
            for (iVolume = 0; iVolume < volumes.Length; iVolume++)
            {
                if (token.EndsWith(volumes[iVolume]))
                {
                    break;
                }
            }

            if (iVolume < volumes.Length) 
            {
                gramms = ExtractNumber(matches[i], matches[i - 1]);
                continue;
            }
        }

        return gramms * pieces;
    }

    private static ulong ExtractNumber(params string[] values) 
    {
        var result = 0UL;
        var regex = new Regex("^(\\d+).*$");

        var match = values
            .Select(x => regex.Match(x))
            .FirstOrDefault(x => x.Success)
            .Groups[1]
            .ValueSpan;

        _ = ulong.TryParse(match, out result);
        return result;
    }
}