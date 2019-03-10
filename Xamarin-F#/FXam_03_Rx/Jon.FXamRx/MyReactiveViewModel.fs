namespace Jon.FXamRx

open ReactiveUI
open Splat
open System
open System.Reactive.Linq

type MyReactiveViewModel (?hostScreen: IScreen) as this =
    inherit ReactiveObject()
    let counterRef = ref null
    let stepValueRef = ref 0

    do
        Observable
            .Interval(TimeSpan.FromSeconds 10.0, RxApp.TaskpoolScheduler)
            .ToProperty(this, "Counter", counterRef, 0L, scheduler = RxApp.MainThreadScheduler)
            |> ignore

    new() = MyReactiveViewModel(null)

    member this.StepValue
        with get () = stepValueRef.Value
        and set value = this.RaiseAndSetIfChanged(stepValueRef, value) |> ignore

    member this.Counter = counterRef.Value.Value;
    member this.StepUpCommand = ReactiveCommand.Create(fun () -> this.StepValue <- this.StepValue + 1)
    member this.StepDownCommand = ReactiveCommand.Create(fun () -> this.StepValue <- this.StepValue - 1)

    interface IRoutableViewModel with
        member this.HostScreen: IScreen = if hostScreen.IsSome then hostScreen.Value else Locator.Current.GetService<IScreen>()
        member this.UrlPathSegment: string = ""