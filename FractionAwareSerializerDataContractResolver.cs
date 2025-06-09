using System.Text.Json;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RangeDemo;

public sealed class FractionAwareSerializerDataContractResolver(JsonSerializerOptions serializerOptions)
    : ISerializerDataContractResolver
{
    private readonly JsonSerializerDataContractResolver _defaultResolver = new(serializerOptions);

    public DataContract GetDataContractForType(Type type)
    {
        var effectiveType = Nullable.GetUnderlyingType(type) ?? type;

        if (effectiveType == typeof(Fraction))
        {
            return DataContract.ForPrimitive(
                underlyingType: effectiveType,
                dataType: DataType.Number,
                dataFormat: "double",
                jsonConverter: value => JsonSerializer.Serialize(value, type, serializerOptions));
        }

        return _defaultResolver.GetDataContractForType(type);
    }
}
