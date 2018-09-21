using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Jonathan.ReactiveProject
{
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
}