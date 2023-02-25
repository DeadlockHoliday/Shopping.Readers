using NMoneys;
using Shopping.Readers.Common.Static;

namespace Shopping.Readers.Common.Data;

public interface ISupplyPackagePosition
{
    public DateOnly Date { get; init; }

    public Vendor Vendor { get; init; }

    public long Quantity { get; init; }

    public Money Price { get; init; }

    public Money TotalPrice => Price.Times(Quantity);
}
