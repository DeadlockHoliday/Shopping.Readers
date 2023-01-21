var items = HtmlFileReader
    .FromCurrentDir("samples/sample.html")
    .ReadOrderItems()
    .OrderBy(x => x.ProductName)
    .ToList();

ResultWriter.WriteResult(items, "result.txt");

Console.WriteLine("done");
