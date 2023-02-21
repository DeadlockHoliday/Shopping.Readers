using Shopping.Readers.Common.Contracts;

namespace Shopping.Readers.MT.Tests.Helpers.HtmlRenderers;

internal class SupplyPositionRendere
{
    public static string Render(ISupplyPosition supplyPosition)
        => $$"""
            <div class="history-order-good">
                <a href="{{supplyPosition.Url}}" class="main-link">
                    <div class="good-img">
                        <img class="main_good-img" />
                    </div>
                    <div class="main-desc">
                        <p class="main-name">{{supplyPosition.Product.CategoryName}}</p>
                        <!---->
                        <div class="main-text">
                            <p>
                                {{supplyPosition.Product.Info}}
                            </p>
                        </div>
                    </div>
                </a>
                <div class="main-quantity">
                    <span class="item-count_label">Количество</span>
                    <span class="item-count">{{supplyPosition.Quantity}}</span>
                </div>
                <div class="main-price">
                    <div class="price-name">Цена</div>
                    <!---->
                    <p class="price-num price-num_history">
                        {{supplyPosition.Price}} ₽
                    </p>
                </div>
                <div class="main-summa">
                    <div class="price-name">Сумма</div>
                    <div class="sum-icon">
                        <p class="price-num price-num_history">
                            {{supplyPosition.TotalPrice}} ₽
                        </p>
                    </div>
                </div>
            </div>
            """;
}