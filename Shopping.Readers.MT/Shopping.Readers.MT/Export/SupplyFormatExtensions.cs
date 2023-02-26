using Shopping.Readers.Common.Data;

namespace Shopping.Readers.MT.Export;

internal static class SupplyFormatExtensions
{
    internal static string ToStamp(this DateOnly date)
        => date.ToString("yyyy-dd-MM");
}
