using SoftCircuits.HtmlMonkey;

internal class HtmlFileReader
{
    private readonly string path;

    public HtmlFileReader(string path)
	{
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException($"'{nameof(path)}' cannot be null or whitespace.", nameof(path));
        }

        this.path = path;
    }

    public OrderItem[] ReadOrderItems()
    {
        var items = new List<OrderItem>();

        var orderNodes = HtmlDocument.FromFile(path)
            .Find(".history-order");
        
        foreach (var orderNode in orderNodes)
        {
            var date = OrderDataNodeParser.ParseDate(orderNode);
            var sum = OrderDataNodeParser.ParseSum(orderNode);

            var orderItems = orderNode.Children
                .Find(".history-order-good")
                .Select(node => OrderItemDataNodeParser.ParseOrderItem(node, date, sum))
                .ToList();

            items.AddRange(orderItems);
        }

        return items.ToArray();
    }

    public static HtmlFileReader FromCurrentDir(string subpath)
        => new (
            Path.Combine(
                Directory.GetCurrentDirectory(),
                subpath));
}