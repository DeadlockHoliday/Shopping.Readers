using Shopping.Common.Data.Supply;

namespace Shopping.Data;

public interface ISupplyPositionRepository
{
    Guid Add(SupplyPosition position);
    bool Update(SupplyPosition position);
    void Remove(Guid id);
    SupplyPosition Get(string jsonPath);
    SupplyPosition[] Load(string jsonPath);
}
