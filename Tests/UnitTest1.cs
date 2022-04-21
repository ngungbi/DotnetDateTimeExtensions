using System;
using System.Text.Json;
using Ngb.DateTime;
using Ngb.DateTime.Extensions;
using Ngb.DateTime.TimeZone;
using NUnit.Framework;

namespace Tests;

public class DateObject {
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
}

public class Tests {
    [SetUp]
    public void Setup() { }

    [Test]
    public void UnixEpochTest() {
        var timestamp = new DateTime(2022, 4, 21, 14, 17, 51, DateTimeKind.Utc); // DateTime.UtcNow;
        var epoch = timestamp.ToUnixEpoch();
        Assert.IsTrue((long) epoch == 1650550671);
        Assert.Pass();
    }

    [Test]
    public void JsonConverterTest() {
        var jsonOptions = new JsonSerializerOptions() {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        jsonOptions.Converters.Add(new DateOnlyJsonConverter());
        jsonOptions.Converters.Add(new TimeOnlyJsonConverter());
        var date = new DateOnly(2022, 4, 20);
        var time = new TimeOnly(21, 15, 55);
        var obj = new {
            date,
            time
        };
        var json = JsonSerializer.Serialize(obj, jsonOptions);
        Assert.IsFalse(string.IsNullOrEmpty(json));
        // Assert.Pass();
    }

    [Test]
    public void JsonDeserializeTest() {
        const string json = "{\"date\":\"2022-04-20\", \"time\":\"21:15:55\"}";
        var jsonOptions = new JsonSerializerOptions() {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        jsonOptions.Converters.Add(new DateOnlyJsonConverter());
        jsonOptions.Converters.Add(new TimeOnlyJsonConverter());
        var date = new DateOnly(2022, 4, 20);
        var time = new TimeOnly(21, 15, 55);
        var obj = JsonSerializer.Deserialize<DateObject>(json, jsonOptions);
        if (obj is null) {
            Assert.Fail();
            return;
        }

        Assert.IsTrue(obj.Date == date);
        Assert.IsTrue(obj.Time == time);
    }

    [Test]
    public void TimeZoneTest() {
        var timeZoneProvider = new TimeZoneProvider();
        var wib = timeZoneProvider.GetTimeZone(TimeZoneName.AsiaJakarta);
        var wita = timeZoneProvider.GetTimeZone(TimeZoneName.WITA);

        Assert.IsFalse(wib.Equals(TimeZoneInfo.Utc));
        Assert.IsTrue(wib.DisplayName.Contains("Jakarta"));
        Assert.IsTrue(wita.DisplayName.Contains("Singapore"));
    }
}
