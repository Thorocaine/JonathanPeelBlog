namespace Jon.FXamRx

open Xamarin.Forms

type App () =
    inherit Application (MainPage = (new AppBootstrapper()).CreateMainPage())