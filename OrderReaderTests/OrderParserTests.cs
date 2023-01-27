using OrderReader.Html;

public class OrderParserTests
{
    [Test]
    public void Parse_SingleProduct_ShouldReturn_CorrectResult()
    {
        var expected = new Product
        {
            CategoryName = "Пластиковая посуда",
            ProductFullName = "Paclan Стакан пластиковый прозрачный Party Classic 200 мл 12шт",
            TotalPrice = 49,
            UnitPrice = 49,
            Url = "https://mt.delivery/single?id=206609",
            OrderDate = new DateOnly(2020, 10, 1)
        };

        var html = OrderRenderer.Render(expected.OrderDate, expected);
        var actual = OrderParser.Parse(html)
            .First();
                
        Assert.Multiple(() =>
        {
            Assert.That(actual.CategoryName, Is.EqualTo(expected.CategoryName));
            Assert.That(actual.FullName, Is.EqualTo(expected.ProductFullName));
            Assert.That(actual.UnitPrice, Is.EqualTo(expected.UnitPrice));
            Assert.That(actual.TotalPrice, Is.EqualTo(expected.TotalPrice));
            Assert.That(actual.OrderDate, Is.EqualTo(expected.OrderDate));
            Assert.That(actual.Url, Is.EqualTo(expected.Url));
        });
    }
}