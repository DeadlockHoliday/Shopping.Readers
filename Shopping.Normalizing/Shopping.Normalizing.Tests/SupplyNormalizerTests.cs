namespace Shopping.Normalizing.Tests;

internal class SupplyNormalizerTests
{
    [TestCase("Молоко вкусное 1л", ExpectedResult = 1000)]
    [TestCase("Молоко вкусное 1л.", ExpectedResult = 1000)]
    [TestCase("Молоко вкусное 1 л", ExpectedResult = 1000)]
    [TestCase("Молоко вкусное 1 л.", ExpectedResult = 1000)]
    [TestCase("Молоко вкусное 1.1л.", ExpectedResult = 1100)]
    [TestCase("Молоко вкусное 0.1л", ExpectedResult = 100)]
    [TestCase("Молоко 0.1л вкусное", ExpectedResult = 100)]
    [TestCase("0.1л вкусное молоко", ExpectedResult = 100)]
    public decimal NormalizeLine_RealCase_ShouldParse_Liters(string line)
        => SupplyNormalizer.NormalizeLine(line).MassGramms;

    [TestCase("0.1шт")]
    [TestCase("2.00.2мл")] // doesnt crash because regex captures 00.2.
    public void NormalizeLine_IncorrectCases_ShouldThrow(string line)
    {
        Assert.That(() => SupplyNormalizer.NormalizeLine(line), Throws.Exception);
    }
}
