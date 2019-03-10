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
I am back to `MyReactiveViewModel.fs`. I want to add variable to store the current value, and add two commands. One for increase, and one for decreae.
I add
```fsharp
	let stepValueRef = ref 0
   
    ...

    member this.StepValue
        with get () = stepValueRef.Value
        and set value = this.RaiseAndSetIfChanged(stepValueRef, value) |> ignore
        
    member this.StepUpCommand = ReactiveCommand.Create(fun () -> this.StepValue <- this.StepValue + 1)
    member this.StepDownCommand = ReactiveCommand.Create(fun () -> this.StepValue <- this.StepValue - 1)
```

I am using a `ref` again, it works nicely with the byref parameter of `RaiseAndSetIfChanged`.

I add the needed controls to `MyReactiveView.xaml`, and while I have it open I rename Message.
```xml
<?xml version="1.0" encoding="utf-8"?>
<rxFroms:ReactiveContentPage xmlns="http://xamarin.com/schemas/2014/forms"
                             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
                             xmlns:rxFroms="clr-namespace:ReactiveUI.XamForms;assembly=ReactiveUI.XamForms"
                             xmlns:local="clr-namespace:Jon.FXamRx"
                             x:Class="Jon.FXamRx.MyReactiveView"
                             x:TypeArguments="local:MyReactiveViewModel">
    <StackLayout>
        <Label x:Name="Counter" VerticalOptions="Center" HorizontalOptions="Center" />
        <StackLayout Orientation="Horizontal">
            <Button x:Name="StepUp" Text="+" />
            <Label x:Name="StepValue" VerticalOptions="Center" HorizontalOptions="Center" />
            <Button x:Name="StepDown" Text="-" />
        </StackLayout>
    </StackLayout>
</rxFroms:ReactiveContentPage>
```

Then I edit `MyReactiveView.xaml.fs` to configure all the needed bindings.
```fsharp
namespace Jon.FXamRx

open ReactiveUI.XamForms
open Xamarin.Forms.Xaml
open ReactiveUI
open Xamarin.Forms

type MyReactiveView () as this =
    inherit ReactiveContentPage<MyReactiveViewModel> ()
    let _ = base.LoadFromXaml(typeof<MyReactiveView>)

    override __.OnAppearing() =
        base.OnAppearing()
        this.OneWayBind (this.ViewModel, (fun vm -> vm.Counter), (fun v -> (v.Message : Label).Text), (fun x -> x.ToString())) |> ignore
        this.OneWayBind (this.ViewModel, (fun vm -> vm.StepValue), (fun v -> (v.Message : Label).Text), (fun x -> x.ToString())) |> ignore
        this.BindCommand (this.ViewModel, (fun vm -> vm.StepUpCommand), (fun v -> v.StepUp)) |> ignore
        this.BindCommand (this.ViewModel, (fun vm -> vm.StepDownCommand), (fun v -> v.StepDown)) |> ignore

    member val Message = base.FindByName<Label>("Counter") with get
    member val StepValue = base.FindByName<Label>("StepValue") with get
    member val StepUp = base.FindByName<Button>("StepUp") with get
    member val StepDown = base.FindByName<Button>("StepDown") with get
```
<!--stackedit_data:
eyJoaXN0b3J5IjpbMTY5Njc2MTY3NywxMjMwMTQ4NTQ5LC0yMz
U1ODUzMCwxMzM2NDA0MDkxLDE1MTgzNzE5MTMsLTc2NzIyMjE0
LC0xMTQ0NTY3ODU2LDQ4NDc0NTQyMCwyODEyMzQ0MzldfQ==
-->