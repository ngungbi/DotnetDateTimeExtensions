using System;
using System.Collections.Generic;

namespace Ngb.DateTimeHelper.TimeZone;

public sealed class TimeZoneProvider {
    private readonly Dictionary<string, TimeZoneInfo> _list = new();
    public static TimeZoneProvider Shared { get; } = new();

    // public static TimeZoneInfo GetTimeZone(string timeZone) => Shared.GetTimeZone(timeZone);

    // [Obsolete("Use TimeZoneInfo.FindSystemTimeZoneById directly")]
    public TimeZoneInfo GetTimeZone(string timeZone) {
        if (_list.TryGetValue(timeZone, out var timeZoneInfo)) {
            return timeZoneInfo;
        }

        try {
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            _list.TryAdd(timeZone, timeZoneInfo);
            return timeZoneInfo;
        } catch (TimeZoneNotFoundException) {
            return TimeZoneInfo.Utc;
        }
    }
}
