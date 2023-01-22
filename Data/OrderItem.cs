internal readonly record struct OrderItem
{
    public string CategoryName { get; init; }
    public string ProductName { get; init; }
    public decimal OrderQuantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal TotalPrice { get; init; }
    public DateOnly OrderDate { get; init; }
    public decimal OrderSum { get; init; }
}