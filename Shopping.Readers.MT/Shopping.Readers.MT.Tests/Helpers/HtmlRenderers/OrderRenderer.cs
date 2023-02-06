using Shopping.Readers.Common.Contracts;

internal class OrderRenderer
{
    public static string Render(IOrder[] orders)
        => String.Join('\n', orders.Select(Render));

    public static string Render(IOrder order)
    {
        var orderDateStr = order.Date.ToString("dd-MM-yyyy") + " 23:59";
        return $$"""
            <table>
                <tr class="history-order">
                    <td>
                        <div class="dropdownMenu">
                            <div>
                                <div class="order-head hopened">
                                    <div class="order-data">
                                        <div class="order-data_item id">{{order.Id}}</div>
                                        <div class="order-data_item date">{{orderDateStr}} </div>
                                        <div class="order-data_item sum">9999.00 ₽</div>
                                        <div class="order-data_item state">оформлен</div>
                                    </div>
                                </div>
                            </div>
                            <div class="dropdown opened">
                                <div id="dropdownContent">
                                    <div class="order-item-container">
                                        {{order.Positions
                                            .Select(OrderPositionRenderer.Render)
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