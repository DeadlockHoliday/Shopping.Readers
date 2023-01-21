internal record struct OrderItem
{
    public string CategoryName;
    public string ProductName;
    public decimal Quantity;
    public string QuantityType;
    public decimal UnitPrice;
    public decimal TotalPrice;
    public DateOnly OrderDate;
    public decimal OrderSum;
}