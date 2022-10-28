using System.Collections;

namespace Ngb.DateTimeHelper;

public readonly struct DateRange : IEnumerable<DateTime> {
    private readonly DateTime _start;
    private readonly DateTime _end;

    private DateRange(DateTime start, DateTime end) {
        if (start > end) {
            _end = start;
            _start = end;
        } else {
            _start = start;
            _end = end;
        }
    }


    /// <summary>
    /// Create an enumerable between start date and end date.
    /// </summary>
    /// <param name="start">Start date</param>
    /// <param name="end">End date</param>
    /// <returns></returns>
    public static DateRange Between(DateTime start, DateTime end) => new(start, end);

    /// <summary>
    /// Create an enumerable between start date and days after start date.
    /// </summary>
    /// <param name="start">Start date</param>
    /// <param name="days">Number of days</param>
    /// <returns></returns>
    public static DateRange From(DateTime start, int days) => new(start, start.AddDays(days));

    public DateRangeDescending Descending() => new(_end, _start);

    IEnumerator<DateTime> IEnumerable<DateTime>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public Enumerator GetEnumerator() => new(this);

    public struct Enumerator : IEnumerator<DateTime> {
        private DateTime _current;
        private readonly DateTime _end;

        public Enumerator(DateRange range) {
            _current = range._start;
            _end = range._end;
        }

        public void Reset() => throw new NotSupportedException();
        object IEnumerator.Current => Current;

        public DateTime Current {
            get {
                var current = _current;
                _current = current.AddDays(1);
                return current;
            }
        }

        public bool MoveNext() => _current <= _end;
        public void Dispose() { }
    }
}

public readonly ref struct DateRangeDescending {
    private readonly DateTime _start;
    private readonly DateTime _end;

    internal DateRangeDescending(DateTime start, DateTime end) {
        // if (start < end) {
        //     _end = start;
        //     _start = end;
        // } else {
        _start = start;
        _end = end;
        // }
    }

    public static DateRangeDescending Between(DateTime start, DateTime end)
        => start < end ? new DateRangeDescending(start, end) : new DateRangeDescending(end, start);

    public Enumerator GetEnumerator() => new(this);

    public ref struct Enumerator {
        private DateTime _current;
        private readonly DateTime _end;

        public Enumerator(DateRangeDescending range) {
            _end = range._end;
            _current = range._start;
        }

        public DateTime Current {
            get {
                var current = _current;
                _current = current.AddDays(-1);
                return current;
            }
        }

        public bool MoveNext() => _current >= _end;
    }
}
