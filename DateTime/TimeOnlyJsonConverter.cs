using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ngb.DateTimeHelper;

public sealed class TimeOnlyJsonConverter : JsonConverter<TimeOnly> {
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        return TimeOnly.TryParse(reader.GetString(), out var time) ? time : default;
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options) {
        var result = value.ToString("HH:mm:ss");
        writer.WriteStringValue(result);
    }
}
