using System;
using Ngb.DateTimeHelper;
using Ngb.DateTimeHelper.Extensions;
using NUnit.Framework;

namespace Tests;

public class RangeTest {
    [SetUp]
    public void Setup() { }

    private readonly DateTime _first = new(2022, 10, 20);
    private readonly DateTime _date2 = new(2022, 10, 25);

    [Test]
    public void AscendingTest() {
        foreach (var date in DateRange.Between(_first, _date2)) {
            Console.WriteLine(date);
        }

        foreach (var date in _first.Until(_date2)) { }

        foreach (var date in DateRange.From(_first, 5)) {
            Console.WriteLine(date);
        }
    }

    [Test]
    public void DescendingTest() {
        foreach (var date in DateRange.Between(_first, _date2).Descending()) {
            Console.WriteLine(date);
        }
    }
}
