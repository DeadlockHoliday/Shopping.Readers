using Shopping.Readers.Common.Static;

namespace Shopping.Readers.Common.Supplies;

public readonly record struct SupplyPackage : ISupplyPackage
{
    public DateOnly Date { get; init; }

    public Vendor Vendor { get; init; }

    public IReadOnlyList<ISupplyPackagePosition> Positions { get; init; }
}
