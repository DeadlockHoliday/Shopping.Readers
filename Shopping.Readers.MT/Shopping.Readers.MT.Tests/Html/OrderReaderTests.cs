using Shopping.Readers.Common.Contracts;
using Shopping.Readers.MT.Data;
using Shopping.Readers.MT.Tests.Helpers;
using AssertBy = Shopping.Readers.MT.Tests.Helpers.AssertHelpers.AssertBy;

namespace Shopping.Readers.MT.Tests.Html;

public class OrderReaderTests
{
    [Test]
    public void Parse_SameProduct_InDifferentOrders_ShouldReturn_SameProduct()
    {
        var expectedPosition = new OrderPosition
        {
            CategoryName = "Пластиковая посуда",
            Info = "Paclan Стакан пластиковый прозрачный Party Classic 200 мл 12шт",
            Price = 49,
            Url = "https://mt.delivery/single?id=206609",
            Quantity = 1,
        };

        var expectedOrders = new IOrder[]
        {
            OrderFactory.CreateOrder(1, new DateOnly(2020, 10, 1), expectedPosition),
            OrderFactory.CreateOrder(2, new DateOnly(2022, 10, 1), expectedPosition),
        };

        var html = OrderRenderer.Render(expectedOrders);
        var result = OrderReader.Parse(html);

        Assert.That(result, Has.Exactly(2).Items);

        Assert.Multiple(() =>
        {
            AssertBy.Order.Equal(expectedOrders, result);
        });
    }

    [Test]
    public void Parse_DuplicatedProduct_ShouldReturn_SingleResult()
    {
        var expectedPosition = new OrderPosition
        {
            CategoryName = "Пластиковая посуда",
            Info = "Paclan Стакан пластиковый прозрачный Party Classic 200 мл 12шт",
            Price = 49,
            Url = "https://mt.delivery/single?id=206609",
            Quantity = 1,
        };

        var expectedOrder = OrderFactory.CreateOrder(
            1,
            new DateOnly(2020, 10, 1),
            expectedPosition,
            expectedPosition);

        var html = OrderRenderer.Render(expectedOrder);
        var result = OrderReader.Parse(html);
        var actualOrder = result.First();

        Assert.That(actualOrder.Positions, Has.Exactly(1).Items);
    }

    [Test]
    public void Parse_SingleProduct_ShouldReturn_CorrectResult()
    {
        var expectedPosition = new OrderPosition
        {
            CategoryName = "Пластиковая посуда",
            Info = "Paclan Стакан пластиковый прозрачный Party Classic 200 мл 12шт",
            Price = 49,
            Url = "https://mt.delivery/single?id=206609",
            Quantity = 1,
        };

        var expectedOrder = OrderFactory.CreateOrder(
            1,
            new DateOnly(2020, 10, 1),
            expectedPosition);

        var html = OrderRenderer.Render(expectedOrder);
        var actualOrder = OrderReader.Parse(html).First();

        Assert.Multiple(() =>
        {
            AssertBy.Order.Equal(expectedOrder, actualOrder);
        });

        Assert.Multiple(() =>
        {
            AssertBy.OrderPosition.Equal(actualOrder.Positions, expectedOrder.Positions);
        });
    }
}