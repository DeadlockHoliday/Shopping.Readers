using Shopping.Processing.Products.Names;
using static Shopping.Processing.Products.Tests.Names.AssertHelpers;

namespace Shopping.Processing.Products.Tests.Names;

internal class NameExtractorTests
{
    [TestCase("Корень имбиря", "Корень имбиря")]
    public void Extract_ShouldKeep_Names_IfUnclear(string line, string expected)
    {
        var actual = NameExtractor.Extract(line) ?? "";
        AssertSample(new(line, expected), actual);
    }

    [Test]
    public void Extract_ShouldExtract_Names_FromVegetables_Correctly()
    {
        AssertSamples(Samples.Vegetables);
    }
}
