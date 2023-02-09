using Shopping.Readers.Common.Contracts;

namespace Shopping.Readers.MT.Export;

internal static class OrderFormatExtensions
{
    internal static string ToCsvFileName(this IOrder order)
        => string.Join(
            '.',
            order.Vendor,
            order.Date.ToStamp(),
            order.Id,
            "csv");

    internal static string ToStamp(this DateOnly date)
        => date.ToString("yyyy-dd-MM");
}
