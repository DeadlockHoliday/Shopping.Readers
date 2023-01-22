public class OrderItem
{
    public string CategoryName { get; set; }
    public string ProductName { get; set; }
    public decimal Quantity { get; set; }
    public string QuantityType { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public DateOnly OrderDate { get; set; }
    public decimal OrderSum { get; set; }
}