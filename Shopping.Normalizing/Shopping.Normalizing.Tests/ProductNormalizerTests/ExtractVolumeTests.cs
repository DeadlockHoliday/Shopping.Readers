namespace Shopping.Normalizing.Tests.ProductNormalizerTests;

internal class ExtractVolumeTests
{
    [TestCase("200 мл", ExpectedResult = 200)]
    [TestCase("0.2 л", ExpectedResult = 200)]
    [TestCase("2 л", ExpectedResult = 2000)]
    [TestCase("2.2 л", ExpectedResult = 2200)]
    [TestCase("1кг", ExpectedResult = 1_000)]
    [TestCase("0кг", ExpectedResult = 0)]
    [TestCase("0.1кг", ExpectedResult = 100)]
    [TestCase("100.500кг", ExpectedResult = 100_500)]
    public long ExtractVolume_ShouldReturn_CorrectResult(string line)
        => ProductNormalizer.ExtractVolume(line);

    [TestCase("1шт", ExpectedResult = 1)]
    [TestCase("0шт", ExpectedResult = 0)]
    public long ExtractVolume_Pieces_ShouldReturn_CorrectResult(string line)
        => ProductNormalizer.ExtractVolume(line);

    [TestCase("0.1шт")]
    [TestCase("2.00.2мл")]
    public void ExtractVolume_ShouldThrow(string line)
    {
        Assert.That(() => ProductNormalizer.ExtractVolume(line), Throws.Exception);
    }

    [TestCase("200.2 мл")]
    [TestCase("200.2мл")]
    [TestCase("200,2 мл")]
    [TestCase("200,2мл")]
    public void ExtractVolume_DecimalMililiters_ShouldRound(string line)
    {
        var expected = 200;
        var actual = ProductNormalizer.ExtractVolume(line);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("Стакан 200 мл 10 шт")]
    [TestCase("Стакан 200 мл 10шт")]
    [TestCase("Стакан 200 мл 10 шт.")]
    [TestCase("Стакан 200 мл 10шт.")]
    [TestCase("Стакан пластиковый 200 мл 10шт.")]
    [TestCase("Стакан 200 мл 10шт. пластиковый.")]
    [TestCase("200 мл 10шт. Стакан пластиковый.")]
    public void ExtractVolume_RealCase_ShouldParse_Pieces(string line)
    {
        var expected = 10;
        var actual = ProductNormalizer.ExtractVolume(line);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("Молоко вкусное 1л", ExpectedResult = 1000)]
    [TestCase("Молоко вкусное 1л.", ExpectedResult = 1000)]
    [TestCase("Молоко вкусное 1 л", ExpectedResult = 1000)]
    [TestCase("Молоко вкусное 1 л.", ExpectedResult = 1000)]
    [TestCase("Молоко вкусное 1.1л.", ExpectedResult = 100)]
    [TestCase("Молоко вкусное 0.1л", ExpectedResult = 100)]
    [TestCase("Молоко 0.1л вкусное", ExpectedResult = 100)]
    [TestCase("0.1л вкусное молоко", ExpectedResult = 100)]
    public long ExtractVolume_RealCase_ShouldParse_Liters(string line)
        => ProductNormalizer.ExtractVolume(line);
}
