using Shopping.Normalizing.Data;
using static Shopping.Normalizing.Processing.UnitExtractor;

namespace Shopping.Normalizing.Tests.Processing;

internal class UnitExtractorTests
{
    [TestCase("Стакан 200 мл")]
    [TestCase("Стакан 200мл")]
    [TestCase("Стакан 200 мл.")]
    [TestCase("Стакан 200мл.")]
    public void Exctract_SingleToken_ShouldReturn_CorrectResult(string line)
    {
        var expected = new Unit[]
        {
            new(200, "мл")
        };

        var actual = Extract(line);
        Assert.That(actual, Is.EquivalentTo(expected));
    }

    [TestCase("Стакан 200 мл 10 шт")]
    [TestCase("Стакан 200мл 10шт")]
    [TestCase("Стакан 200 мл. 10 шт.")]
    [TestCase("Стакан 200мл. 10шт.")]
    public void Exctract_MultipleTokens_ShouldReturn_CorrectResult(string line)
    {
        var expected = new Unit[]
        {
            new(200, "мл"),
            new(10, "шт")
        };

        var actual = Extract(line);
        Assert.That(actual, Is.EquivalentTo(expected));
    }

    [TestCase("Стакан 200.0 мл", "200.0")]
    [TestCase("Стакан 200.5 мл", "200.5")]
    [TestCase("Стакан 200.5мл", "200.5")]
    [TestCase("Стакан 200.9 мл", "200.9")]
    [TestCase("Стакан 200.1 мл", "200.1")]
    [TestCase("Стакан 200.19 мл", "200.19")]
    public void Exctract_DecimalTokens_ShouldReturn_CorrectResult(string line, string expected)
    {
        var actual = Extract(line)
            .ElementAt(0)
            .Value
            .ToString();

        Assert.That(actual, Is.EqualTo(expected));
    }
}
