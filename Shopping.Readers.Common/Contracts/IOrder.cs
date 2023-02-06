using Shopping.Readers.Common.Contracts.Static;

namespace Shopping.Readers.Common.Contracts;

public interface IOrder
{
    int Id { get; }
    DateOnly Date { get; }
    Vendor Vendor { get; }
    IReadOnlyList<IOrderPosition> Positions { get; }
}