using System.Text;
using System.Text.Json;

namespace FieldOps.Shared.Infrastructure.Modules;

internal sealed class JsonModuleSerializer : IModuleSerializer
{
    private readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public byte[] Serialize<T>(T value)
        => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, options));

    public T Deserialize<T>(byte[] value)
        => JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(value), options)!;

    public object Deserialize(byte[] value, Type type)
        => JsonSerializer.Deserialize(Encoding.UTF8.GetString(value), type, options)!;
}
