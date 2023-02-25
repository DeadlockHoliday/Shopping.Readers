

using Shopping.Readers.Common.Supplies;

namespace Shopping.Readers.MT.Tests.Helpers.HtmlRenderers;

internal class SupplyPackagePositionRenderer
{
    public static string Render(ISupplyPackagePosition supplyPackagePosition)
        => $$"""
            <div class="history-order-good">
                <a href="www.example.com" class="main-link">
                    <div class="good-img">
                        <img class="main_good-img" />
                    </div>
                    <div class="main-desc">
                        <p class="main-name">{{supplyPackagePosition.Product.CategoryName}}</p>
                        <!---->
                        <div class="main-text">
                            <p>
                                {{supplyPackagePosition.Product.Name}}
                            </p>
                        </div>
                    </div>
                </a>
                <div class="main-quantity">
                    <span class="item-count_label">Количество</span>
                    <span class="item-count">{{supplyPackagePosition.Quantity}}</span>
                </div>
                <div class="main-price">
                    <div class="price-name">Цена</div>
                    <!---->
                    <p class="price-num price-num_history">
                        {{supplyPackagePosition.Price.Amount}} ₽
                    </p>
                </div>
                <div class="main-summa">
                    <div class="price-name">Сумма</div>
                    <div class="sum-icon">
                        <p class="price-num price-num_history">
                            {{supplyPackagePosition.TotalPrice.Amount}} ₽
                        </p>
                    </div>
                </div>
            </div>
            """;
}