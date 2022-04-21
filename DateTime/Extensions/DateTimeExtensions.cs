namespace Ngb.DateTime.Extensions;

public static class DateTimeExtensions {
    /// <summary>
    /// Convert DateTime to Unix epoch
    /// </summary>
    /// <param name="timestamp">Input timestamp</param>
    /// <returns>Unix epoch seconds</returns>
    public static double ToUnixEpoch(this System.DateTime timestamp) {
        return (timestamp - System.DateTime.UnixEpoch).TotalSeconds;
    }
}
