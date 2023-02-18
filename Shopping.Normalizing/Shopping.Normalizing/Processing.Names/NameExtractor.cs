using System.Text.RegularExpressions;

namespace Shopping.Normalizing.Processing.Names;

internal static class NameExtractor
{
    private static readonly TokenExtractor[] filters = new TokenExtractor[]
    {
        TokenExtractor.Exclusive(@"^.{0,2}$"), // smallWords
        TokenExtractor.Exclusive(@"\w+(?:ая)"), // adjectives
        TokenExtractor.Exclusive(@"\w+ньон"), // foreignWords
        TokenExtractor.Exclusive(@"\(.+?\)"), // parentesed (words)
    };

    private static readonly TokenExtractor unclearWordsExtractor = TokenExtractor.Inclusive(@"^(?:корень)$");

    /// <summary>
    /// Extracts a name as single word.
    /// </summary>
    /// <remarks>
    /// If failed to recognize a single word - returns multiple words instead.
    /// </remarks>
    internal static string Extract(string line)
    {
        var filteredWords = line
            .Split(' ')
            .Apply(filters);

        var isFirstWordUnclear = filteredWords.Take(1).Apply(unclearWordsExtractor).Any();
        return isFirstWordUnclear switch
        {
            true => string.Join(' ', filteredWords),
            _ => filteredWords.First()
        };
    }
}
