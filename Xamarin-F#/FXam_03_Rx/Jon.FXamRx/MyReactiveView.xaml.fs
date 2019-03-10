namespace Jon.FXamRx

open ReactiveUI.XamForms
open Xamarin.Forms.Xaml
open ReactiveUI
open Xamarin.Forms
open System.Reactive.Disposables

type MyReactiveView () as this =
    inherit ReactiveContentPage<MyReactiveViewModel> ()
    let _ = base.LoadFromXaml(typeof<MyReactiveView>)

    let activated disposables =
       this.OneWayBind(this.ViewModel, (fun vm -> vm.Counter), (fun v -> (v.Message : Label).Text), (fun x -> x.ToString()))
           .DisposeWith(disposables)
           |> ignore

       this.OneWayBind(this.ViewModel, (fun vm -> vm.StepValue), (fun v -> (v.Message : Label).Text), (fun x -> x.ToString()))
           .DisposeWith(disposables)
           |> ignore

       this.BindCommand(this.ViewModel, (fun vm -> vm.StepUpCommand), (fun v -> v.StepUp))
           .DisposeWith(disposables)
           |> ignore

       this.BindCommand(this.ViewModel, (fun vm -> vm.StepDownCommand), (fun v -> v.StepDown))
           .DisposeWith(disposables)
           |> ignore

    do this.WhenActivated(activated) |> ignore

    member val Message = base.FindByName<Label>("Counter") with get
    member val StepValue = base.FindByName<Label>("StepValue") with get
    member val StepUp = base.FindByName<Button>("StepUp") with get
    member val StepDown = base.FindByName<Button>("StepDown") with get