namespace Jon.FXamRx

open Xamarin.Forms

module private Page1 =
    let content () =
        let layout = new StackLayout()
        layout.Children.Add (new Label (Text = "Welcome to F# & Xamarin Forms!"))
        layout

type Page1() =
    inherit ContentPage (Content = Page1.content())
