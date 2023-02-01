using OrderReader.Html;

public class OrderParserTests
{
    [Test]
    public void Sometimes_Order_Could_Be_Empty_But_With_Sum()
    {
        throw new NotImplementedException("""
            When i just go shopping outside. 
            But still an order could have some info like... 
                some products without sum but named ones.
            Still looks like an idea for a separate project.
    """);
    }

    [Test]
    public void Parse_DuplicatedProduct_ShouldReturn_SingleResult()
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

        var html = OrderRenderer.Render(expected.OrderDate, new Product[] { expected, expected });
        var result = OrderParser.Parse(html);
        var actual = result.FirstOrDefault();

        Assert.That(result, Has.Exactly(1).Items);
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