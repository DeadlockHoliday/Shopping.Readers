namespace Shopping.Normalizing.Tests;

internal class ProductNormalizerTests
{
    [Test]
    public void ExtractMassGramms_ShouldReturn_CorrectResult()
    {
        var line = "200 мл 12шт";

        var expected = 200 * 12;
        var actual = ProductNormalizer.ExtractMassGramms(line);

        Assert.That(actual, Is.EqualTo(expected));
    }
}
