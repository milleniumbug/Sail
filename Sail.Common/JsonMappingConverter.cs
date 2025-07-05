using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sail;

public abstract class JsonMappingConverter<TSource, TTarget> : JsonConverter<TTarget>
{
    public override TTarget? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return Map(JsonSerializer.Deserialize<TSource>(ref reader, options)!);
    }

    public override void Write(Utf8JsonWriter writer, TTarget value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize<TSource>(writer, Unmap(value), options);
    }

    protected abstract TTarget Map(TSource value);

    protected abstract TSource Unmap(TTarget value);
}