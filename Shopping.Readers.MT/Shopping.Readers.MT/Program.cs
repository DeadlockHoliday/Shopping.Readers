using Shopping.Readers.MT;
using Shopping.Readers.MT.Html;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Shopping.Readers.MT.Tests")]

var fileNowTimeStamp = DateTime.UtcNow.ToFileTimeUtc();
var input = "https://gist.githubusercontent.com/DeadlockHoliday/8f4cf41fa7f2db97df40d0eff9d607ef/raw/b253538fcd5a215659450b331ac9f506abd229fe/gistfile1.txt";
var output = $"products.mt.{fileNowTimeStamp}.csv";

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