using Shopping.Readers.Common.Contracts;

namespace Shopping.Readers.MT.Export;

internal static class SupplyFormatExtensions
{
    internal static string ToCsvFileName(this ISupply supply)
        => string.Join(
            '.',
            supply.Vendor,
            supply.Date.ToStamp(),
            "csv");

    internal static string ToStamp(this DateOnly date)
        => date.ToString("yyyy-dd-MM");
}
