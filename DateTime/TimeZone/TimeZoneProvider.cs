using System;
using System.Collections.Generic;

namespace Ngb.DateTimeHelper.TimeZone;

public class TimeZoneProvider {
    private readonly Dictionary<string, TimeZoneInfo> _list = new();

    public TimeZoneInfo GetTimeZone(string timeZone) {
        if (_list.TryGetValue(timeZone, out var timeZoneInfo)) {
            return timeZoneInfo;
        }

        try {
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            _list.Add(timeZone, timeZoneInfo);
            return timeZoneInfo;
        } catch (TimeZoneNotFoundException) {
            return TimeZoneInfo.Utc;
        }
    }
}
