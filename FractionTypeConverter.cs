using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace RangeDemo;

public sealed class FractionTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || sourceType == typeof(double) || base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        Debug.WriteLine($"Entering FractionTypeConverter.ConvertFrom using culture: {CultureFormatter.Format(culture)}");

        return value switch
        {
            string stringValue => Fraction.Parse(stringValue, culture),
            double doubleValue => DoubleToFractionConverter.DoubleToFraction(doubleValue),
            _ => base.ConvertFrom(context, culture, value)
        };
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value,
        Type destinationType)
    {
        Debug.WriteLine($"Entering FractionTypeConverter.ConvertTo using culture: {CultureFormatter.Format(culture)}");

        return value switch
        {
            Fraction fraction when destinationType == typeof(string) => fraction.Format(culture),
            Fraction fraction when destinationType == typeof(double) => fraction.ToDouble(),
            _ => base.ConvertTo(context, culture, value, destinationType)
        };
    }
}