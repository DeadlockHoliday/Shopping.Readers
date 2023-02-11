namespace Shopping.Normalizing.Tests;

internal class ProductNormalizerTests
{
    [TestCase("200 мл 12шт")]
    [TestCase("200мл 12шт")]
    [TestCase("200 мл. 12шт")]
    [TestCase("200мл. 12шт")]
    [TestCase("200 мл 12шт.")]
    [TestCase("200 мл 12 шт")]
    [TestCase("200 мл 12 шт.")]
    [TestCase("200 мл 12 шт.шт.")]
    [TestCase("200 мл 12 12 шт.шт.")]
    public void ExtractMassGramms_ShouldReturn_CorrectResult(string line)
    {
        var expected = 200 * 12;
        var actual = ProductNormalizer.ExtractMassGramms(line);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("200 мл 12.2шт.")]
    [TestCase("12 шт.")]
    [TestCase("2 00 мл 1 2 шт.")]
    [TestCase("200.2 мл 12шт.")]
    [TestCase("200.2мл 12шт.")]
    public void SpecificCases(string line)
    {
        throw new NotImplementedException();
    }

    [TestCase("")]
    [TestCase("200 12 шт.")]

    public void WrongCases(string line)
    {
        throw new NotImplementedException();
    }
}
