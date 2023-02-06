using Shopping.Readers.Common.Contracts;
using Shopping.Readers.Common.Contracts.Static;

namespace Shopping.Readers.MT.Data;

internal readonly record struct Order : IOrder
{
    public int Id { get; init; }

    public DateOnly Date { get; init; }

    public Vendor Vendor => Vendor.MT;

    public IReadOnlyList<IOrderPosition> Positions { get; init; }
}
