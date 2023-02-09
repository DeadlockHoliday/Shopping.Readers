namespace Shopping.Readers.MT.Helpers;

internal static class ParseHelper
{
    public static decimal? AsDecimal(this string value)
    {
        if (decimal.TryParse(value, out decimal result)) { return result; }
        else return null;
    }

    public static decimal? AsSum(this string value)
        => value.Split(' ')[0].AsDecimal();
}
