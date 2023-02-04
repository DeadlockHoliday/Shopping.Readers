using Shopping.Readers.MT;
using Shopping.Readers.MT.Html;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Shopping.Readers.MT.Tests")]

var fileNowTimeStamp = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
var input = "https://raw.githubusercontent.com/DeadlockHoliday/Shopping.Samples/main/Shopping.Samples.Readers.Input/MT.html";
var output = $"MT.{fileNowTimeStamp}.csv";

var inputHtml = GetHtml(input);

var items = OrderReader.Parse(inputHtml)
    .Where(x => x.CategoryName != "Услуги")
    .ToList();

ResultWriter.WriteResult(items, output);

static string GetHtml(string url)
{
    using (HttpClient client = new())
    {
        return client.GetStringAsync(url).Result;
    }
}