using Shopping.Readers.Common.Supplies;

namespace Shopping.Readers.MT.Data;

internal readonly record struct Supply : ISupplyPackage
{
    public DateOnly Date { get; init; }

    public Vendor Vendor => Vendor.MT;

    public IReadOnlyList<ISupplyPackagePosition> Positions { get; init; }
}
