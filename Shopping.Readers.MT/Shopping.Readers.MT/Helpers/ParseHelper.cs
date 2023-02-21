using NMoneys;

namespace Shopping.Readers.MT.Helpers;

internal static class ParseHelper
{
    public static decimal? AsDecimal(this string value)
    {
        if (decimal.TryParse(value, out var result)) { return result; }
        else return null;
    }

    public static long? AsLong(this string value)
    {
        if (long.TryParse(value, out var result)) { return result; }
        else return null;
    }

    public static decimal? AsSum(this string value)
        => value.Split(' ')[0].AsDecimal();

    public static Money AsMoney(this decimal value)
        => new Money(value);
}
