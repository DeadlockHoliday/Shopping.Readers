using System.Text.RegularExpressions;

namespace Shopping.Normalizing.Processing.Names;

internal class TokenExtractor
{
    private static readonly RegexOptions regexOptions =
          RegexOptions.IgnoreCase
        | RegexOptions.CultureInvariant
        | RegexOptions.Compiled
        | RegexOptions.Multiline
        | RegexOptions.IgnorePatternWhitespace;

    private readonly Regex regex;
    private readonly bool isMatch;

    private TokenExtractor(string regexSelector, bool isMatch)
    {
        this.regex = new Regex(regexSelector, regexOptions);
        this.isMatch = isMatch;
    }

    public static TokenExtractor Inclusive(string regexSelector)
        => new(regexSelector, true);

    public static TokenExtractor Exclusive(string regexSelector)
        => new(regexSelector, false);

    public string[] Apply(IEnumerable<string> tokens)
        => tokens.Where(x => regex.IsMatch(x) == isMatch).ToArray();

    public string[] Apply(string line)
        => regex.Matches(line).Select(x => x.Value).ToArray();

    public static string[] Apply(IEnumerable<string> tokens, TokenExtractor[] extractors)
    {
        var result = tokens.ToArray();
        foreach (var extractor in extractors)
        {
            result = extractor.Apply(result);
        }

        return result;
    }
}
