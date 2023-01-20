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

using var outStream = File.OpenWrite("result.txt");
using var textWriter = new StreamWriter(outStream);

foreach (var item in items)
{
    throw new NotImplementedException("Why record doesnt write ToString()? ");
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
            Name = children.Find(".main-name").First().Text,
            Description = children.Find(".main-text > p").First().Text,
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
    internal string Name; // .main-name
    internal string Description; // .main-text > p.text()
    internal decimal Quantity; // .item-count
    internal string QuantityType; // Pieces, kg.
    internal decimal UnitPrice; // .main-price > .price-num.text()
    internal decimal TotalPrice; // .main-summa > .price-num.text()
    internal DateOnly OrderDate;
    internal decimal OrderSum;
}