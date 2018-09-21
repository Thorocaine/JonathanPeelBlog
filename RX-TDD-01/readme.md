## The problem

I have done a project using [Reactive Extentions](http://reactivex.io/), but I have not been able to figure out how to write unit tests for it.
My plan is o create a simple example I can use to try unit testing reactive code.

## The solution

To start with I am creating a new solution, with two projects, one is a simple .Net Standard project, and the other an XUnit project.
Add [System.Reactive](https://www.nuget.org/packages/System.Reactive/) to both projects, and add [Microsoft.Reactive.Testing](https://www.nuget.org/packages/Microsoft.Reactive.Testing/) to the test project.

I want to start with something simple, like a counter that will return the next value every seccond.

### First Test

The first test will be to make sure it starts by returning a `0`.  
The outline of test class is:

```csharp
using System.Reactive.Linq;
using System.Threading.Tasks;
using Jonathan.ReactiveProject;
using Xunit;

namespace Jonathan.ReactiveProjectTests
{
    public class ReactiveObjectTests
    {
        [Fact]
        public async Task Sequence_Recieves_0()
        {
            var fixture = new ReactiveObject();
            var result = await fixture.Sequence.FirstAsync();
            Assert.Equal(0, result);
        }
    }
}
```

This test is going to fail, becase we haven't written anything to support it yet,
so we need to start with the minimum amount of code to allow the test to pass:

```csharp
using System;
using System.Reactive.Linq;

namespace Jonathan.ReactiveProject
{
    public class ReactiveObject
    {
        public IObservable<int> Sequence { get; } = Observable.Return(0);
    }
}
```

### A test with a schedualer

I want to be able to recieve the next value, so the test needs to be a little more sofisticated.

```csharp
[Fact]
public async Task Sequence_Recieves_1()
{
    var scheduler = new TestScheduler();
    var fixture = new ReactiveObject(scheduler);
    var result = 0;
    fixture.Sequence.Take(2).Subscribe(x => result = x);
    scheduler.Start();
    Assert.Equal(1, result);
}
```

We use a [Scheduler](http://www.introtorx.com/Content/v1.0.10621.0/15_SchedulingAndThreading.html) to help with the flow. If we implemented this we would repplace the `TestScheduler` with something like `RxApp.MainThreadScheduler`, or `Scheduler.CurrentThread` depending on what thread we need the code to run on.

We `Take(2)` because we want the second result (the first one being `0`).

This test is not going to pass, so we need to update our `ReactiveObject`:

```csharp
public class ReactiveObject
{
    int counter = 0;

    public ReactiveObject(IScheduler scheduler)
    {
        Sequence = Observable
                  .Interval(TimeSpan.FromSeconds(1), scheduler)
                  .Select(_ => counter++);
    }

    public IObservable<int> Sequence { get; }
}
```

This breaks the first test, but we can remove that, and update out second `[Fact]` to a `[Theory]` to handle more options:

```csharp
[Theory]
[InlineData(0)]
[InlineData(1)]
public async Task Sequence_Recieves(int expected)
{
    var scheduler = new TestScheduler();
    var fixture = new ReactiveObject(scheduler);
    var result = 0;
    fixture.Sequence.Take(expected + 1).Subscribe(x => result = x);
    scheduler.Start();
    Assert.Equal(expected, result);
}
```

This test should still pass, so lets add more inline data, and it should still work just fine:

```csharp
[InlineData(0)]
[InlineData(1)]
[InlineData(2)]
[InlineData(3)]
[InlineData(4)]
[InlineData(5)]
[InlineData(6)]
[InlineData(7)]
[InlineData(8)]
[InlineData(9)]
[InlineData(10)]
[InlineData(100)]
[InlineData(1000)]
[InlineData(10000)]
[InlineData(100000)]
```

Because of how the `TestSchedualr` works, this still runs very quickly, and we don't have to wait 100000 seconds for the last one to run.

### Extra tests

If we want to, we can also add a test that will check the range of results returned, I will start by copying the first test, and making a few modifications to it:

```csharp
[Theory]
[InlineData(0)]
[InlineData(1)]
[InlineData(2)]
[InlineData(3)]
[InlineData(4)]
[InlineData(5)]
[InlineData(6)]
[InlineData(7)]
[InlineData(8)]
[InlineData(9)]
[InlineData(10)]
[InlineData(100)]
[InlineData(1000)]
[InlineData(10000)]
[InlineData(100000)]
public async Task Sequence_Recieves_Range(int expected)
{
    var expectedRange = Enumerable.Range(0, expected + 1);
    var scheduler = new TestScheduler();
    var fixture = new ReactiveObject(scheduler);
    var resultRange = new List<int>();
    fixture.Sequence.Take(expected + 1).Subscribe(x => resultRange.Add(x));
    scheduler.Start();
    Assert.Equal(expectedRange, resultRange);
}
```

These tests also pass, so the next step will be to refactor.

I can't see to much wrong in `ReactiveObject` so I will refactor my test class;
I can take the scheduler and fixture out of each test method (Some people prefer to set up each test from scratch within each test, and not to mix anything, but for now I will take it out). There is also a lot of duplicated `InlineData`, so I will see what can be done with that:

```csharp
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
```

## Done

That is it for now, I really don't know if this is bthe best way to do that, but it is more than what I have been able to figure out before.

If you have any questions, or suggestions, please leave a comment. If you would like to grab the code I was working on, it is on [GitHub](https://github.com/Thorocaine/JonathanPeelBlog/tree/master/RX-TDD-01).
<!--stackedit_data:
eyJoaXN0b3J5IjpbMTE5NTIwNDMyNCwxMDM5MzE4Ml19
-->