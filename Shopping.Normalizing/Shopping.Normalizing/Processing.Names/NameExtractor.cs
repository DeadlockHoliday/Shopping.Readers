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
    };

    private static readonly TokenExtractor[] unclearWordExtractors = new[]
    {
        TokenExtractor.Inclusive(@"^(?:корень)$"),
    };

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

        return IsFirstWordClear(filteredWords) switch
        {
            true => filteredWords.First(),
            false => string.Join(' ', filteredWords),
        };
    }

    private static bool IsFirstWordClear(IEnumerable<string> filteredWords)
    {
        var firstWord = filteredWords.Take(1);

        bool isTokenClear(TokenExtractor x)
            => x.Apply(firstWord).Count() == 0;

        return unclearWordExtractors.All(isTokenClear);
    }
}
