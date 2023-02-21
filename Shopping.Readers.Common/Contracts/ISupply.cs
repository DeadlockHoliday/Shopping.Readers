using Shopping.Readers.Common.Contracts.Static;

namespace Shopping.Readers.Common.Contracts;

public interface ISupply
{
    DateOnly Date { get; }
    Vendor Vendor { get; }
    IReadOnlyList<ISupplyPosition> Positions { get; }
}