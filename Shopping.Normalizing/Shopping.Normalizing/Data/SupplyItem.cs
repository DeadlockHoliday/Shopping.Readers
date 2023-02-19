namespace Shopping.Normalizing.Data;

/// <summary>
/// Represent a bunch of items of similar nature.
/// </summary>
/// <remarks>
/// - supply of garlic.
/// - supply of bread.
/// - etc.
/// </remarks>
public readonly record struct SupplyItem
{
    public string GroupingName { get; init; }
    public ulong MassGramms { get; init; }
    public ulong Pieces { get; init; }
}