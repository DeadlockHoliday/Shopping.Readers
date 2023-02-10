namespace Shopping.Normalizing.Data;

internal readonly record struct SupplyRequest
{
    public string Name { get; init; }
    public decimal Amount { get; init; }
}