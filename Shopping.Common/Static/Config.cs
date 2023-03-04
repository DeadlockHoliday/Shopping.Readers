using System.Globalization;

namespace Shopping.Common.Static;

public static class Config
{
    public static string Culture = string.Empty;

    public static CultureInfo CultureInfo => string.IsNullOrWhiteSpace(Culture) ? CultureInfo.InvariantCulture : new(Culture);
}
