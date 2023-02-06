namespace Shopping.Readers.MT.Tests;

public class OrderReaderTests
{
    [Test]
    public void Parse_SameProduct_InDifferentOrders_ShouldReturn_SameProduct()
    {
        var expected = new Product
        {
            CategoryName = "Пластиковая посуда",
            ProductFullName = "Paclan Стакан пластиковый прозрачный Party Classic 200 мл 12шт",
            TotalPrice = 49,
            UnitPrice = 49,
            Url = "https://mt.delivery/single?id=206609"
        };

        var firstOrderDate = new DateOnly(2020, 10, 1);
        var secondOrderDate = new DateOnly(2020, 10, 1);

        var firstOrderHtml = OrderRenderer.Render(firstOrderDate, expected);
        var secondOrderHtml = OrderRenderer.Render(secondOrderDate, expected);

        var html = firstOrderHtml + secondOrderHtml;
        var result = OrderReader.Parse(html);

        Assert.That(result, Has.Exactly(2).Items);

        Assert.Multiple(() =>
        {
            Assert.That(result[0].Date, Is.EqualTo(firstOrderDate));
            Assert.That(result[1].Date, Is.EqualTo(secondOrderDate));
        });

        Assert.Multiple(() =>
        {
            foreach (var actualOrder in result)
            {
                Assert.Multiple(() =>
                {
                    foreach (var actualPosition in actualOrder.Positions)
                    {
                        Assert.That(actualPosition.CategoryName, Is.EqualTo(expected.CategoryName));
                        Assert.That(actualPosition.Info, Is.EqualTo(expected.ProductFullName));
                        Assert.That(actualPosition.Price, Is.EqualTo(expected.UnitPrice));
                        Assert.That(actualPosition.Url, Is.EqualTo(expected.Url));
                    }
                });
            }
        });
    }

    [Test]
    public void Parse_DuplicatedProduct_ShouldReturn_SingleResult()
    {
        // TODO: reduce time from 100ms to 10ms.
        // 1. Set time limit for test.
        // 2. See bottleneck.
        // 3. Fix it.

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
        var result = OrderReader.Parse(html);
        var actual = result.FirstOrDefault();

        Assert.That(result, Has.Exactly(1).Items);
        Assert.Multiple(() =>
        {
            Assert.That(actual.CategoryName, Is.EqualTo(expected.CategoryName));
            Assert.That(actual.Info, Is.EqualTo(expected.ProductFullName));
            Assert.That(actual.Price, Is.EqualTo(expected.UnitPrice));
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
        var actual = OrderReader.Parse(html)
            .First();

        Assert.Multiple(() =>
        {
            Assert.That(actual.CategoryName, Is.EqualTo(expected.CategoryName));
            Assert.That(actual.Info, Is.EqualTo(expected.ProductFullName));
            Assert.That(actual.Price, Is.EqualTo(expected.UnitPrice));
            Assert.That(actual.TotalPrice, Is.EqualTo(expected.TotalPrice));
            Assert.That(actual.OrderDate, Is.EqualTo(expected.OrderDate));
            Assert.That(actual.Url, Is.EqualTo(expected.Url));
        });
    }
}