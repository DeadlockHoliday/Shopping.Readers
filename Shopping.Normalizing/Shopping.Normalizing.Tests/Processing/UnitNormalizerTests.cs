using static Shopping.Normalizing.Processing.UnitNormalizer;

namespace Shopping.Normalizing.Tests.Processing;

internal class UnitNormalizerTests
{
    [TestCase("200 мл", ExpectedResult = 200)]
    [TestCase("0.2 л", ExpectedResult = 200)]
    [TestCase("2 л", ExpectedResult = 2000)]
    [TestCase("2.2 л", ExpectedResult = 2200)]
    [TestCase("1кг", ExpectedResult = 1_000)]
    [TestCase("0кг", ExpectedResult = 0)]
    [TestCase("0.1кг", ExpectedResult = 100)]
    [TestCase("100.500кг", ExpectedResult = 100_500)]
    public decimal Normalize_ShouldReturn_Gramms(string line)
    {
        throw new NotImplementedException();
    }

    [TestCase("Молоко вкусное 1л", ExpectedResult = 1000)]
    [TestCase("Молоко вкусное 1л.", ExpectedResult = 1000)]
    [TestCase("Молоко вкусное 1 л", ExpectedResult = 1000)]
    [TestCase("Молоко вкусное 1 л.", ExpectedResult = 1000)]
    [TestCase("Молоко вкусное 1.1л.", ExpectedResult = 100)]
    [TestCase("Молоко вкусное 0.1л", ExpectedResult = 100)]
    [TestCase("Молоко 0.1л вкусное", ExpectedResult = 100)]
    [TestCase("0.1л вкусное молоко", ExpectedResult = 100)]
    public decimal ExtractVolume_RealCase_ShouldParse_Liters(string line)
    {
        throw new NotImplementedException();
    }

    [TestCase("0.1шт")]
    [TestCase("2.00.2мл")]
    public void ExtractVolume_ShouldThrow(string line)
    {
        throw new NotImplementedException();
    }
}
