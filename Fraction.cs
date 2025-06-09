using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace RangeDemo;

[TypeConverter(typeof(FractionTypeConverter))]
[DebuggerDisplay("{ToString(),nq}")]
public sealed class Fraction(int numerator, int denominator) : IComparable<Fraction>, IComparable, IFormattable
{
    public int Numerator { get; } = numerator;
    public int Denominator { get; } = denominator;

    public static Fraction Parse(string value, CultureInfo? culture)
    {
        ArgumentNullException.ThrowIfNull(value);

        Debug.WriteLine($"Entering Fraction.Parse using culture: {CultureFormatter.Format(culture)}");

        char separator = GetSeparator(culture);
        var parts = value.Split(separator, 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 2)
        {
            if (int.TryParse(parts[0], culture, out var numerator) &&
                int.TryParse(parts[1], culture, out var denominator))
            {
                return new Fraction(numerator, denominator);
            }
        }

        throw new FormatException($"Value '{value}' is not a valid fraction.");
    }

    public string Format(CultureInfo? culture)
    {
        Debug.WriteLine($"Entering Fraction.Format using culture: {CultureFormatter.Format(culture)}");

        char separator = GetSeparator(culture);
        return $"{Numerator}{separator}{Denominator}";
    }

    private static char GetSeparator(CultureInfo? culture)
    {
        return CultureInfo.InvariantCulture.Equals(culture) ? '/' : ':';
    }

    public int CompareTo(Fraction? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (other is null)
        {
            return 1;
        }

        return ToDouble().CompareTo(other.ToDouble());
    }

    public int CompareTo(object? obj)
    {
        return CompareTo(obj as Fraction);
    }

    public double ToDouble()
    {
        return Numerator / (double)Denominator;
    }

    public override string ToString()
    {
        // Intentionally returning something unparsable, this is for diagnostics only.
        return $"{nameof(Fraction)}: {Numerator}/{Denominator} ~ {ToDouble()}";
    }

    // Used by System.Convert.ToString, which never passes a format.
    string IFormattable.ToString(string? format, IFormatProvider? formatProvider)
    {
        if (format == "f")
        {
            return formatProvider is CultureInfo culture ? Format(culture) : Format(null);
        }

        return ToDouble().ToString(formatProvider);
    }
}
