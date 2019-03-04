namespace Jon.FXamRx.Android

// open System
open Jon.FXamRx
open Android.App
// open Android.Content
// open Android.OS
// open Android.Runtime
// open Android.Views
// open Android.Widget
open Xamarin.Forms
open Xamarin.Forms.Platform.Android

type Resources = Jon.FXamRx.Android.Resource

[<Activity (Label = "Jon.FXamRx.Android", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/MainTheme")>]
type MainActivity () =
    inherit FormsAppCompatActivity()

    // let mutable count:int = 1

    override this.OnCreate (bundle) =
        base.OnCreate bundle
        Forms.Init (this, bundle)
        this.LoadApplication (new App())

        //// Set our view from the "main" layout resource
        //this.SetContentView (Resources.Layout.Main)

        //// Get our button from the layout resource, and attach an event to it
        //let button = this.FindViewById<Button>(Resources.Id.myButton)
        //button.Click.Add (fun args ->
        //    button.Text <- sprintf "%d clicks!" count
        //    count <- count + 1
        //)
