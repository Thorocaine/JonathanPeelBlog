namespace Jon.FXamRx

open ReactiveUI
open Splat

type MyReactiveViewModel (?hostScreen: IScreen) =
    inherit ReactiveObject()
    new() = MyReactiveViewModel(null)

    member this.Message = "Welcom to a basic MVVM"

    interface IRoutableViewModel with
        member this.HostScreen: IScreen = if hostScreen.IsSome then hostScreen.Value else Locator.Current.GetService<IScreen>()
        member this.UrlPathSegment: string = ""