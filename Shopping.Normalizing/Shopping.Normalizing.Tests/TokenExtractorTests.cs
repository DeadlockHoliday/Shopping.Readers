using static Shopping.Normalizing.TokenExtractor;

namespace Shopping.Normalizing.Tests;

internal class TokenExtractorTests
{
    [TestCase("Стакан 200 мл")]
    [TestCase("Стакан 200мл")]
    [TestCase("Стакан 200 мл.")]
    [TestCase("Стакан 200мл.")]
    public void ExtractTokens_ShouldReturn_CorrectResult(string line)
    {
        var expected = new MeasureToken[]
        {
            new(200, "мл")
        };

        var actual = ExtractTokens(line);
        Assert.That(actual, Is.EquivalentTo(expected));
    }
}
