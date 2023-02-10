namespace Shopping.Normalizing.Data;

/// <summary>
/// Represent a bunch of items of similar nature.
/// </summary>
/// <remarks>
/// - supply of garlic.
/// - supply of bread.
/// - etc.
/// </remarks>
internal readonly record struct SupplyItem
{
    public string Name { get; init; }
    public ulong MassGramms { get; init; }
    public decimal Amount { get; init; }
}