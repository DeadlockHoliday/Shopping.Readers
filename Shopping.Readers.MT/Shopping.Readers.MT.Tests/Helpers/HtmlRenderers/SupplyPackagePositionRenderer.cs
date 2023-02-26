

using Shopping.Readers.Common.Data;
using Shopping.Readers.Common.Data.Products;

namespace Shopping.Readers.MT.Tests.Helpers.HtmlRenderers;

internal class SupplyPackagePositionRenderer
{
    public static string Render(UnprocessedSupplyPackagePosition supplyPackagePosition)
        => $$"""
            <div class="history-order-good">
                {{Render(supplyPackagePosition.Product)}}
                {{Render(supplyPackagePosition as ISupplyPackagePosition)}}
            </div>
            """;

    public static string Render(IEnumerable<UnprocessedSupplyPackagePosition> supplyPackagePositions)
        => supplyPackagePositions.Select(Render)
            .Aggregate((x, y) => x + Environment.NewLine + y);

    private static string Render(UnprocessedProduct product)
        => $$"""
        <a href="www.example.com" class="main-link">
                <div class="good-img">
                    <img class="main_good-img" />
                </div>
                <div class="main-desc">
                    <p class="main-name">{{product.CategoryName}}</p>
                    <!---->
                    <div class="main-text">
                        <p>
                            {{product.Info}}
                        </p>
                    </div>
                </div>
            </a>
        """;

    private static string Render(ISupplyPackagePosition position)
    => $$"""
            <div class="main-quantity">
                <span class="item-count_label">Количество</span>
                <span class="item-count">{{position.Quantity}}</span>
            </div>
            <div class="main-price">
                <div class="price-name">Цена</div>
                <!---->
                <p class="price-num price-num_history">
                    {{position.Price.Amount}} ₽
                </p>
            </div>
            <div class="main-summa">
                <div class="price-name">Сумма</div>
                <div class="sum-icon">
                    <p class="price-num price-num_history">
                        {{position.TotalPrice.Amount}} ₽
                    </p>
                </div>
            </div>
        """;

}