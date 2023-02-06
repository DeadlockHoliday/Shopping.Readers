namespace Shopping.Readers.MT.Tests;

public class ProductReaderTests
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
        var actual = ProductReader.Read(html)
            .First();

        Assert.Multiple(() =>
        {
            Assert.That(actual.CategoryName, Is.EqualTo(expected.CategoryName));
            Assert.That(actual.Info, Is.EqualTo(expected.ProductFullName));
            Assert.That(actual.Price, Is.EqualTo(expected.UnitPrice));
            Assert.That(actual.Url, Is.EqualTo(expected.Url));
        });
    }
}