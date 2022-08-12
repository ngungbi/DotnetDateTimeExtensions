using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ngb.DateTime;

public class DateOnlyJsonConverter : JsonConverter<DateOnly> {
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        return DateOnly.TryParse(reader.GetString(), out var date) ? date : default;
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options) {
        var result = value.ToString("yyyy-MM-dd");
        writer.WriteStringValue(result);
    }
}
