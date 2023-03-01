using Shopping.Common.Data.Supply;
using Shopping.Readers.MT.Html;
using SoftCircuits.HtmlMonkey;

namespace Shopping.Readers.MT.Facade;

public class MTSupplyPositionsReader : ISupplyPositionsReader
{
    public SupplyPosition[] Read(string html)
        => SupplyReader.Parse(HtmlDocument.FromHtml(html));
}
