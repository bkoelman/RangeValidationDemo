using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace RangeDemo;

public sealed class FractionTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        Debug.WriteLine($"Entering FractionTypeConverter.ConvertFrom using culture: {CultureFormatter.Format(culture)}");

        return value is string stringValue
            ? Fraction.Parse(stringValue, culture)
            : base.ConvertFrom(context, culture, value);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value,
        Type destinationType)
    {
        Debug.WriteLine($"Entering FractionTypeConverter.ConvertTo using culture: {CultureFormatter.Format(culture)}");

        return destinationType == typeof(string) && value is Fraction fraction
            ? fraction.Format(culture)
            : base.ConvertTo(context, culture, value, destinationType);
    }
}
