using Shopping.Readers.MT.Data;
using Shopping.Readers.MT.Html;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Shopping.Readers.MT.Tests")]

const string input = "samples/sample.html";
const string output = "OrderReader.db";

var inputHtml = GetHtml(ToCurrentPath(input));

var items = OrderParser.Parse(inputHtml)
    .Where(x => x.CategoryName != "Услуги")
    .ToList();

ResultWriter.WriteResult(items, output);

static string ToCurrentPath(string path)
    => Path.Combine(
                Directory.GetCurrentDirectory(),
                path);

static string GetHtml(string path)
    => File.ReadAllText(path);