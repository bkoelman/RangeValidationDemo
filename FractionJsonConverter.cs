using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RangeDemo;

public sealed class FractionJsonConverter : JsonConverter<Fraction>
{
    public override Fraction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var culture = Thread.CurrentThread.CurrentCulture;
        Debug.WriteLine($"Entering FractionJsonConverter.Read using culture: {CultureFormatter.Format(culture)}");

        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();
            if (value != null)
            {
                try
                {
                    return Fraction.Parse(value, culture);
                }
                catch (FormatException exception)
                {
                    throw new JsonException(exception.Message, exception);
                }
            }
        }

        throw new JsonException("Expected string value in JSON document.");
    }

    public override void Write(Utf8JsonWriter writer, Fraction value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
