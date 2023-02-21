﻿using Shopping.Readers.Common.Contracts;
using Shopping.Readers.MT.Data;
using Shopping.Readers.MT.Tests.Helpers;
using AssertBy = Shopping.Readers.MT.Tests.Helpers.AssertHelpers.AssertBy;

namespace Shopping.Readers.MT.Tests.Html;

public class SupplyReaderTests
{
    [Test]
    public void Parse_SameProduct_InDifferentOrders_ShouldReturn_SameProduct()
    {
        var expectedPosition = new SupplyPosition
        {
            Product = new Product()
            {
                CategoryName = "Пластиковая посуда",
                Info = "Paclan Стакан пластиковый прозрачный Party Classic 200 мл 12шт",
            },
            Price = new NMoneys.Money(49),
            Url = "https://mt.delivery/single?id=206609",
            Quantity = 1,
        };

        var expectedOrders = new ISupply[]
        {
            SupplyFactory.Create(new DateOnly(2020, 10, 1), expectedPosition),
            SupplyFactory.Create(new DateOnly(2022, 10, 1), expectedPosition),
        };

        var html = SupplyRenderer.Render(expectedOrders);
        var result = SupplyReader.Parse(html);

        Assert.That(result, Has.Exactly(2).Items);

        Assert.Multiple(() =>
        {
            AssertBy.Supply.Equal(expectedOrders, result);
        });
    }

    [Test]
    public void Parse_DuplicatedProduct_ShouldReturn_SingleResult()
    {
        var expectedPosition = new SupplyPosition
        {
            Product = new Product()
            {
                CategoryName = "Пластиковая посуда",
                Info = "Paclan Стакан пластиковый прозрачный Party Classic 200 мл 12шт",
            },            
            Price = new NMoneys.Money(49),
            Url = "https://mt.delivery/single?id=206609",
            Quantity = 1,
        };

        var expectedOrder = SupplyFactory.Create(
            new DateOnly(2020, 10, 1),
            expectedPosition,
            expectedPosition);

        var html = SupplyRenderer.Render(expectedOrder);
        var result = SupplyReader.Parse(html);
        var actualOrder = result.First();

        Assert.That(actualOrder.Positions, Has.Exactly(1).Items);
    }

    [Test]
    public void Parse_SingleProduct_ShouldReturn_CorrectResult()
    {
        var expectedPosition = new SupplyPosition
        {
            Product = new Product
            {
                CategoryName = "Пластиковая посуда",
                Info = "Paclan Стакан пластиковый прозрачный Party Classic 200 мл 12шт",
            },
            Price = new NMoneys.Money(49),
            Url = "https://mt.delivery/single?id=206609",
            Quantity = 1,
        };

        var expectedOrder = SupplyFactory.Create(
            new DateOnly(2020, 10, 1),
            expectedPosition);

        var html = SupplyRenderer.Render(expectedOrder);
        var actualOrder = SupplyReader.Parse(html).First();

        Assert.Multiple(() =>
        {
            AssertBy.Supply.Equal(expectedOrder, actualOrder);
        });

        Assert.Multiple(() =>
        {
            AssertBy.SupplyPosition.Equal(actualOrder.Positions, expectedOrder.Positions);
        });
    }
}