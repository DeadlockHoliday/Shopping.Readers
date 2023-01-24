using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("OrderReaderTests")]

const string input = "samples/sample.html";
const string output = "OrderReader.db";

var items = OrderDataNodeParser.ReadOrderItems(ToCurrentPath(input))
    .Where(x => x.CategoryName != "Услуги")
    .ToList();

ResultWriter.WriteResult(items, output);

static string ToCurrentPath(string path)
    => Path.Combine(
                Directory.GetCurrentDirectory(),
                path);