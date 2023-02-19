using System.Text.RegularExpressions;

namespace Shopping.Normalizing.Processing.Names;

internal static class NameExtractor
{
    private static readonly TokenExtractor[] filters = new[]
    {
        TokenExtractor.Exclusive(@"^.{0,2}$"), // smallWords
        TokenExtractor.Exclusive(@"\w+(?:ая)"), // adjectives
        TokenExtractor.Exclusive(@"\w+ньон"), // foreignWords
        TokenExtractor.Exclusive(@"\(.+?\)"), // parentesed (words)
        TokenExtractor.Exclusive(@"^[a-zA-Z]+?$"), // english
    };

    private static readonly string[] specialWords = new string[]
    {
        "корень",
        "Крупа",
        "Масло",
        "Пена",
        "Хлопья"
    };

    /// <summary>
    /// Extracts a name as single word.
    /// </summary>
    /// <remarks>
    /// If failed to recognize a single word - returns multiple words instead.
    /// </remarks>
    internal static string Extract(string line)
    {
        return ExtractWithSpecialWords(line)
            ?? ExtractWithFilters(line);
    }

    private static string ExtractWithFilters(string line)
    {
        var tokens = line.Split(' ')
            .Apply(filters);

        return string.Join(' ', tokens);
    }

    private static string? ExtractWithSpecialWords(string line) 
    {
        var tokens = line.Split(' ');
        if (!tokens.Any())
        {
            return null;
        }

        if (specialWords.Any(x => x.Equals(tokens[0])))
        {
            return string.Join(' ', tokens[0], tokens[1]);
        }

        return null;
    }
}
