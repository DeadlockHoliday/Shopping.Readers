using Shopping.Common.Data.Supply;

namespace Shopping.Common.Modules;

public interface ISupplyPositionsReader
{
    SupplyPosition[] Read(string html);
}