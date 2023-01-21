using SoftCircuits.HtmlMonkey;

internal class OrderItemDataNodeParser
{
    public static List<OrderItem> ParseOrderItems(HtmlElementNode orderNode)
    {
        var result = new List<OrderItem>();
        var date = OrderDataNodeParser.ParseDate(orderNode);
        var sum = OrderDataNodeParser.ParseSum(orderNode);

        var itemNodes = orderNode.Children.Find(".history-order-good");
        foreach (var node in itemNodes)
        {
            var children = node.Children
                .Where(x => !string.IsNullOrWhiteSpace(x.Text))
                .ToArray();

            var prices = children.Find(".price-num")
                .Select(x => x.Text.Trim()
                    .Split(' ')
                    .First())
                .ToArray();

            var orderItem = new OrderItem()
            {
                CategoryName = children.Find(".main-name").First().Text,
                ProductName = children.Find(".main-text > p").First().Text,
                Quantity = decimal.Parse(children.Find(".item-count").First().Text),
                QuantityType = "?",
                UnitPrice = decimal.Parse(prices[0]),
                TotalPrice = decimal.Parse(prices[1]),
                OrderDate = date,
                OrderSum = sum
            };
            result.Add(orderItem);
        }

        return result;
    }
}