using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace template_csharp.Converters;

public class UnixTimestampConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.Number => DateTimeOffset.FromUnixTimeSeconds((long)reader.GetDouble()).UtcDateTime,
            JsonTokenType.String => DateTimeOffset.Parse(reader.GetString()!,
                    CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind).UtcDateTime,
            _ => throw new JsonException($"Unexpected token {reader.TokenType} for DateTime.")
        };
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        var utc = value.Kind == DateTimeKind.Unspecified
            ? DateTime.SpecifyKind(value, DateTimeKind.Utc)
            : value;
        writer.WriteNumberValue(((DateTimeOffset)utc).ToUnixTimeSeconds());
    }
}
