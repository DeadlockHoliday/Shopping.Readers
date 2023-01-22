var input = "samples/sample.html";
var output = "OrderReader.db";

var items = HtmlFileReader
    .FromCurrentDir(input)
    .ReadOrderItems()
    .Where(x => x.CategoryName != "Услуги")
    .OrderBy(x => x.CategoryName)
    .ThenBy(x => x.ProductName)
    .ToList();

ResultWriter.WriteResult(items, output);