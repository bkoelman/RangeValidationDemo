using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace RangeDemo;

public sealed class FractionTypeConverter : TypeConverter
{
    // The culture parameter in TypeConverter.ConvertFromString is documented as:
    //   A CultureInfo. If null is passed, the current culture is assumed.
    // See https://learn.microsoft.com/dotnet/api/system.componentmodel.typeconverter.convertfromstring.

    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || sourceType == typeof(decimal) || base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        var effectiveCulture = culture ?? Thread.CurrentThread.CurrentCulture;
        Debug.WriteLine($"Entering FractionTypeConverter.ConvertFrom using culture: {CultureFormatter.Format(culture)} => {CultureFormatter.Format(effectiveCulture)}");

        return value switch
        {
            string stringValue => Fraction.Parse(stringValue, effectiveCulture),
            decimal decimalValue => DoubleToFractionConverter.DoubleToFraction((double)decimalValue),
            _ => base.ConvertFrom(context, effectiveCulture, value)
        };
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value,
        Type destinationType)
    {
        var effectiveCulture = culture ?? Thread.CurrentThread.CurrentCulture;
        Debug.WriteLine($"Entering FractionTypeConverter.ConvertTo using culture: {CultureFormatter.Format(culture)} => {CultureFormatter.Format(effectiveCulture)}");

        return value switch
        {
            Fraction fraction when destinationType == typeof(string) => fraction.Format(effectiveCulture),
            Fraction fraction when destinationType == typeof(decimal) => (decimal)fraction.ToDouble(),
            _ => base.ConvertTo(context, effectiveCulture, value, destinationType)
        };
    }
}