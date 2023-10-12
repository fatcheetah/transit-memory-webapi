using System.Text.Json;

namespace transit_parser.ExtensionMethods;

public static class JsonExtension
{
    public static string ToJson<T>(this T value)
    {
        return JsonSerializer.Serialize(value);
    }
}