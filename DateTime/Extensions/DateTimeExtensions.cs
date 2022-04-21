namespace Ngb.DateTime.Extensions;

public static class DateTimeExtensions {
    public static double ToUnixEpoch(this System.DateTime timestamp) {
        return (timestamp - System.DateTime.UnixEpoch).TotalSeconds;
    }
}
