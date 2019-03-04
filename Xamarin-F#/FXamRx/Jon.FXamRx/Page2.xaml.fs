namespace Jon.FXamRx

open Xamarin.Forms
open Xamarin.Forms.Xaml

type Page2() =
   inherit ContentPage()
   let _ = base.LoadFromXaml(typeof<Page2>)