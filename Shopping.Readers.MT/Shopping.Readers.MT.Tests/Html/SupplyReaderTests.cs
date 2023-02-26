using Shopping.Readers.Common.Data;
using Shopping.Readers.Common.Data.Products;
using Shopping.Readers.Common.Data.Supply;

namespace Shopping.Readers.MT.Tests.Html;

public class SupplyReaderTests
{
    [Test]
    public void Parse_SameProduct_InDifferentOrders_ShouldReturn_Duplicates()
    {
        var product = new Product()
        {
            Category = "Пластиковая посуда",
            Info = "Paclan Стакан пластиковый прозрачный Party Classic 200 мл 12шт",
        };

        var expectedPositions = new SupplyPosition[]
        {
            new() {
                Product = product,
                Invoice = new()
                {
                    Price = new NMoneys.Money(49),
                    //Url = "https://mt.delivery/single?id=206609",
                    Quantity = 1,
                    Date = new DateOnly(2020, 10, 1),
                }
            },
            new() {
                Product = product,
                Invoice = new()
                {
                    Price = new NMoneys.Money(51),
                    //Url = "https://mt.delivery/single?id=206609",
                    Quantity = 3,
                    Date = new DateOnly(2020, 11, 1),
                }
            },
        };

        var html = SupplyPackagesRenderer.Render(expectedPositions);
        var result = SupplyReader.Parse(html);

        Assert.That(result, Is.EquivalentTo(expectedPositions));        
    }

    [Test]
    public void Parse_SingleProduct_ShouldReturn_CorrectResult()
    {
        var expectedPosition = new SupplyPosition
        {
            Product = new()
            {
                Category = "Пластиковая посуда",
                Info = "Paclan Стакан пластиковый прозрачный Party Classic 200 мл 12шт",
            },
            Invoice = new Invoice()
            {
                Price = new NMoneys.Money(49),
                //Url = "https://mt.delivery/single?id=206609",
                Quantity = 1,
                Date = new DateOnly(2020, 10, 1),
            }
        };

        var html = SupplyPackagesRenderer.Render(expectedPosition);
        var actualPosition = SupplyReader.Parse(html).First();

        Assert.Multiple(() =>
        {
            Assert.That(expectedPosition, Is.EqualTo(actualPosition));
        });
    }
}