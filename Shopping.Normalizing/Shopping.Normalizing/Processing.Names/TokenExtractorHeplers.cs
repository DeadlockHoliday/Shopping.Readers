using System.Text.RegularExpressions;

namespace Shopping.Normalizing.Processing.Names;

internal static class TokenExtractorHeplers
{
    internal static IEnumerable<string> Apply(this IEnumerable<string> source, params TokenExtractor[] extractors)
    {
        var result = source.ToArray();
        foreach (var extractor in extractors)
        {
            result = extractor.Apply(result);
        }
        return result;
    }
}
