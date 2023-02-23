using Shopping.Readers.Common.Supplies;

namespace Shopping.Readers.MT.Export;

internal static class SupplyFormatExtensions
{
    internal static string ToCsvFileName(this ISupplyPackage supply)
        => string.Join(
            '.',
            supply.Vendor,
            supply.Date.ToStamp(),
            "csv");

    internal static string ToStamp(this DateOnly date)
        => date.ToString("yyyy-dd-MM");
}
