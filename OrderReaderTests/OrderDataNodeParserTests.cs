using SoftCircuits.HtmlMonkey;

public class OrderItemDataNodeParserTests
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

        var html = ProductRenderer.RenderProductHtml(expected);
        var actual = ProductNodeParser.Parse(html)
            .First();
                
        Assert.Multiple(() =>
        {
            Assert.That(actual.CategoryName, Is.EqualTo(expected.CategoryName));
            Assert.That(actual.FullName, Is.EqualTo(expected.ProductFullName));
            Assert.That(actual.UnitPrice, Is.EqualTo(expected.UnitPrice));
            Assert.That(actual.TotalPrice, Is.EqualTo(expected.TotalPrice));
        });
    }
}