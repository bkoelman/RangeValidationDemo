using System.Globalization;

namespace RangeDemo;

public static class CultureFormatter
{
    public static string Format(CultureInfo? culture)
    {
        return CultureInfo.InvariantCulture.Equals(culture) ? "Invariant" : culture == null ? "None" : culture.Name;
    }
}
