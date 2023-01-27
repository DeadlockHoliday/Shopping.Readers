namespace OrderReader.Data;

internal readonly record struct Product
{
    public string CategoryName { get; init; }
    public string FullName { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal TotalPrice { get; init; }
    public DateOnly? OrderDate { get; init; }
    public string Url { get; init; }
}