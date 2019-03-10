It is time to add some actual/more reaxtive things to this. I am going to try a few simple examples.
 - A counter that will increment every 10 seconds.
 - A +/- button that will modify a number.
 - A search box that will list a few predefined options.

## Copy the project
I am going to copy my `FXamRx` project, but I won't rename anything.

## MyReactiveViewModel
Most the work will be done in `MyReactiveViewModel.fs`. Once that is done, the binding will be done in the view.

### Counter
`ReactiveUI` allows properties to be linked to observables. This is a type called `ObservableAsPropertyHelper`.

I have found a few ways that this can be defined. The one option is to make the variable mutable. I don't like this. In _C#_ it is possible to do this with a readonly variable, so it seems strange needing to make it mutable in _F#_.

If the variable is defined as a reference, it can be passed into a _byref_ parameter. That is what I will use, until I find a better way.

This ends up being
```fsharp
namespace Jon.FXamRx

open ReactiveUI
open Splat
open System
open System.Reactive.Linq

type MyReactiveViewModel (?hostScreen: IScreen) as this =
    inherit ReactiveObject()
    let counterRef = ref null

    do
        Observable
            .Interval(TimeSpan.FromSeconds 10.0, RxApp.TaskpoolScheduler)
            .ToProperty(this, "Counter", counterRef, 0L, scheduler = RxApp.MainThreadScheduler)
            |> ignore

    new() = MyReactiveViewModel(null)
    member this.Counter = counterRef.Value.Value;

    interface IRoutableViewModel with
        member this.HostScreen: IScreen = if hostScreen.IsSome then hostScreen.Value else Locator.Current.GetService<IScreen>()
        member this.UrlPathSegment: string = ""
```

I modify `MyReactiveView.xaml.fs`. For now I keep the Message label, but I will rename that later.
```fsharp
namespace Jon.FXamRx

open ReactiveUI.XamForms
open Xamarin.Forms.Xaml
open ReactiveUI
open Xamarin.Forms

type MyReactiveView () as this =
    inherit ReactiveContentPage<MyReactiveViewModel> ()
    let _ = base.LoadFromXaml(typeof<MyReactiveView>)
    let message = base.FindByName<Label>("Message")

    override __.OnAppearing() =
        base.OnAppearing()
        this.OneWayBind (this.ViewModel, (fun vm -> vm.Counter), (fun v -> (v.Message : Label).Text), (fun x -> x.ToString())) |> ignore

    member val Message = message with get
```

### +/- Stepper
I am back to MyReactiveViewModel.fs
<!--stackedit_data:
eyJoaXN0b3J5IjpbLTE4NDAyNjMwNjksMTUxODM3MTkxMywtNz
Y3MjIyMTQsLTExNDQ1Njc4NTYsNDg0NzQ1NDIwLDI4MTIzNDQz
OV19
-->