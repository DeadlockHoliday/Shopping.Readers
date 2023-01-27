internal class OrderRenderer
{
    public static string Render(DateOnly orderDate)
        => Render(orderDate, Array.Empty<Product>());

    public static string Render(DateOnly orderDate, Product product)
        => Render(orderDate, new Product[] { product });

    public static string Render(DateOnly orderDate, IEnumerable<Product> products)
    {
        var orderDateStr = orderDate.ToString("dd-MM-yyyy") + " 23:59";
        return $$"""
            <table>
                <tr class="history-order">
                    <td>
                        <div class="dropdownMenu">
                            <div>
                                <div class="order-head hopened">
                                    <div class="order-data">
                                        <div class="order-data_item id">100964</div>
                                        <div class="order-data_item date">{{orderDateStr}} </div>
                                        <div class="order-data_item sum">9261.00 ₽</div>
                                        <div class="order-data_item state">оформлен</div>
                                    </div>
                                </div>
                            </div>
                            <div class="dropdown opened">
                                <div id="dropdownContent">
                                    <div class="order-item-container">
                                        {{products
                                            .Select(ProductRenderer.Render)
                                            .Aggregate((x, y) => x + Environment.NewLine +  y)}}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        """;
    }
    
}