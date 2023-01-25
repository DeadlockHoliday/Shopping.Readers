using SoftCircuits.HtmlMonkey;

namespace OrderReaderTests;

public class OrderItemDataNodeParserTests
{
    // TODO: Better naming
    [Test]
    public void ParseDate_ShouldReturn_CorrectResult()
    {
        var expectedItem = new
        {
            CategoryName = "Пластиковая посуда",
            ProductFullName = "Paclan Стакан пластиковый прозрачный Party Classic 200 мл 12шт",
            TotalPrice = 49,
            UnitPrice = 49,
            Url = "https://mt.delivery/single?id=206609"
        };
        
        var html = $$"""
        <div class="history-order-good">
            <a href="{{expectedItem.Url}}" class="main-link">
                <div class="good-img">
                    <img alt="Paclan Стакан пластиковый прозрачный Party Classic 200 мл 12шт" class="main_good-img" data-src="/img/img/items/small/206609.png" src="./История заказов МТ_files/206609.png" lazy="loaded" />
                </div>
                <div class="main-desc">
                    <p class="main-name">{{expectedItem.CategoryName}}</p>
                    <!---->
                    <div class="main-text">
                        <p>
                            {{expectedItem.ProductFullName}}
                        </p>
                    </div>
                </div>
            </a>
            <div class="main-quantity">
                <span class="item-count_label">Количество</span>
                <span class="item-count">1</span>
            </div>
            <div class="main-price">
                <div class="price-name">Цена</div>
                <!---->
                <p class="price-num price-num_history">
                    {{expectedItem.UnitPrice}} ₽
                </p>
            </div>
            <div class="main-summa">
                <div class="price-name">Сумма</div>
                <div class="sum-icon">
                    <p class="price-num price-num_history">
                        {{expectedItem.TotalPrice}} ₽
                    </p>
                </div>
            </div>
        </div>
        """;

        var item = OrderItemDataNodeParser.ParseOrderItems(html)
            .First();
                
        Assert.Multiple(() =>
        {
            Assert.That(item.CategoryName, Is.EqualTo(expectedItem.CategoryName));
            Assert.That(item.ProductFullName, Is.EqualTo(expectedItem.ProductFullName));
            Assert.That(item.UnitPrice, Is.EqualTo(expectedItem.UnitPrice));
            Assert.That(item.TotalPrice, Is.EqualTo(expectedItem.TotalPrice));
        });
    }
}