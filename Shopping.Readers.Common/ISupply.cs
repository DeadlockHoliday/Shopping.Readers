using Shopping.Readers.Common.Static;

namespace Shopping.Readers.Common;

public interface ISupply
{
    DateOnly Date { get; }
    Vendor Vendor { get; }
    IReadOnlyList<ISupplyPosition> Positions { get; }
}