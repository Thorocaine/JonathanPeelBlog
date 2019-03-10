namespace Jon.FXamRx

open ReactiveUI
open Splat
open ReactiveUI.XamForms
open Jon.FXamRx
open Xamarin.Forms

type AppBootstrapper() as this =
    inherit ReactiveObject()

    let router = new RoutingState()

    do
        Locator.CurrentMutable.RegisterConstant<IScreen> this
        Locator.CurrentMutable.Register (fun () -> new MyReactiveView() :> IViewFor<MyReactiveViewModel>)
        router.NavigateAndReset.Execute(MyReactiveViewModel()) |> ignore

    member __.CreateMainPage() =
        new RoutedViewHost() :> Page

    interface IScreen with
        member __.Router
            with get() =
                router
