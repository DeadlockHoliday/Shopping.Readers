using Shopping.Readers.Common.Contracts;
using Shopping.Readers.MT.Data;
using System.Collections.Immutable;
using AssertBy = Shopping.Readers.MT.Tests.Helpers.AssertHelpers.AssertBy;

namespace Shopping.Readers.MT.Tests;

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

        // TODO: create a factory
        var expectedOrders = new Order[]
        {
            new(){ Id = 1, Date = new DateOnly(2020, 10, 1), Positions = new OrderPosition[] { expectedPosition }.Cast<IOrderPosition>().ToImmutableList() },
            new(){ Id = 2, Date = new DateOnly(2022, 10, 1), Positions = new OrderPosition[] { expectedPosition }.Cast<IOrderPosition>().ToImmutableList() },
        }.Cast<IOrder>().ToArray();

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
        // TODO: reduce time from 100ms to 10ms.
        // 1. Set time limit for test.
        // 2. See bottleneck.
        // 3. Fix it.

        var expectedPosition = new OrderPosition
        {
            CategoryName = "Пластиковая посуда",
            Info = "Paclan Стакан пластиковый прозрачный Party Classic 200 мл 12шт",
            Price = 49,
            Url = "https://mt.delivery/single?id=206609",
            Quantity = 1,
        };

        // TODO: make a factory method/.
        var expectedOrderPositions = new OrderPosition[] { expectedPosition, expectedPosition }.Cast<IOrderPosition>().ToImmutableList();
        var expectedOrder = new Order { Id = 1, Date = new DateOnly(2020, 10, 1), Positions = expectedOrderPositions };

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

        var expectedOrderPositions = new OrderPosition[] { expectedPosition }.Cast<IOrderPosition>().ToImmutableList();
        var expectedOrder = new Order { Id = 1, Date = new DateOnly(2020, 10, 1), Positions = expectedOrderPositions };

        var html = OrderRenderer.Render(expectedOrder);
        var actualOrder = OrderReader.Parse(html)
            .First();

        Assert.Multiple(() =>
        {
            AssertBy.Order.Equal(expectedOrder, actualOrder);
        });

        Assert.Multiple(() =>
        {
            AssertBy.OrderPosition.Equal(expectedOrder.Positions, expectedOrderPositions);
        });
    }
}