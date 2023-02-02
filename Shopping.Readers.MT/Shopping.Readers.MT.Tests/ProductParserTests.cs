namespace Shopping.Readers.MT.Tests;

public class ProductParserTests
{
    [Test]
    public void ParseDate_ShouldReturn_CorrectResult()
    {
        var expected = new Product
        {
            CategoryName = "Пластиковая посуда",
            ProductFullName = "Paclan Стакан пластиковый прозрачный Party Classic 200 мл 12шт",
            TotalPrice = 49,
            UnitPrice = 49,
            Url = "https://mt.delivery/single?id=206609"
        };

        var html = ProductRenderer.Render(expected);
        var actual = ProductParser.Parse(html)
            .First();

        Assert.Multiple(() =>
        {
            Assert.That(actual.CategoryName, Is.EqualTo(expected.CategoryName));
            Assert.That(actual.FullName, Is.EqualTo(expected.ProductFullName));
            Assert.That(actual.UnitPrice, Is.EqualTo(expected.UnitPrice));
            Assert.That(actual.TotalPrice, Is.EqualTo(expected.TotalPrice));
            Assert.That(actual.Url, Is.EqualTo(expected.Url));
        });
    }
}