
internal class ProductRenderer
{
    public static string Render(Product product)
    => $$"""
            <div class="history-order-good">
                <a href="{{product.Url}}" class="main-link">
                    <div class="good-img">
                        <img alt="Paclan Стакан пластиковый прозрачный Party Classic 200 мл 12шт" class="main_good-img" />
                    </div>
                    <div class="main-desc">
                        <p class="main-name">{{product.CategoryName}}</p>
                        <!---->
                        <div class="main-text">
                            <p>
                                {{product.ProductFullName}}
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
                        {{product.UnitPrice}} ₽
                    </p>
                </div>
                <div class="main-summa">
                    <div class="price-name">Сумма</div>
                    <div class="sum-icon">
                        <p class="price-num price-num_history">
                            {{product.TotalPrice}} ₽
                        </p>
                    </div>
                </div>
            </div>
            """;
}