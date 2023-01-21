using SoftCircuits.HtmlMonkey;

var path = Path.Combine(
    Directory.GetCurrentDirectory(), 
    "samples/sample.html");

var doc = HtmlDocument.FromFile(path);
var orderNodes = doc.Find(".history-order");
var items = new List<OrderItem>();

foreach (var orderNode in orderNodes)
{
    items.AddRange(ParseOrderItems(orderNode));
}

items = items.OrderBy(x => x.ProductName).ToList();

using var outStream = File.OpenWrite("result.txt");
using var textWriter = new StreamWriter(outStream);

foreach (var item in items)
{
    textWriter.WriteLine(item);
}

Console.WriteLine("done");

static List<OrderItem> ParseOrderItems(HtmlElementNode orderNode)
{
    var date = ParseDate(orderNode);
    var sum = ParseSum(orderNode);
    var itemNodes = orderNode.Children.Find(".history-order-good");

    var result = new List<OrderItem>();
    foreach (var node in itemNodes)
    {
        var children = node.Children
            .Where(x => !string.IsNullOrWhiteSpace(x.Text))
            .ToArray();

        var prices = children.Find(".price-num").Select(x => x.Text.Trim().Split(' ').First()).ToArray();

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

static string[] ParseOrderData(HtmlElementNode node)
{
    var orderDataRow = node.Children
        .Find(".order-data")
        .First();

    return orderDataRow.Children
        .Find("div")
        .Select(x => x.Text.Trim())
        .ToArray();
}

static DateOnly ParseDate(HtmlElementNode node)
{
    var orderData = ParseOrderData(node);
    return DateOnly.ParseExact(orderData[1].Split(' ').First(), "dd-mm-yyyy");
}

static decimal ParseSum(HtmlElementNode node)
{
    var orderData = ParseOrderData(node);
    return decimal.Parse(orderData[2].Split(' ').First());
}

internal record struct OrderItem
{
    public string CategoryName; // .main-name
    public string ProductName; // .main-text > p.text()
    public decimal Quantity; // .item-count
    public string QuantityType; // Pieces, kg.
    public decimal UnitPrice; // .main-price > .price-num.text()
    public decimal TotalPrice; // .main-summa > .price-num.text()
    public DateOnly OrderDate;
    public decimal OrderSum;
}