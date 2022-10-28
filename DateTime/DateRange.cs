namespace Ngb.DateTimeHelper;

public readonly ref struct DateRange {
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


    public static DateRange Between(DateTime start, DateTime end) => new(start, end);
    public static DateRange From(DateTime start, int days) => new(start, start.AddDays(days));

    public DateRangeDescending Descending() => new(_end, _start);

    public Enumerator GetEnumerator() => new(this);

    public ref struct Enumerator {
        private DateTime _current;
        private readonly DateTime _end;

        public Enumerator(DateRange range) {
            _current = range._start;
            _end = range._end;
        }

        public DateTime Current {
            get {
                var current = _current;
                _current = current.AddDays(1);
                return current;
            }
        }

        public bool MoveNext() => _current <= _end;
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
