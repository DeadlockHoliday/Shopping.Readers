using Shopping.Readers.Common.Static;

namespace Shopping.Readers.Common.Supplies;

/// <summary>
/// 
/// </summary>
public interface ISupplyPackage
{
    DateOnly Date { get; }
    Vendor Vendor { get; }
    IReadOnlyList<ISupplyPackagePosition> Positions { get; }
}