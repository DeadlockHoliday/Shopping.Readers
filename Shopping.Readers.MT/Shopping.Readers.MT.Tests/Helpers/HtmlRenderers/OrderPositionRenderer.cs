using Shopping.Readers.Common.Contracts;

namespace Shopping.Readers.MT.Tests.Helpers.HtmlRenderers;

internal class OrderPositionRenderer
{
    public static string Render(IOrderPosition orderPosition)
        => $$"""
            <div class="history-order-good">
                <a href="{{orderPosition.Url}}" class="main-link">
                    <div class="good-img">
                        <img class="main_good-img" />
                    </div>
                    <div class="main-desc">
                        <p class="main-name">{{orderPosition.CategoryName}}</p>
                        <!---->
                        <div class="main-text">
                            <p>
                                {{orderPosition.Info}}
                            </p>
                        </div>
                    </div>
                </a>
                <div class="main-quantity">
                    <span class="item-count_label">Количество</span>
                    <span class="item-count">{{orderPosition.Quantity}}</span>
                </div>
                <div class="main-price">
                    <div class="price-name">Цена</div>
                    <!---->
                    <p class="price-num price-num_history">
                        {{orderPosition.Price}} ₽
                    </p>
                </div>
                <div class="main-summa">
                    <div class="price-name">Сумма</div>
                    <div class="sum-icon">
                        <p class="price-num price-num_history">
                            {{orderPosition.TotalPrice}} ₽
                        </p>
                    </div>
                </div>
            </div>
            """;
}