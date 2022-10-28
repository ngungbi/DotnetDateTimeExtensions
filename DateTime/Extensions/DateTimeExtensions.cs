using System.Runtime.CompilerServices;

namespace Ngb.DateTimeHelper.Extensions;

public static class DateTimeExtensions {
    /// <summary>
    /// Convert DateTime to Unix epoch
    /// </summary>
    /// <param name="timestamp">Input timestamp</param>
    /// <returns>Unix epoch seconds</returns>
    public static double ToUnixEpoch(this System.DateTime timestamp) {
        return (timestamp - System.DateTime.UnixEpoch).TotalSeconds;
    }

    /// <summary>
    /// Create an enumerable range of date
    /// </summary>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns></returns>
    public static DateRange Until(this DateTime startDate, DateTime endDate) => DateRange.Between(startDate, endDate);

    public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan) {
        // Console.WriteLine($"Wait for {timeSpan.TotalSeconds} seconds");
        return Task.Delay(timeSpan).GetAwaiter();
    }

    public static TaskAwaiter GetAwaiter(this DateTime dateTime) => (dateTime - DateTime.UtcNow).GetAwaiter();

    public static TaskAwaiter GetAwaiter(this DateTimeOffset dateTimeOffset) => dateTimeOffset.UtcDateTime.GetAwaiter();
}
