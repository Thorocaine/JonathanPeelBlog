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
        this.OneWayBind (this.ViewModel, (fun vm -> vm.Message), (fun v -> (v.Message : Label).Text)) |> ignore

    member val Message = message with get