using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Jonathan.ReactiveProject;
using Microsoft.Reactive.Testing;
using Xunit;

namespace Jonathan.ReactiveProjectTests
{
    public class ReactiveObjectTests
    {
        readonly TestScheduler scheduler = new TestScheduler();
        readonly ReactiveObject fixture;

        public ReactiveObjectTests() => fixture = new ReactiveObject(scheduler);

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Sequence_Recieves(int expected)
        {
            var result = 0;
            fixture.Sequence.Take(expected + 1).Subscribe(x => result = x);
            scheduler.Start();
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Sequence_Recieves_Range(int expected)
        {
            var expectedRange = Enumerable.Range(0, expected + 1);
            var resultRange = new List<int>();
            fixture.Sequence.Take(expected + 1).Subscribe(x => resultRange.Add(x));
            scheduler.Start();
            Assert.Equal(expectedRange, resultRange);
        }

        public static IEnumerable<object[]> GetTestData() =>
            (new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 100, 1000, 10000, 100000 })
           .Select(x => new[] { x as object });
    }
}