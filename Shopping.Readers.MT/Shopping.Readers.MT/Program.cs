using Shopping.Readers.MT.Export;
using Shopping.Readers.MT.Html;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Shopping.Readers.MT.Tests")]

var input = "https://raw.githubusercontent.com/DeadlockHoliday/Shopping.Samples/main/Shopping.Samples.Readers.Input/MT.html";
var outputFolder = Environment.CurrentDirectory;

var inputHtml = GetHtml(input);

var positions = SupplyReader.Parse(inputHtml).ToArray();

ResultWriter.Write(positions, outputFolder);

static string GetHtml(string url)
{
    using (HttpClient client = new())
    {
        return client.GetStringAsync(url).Result;
    }
}