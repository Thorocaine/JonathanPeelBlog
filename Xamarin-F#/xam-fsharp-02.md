Next, I want to add [ReactiveUI](https://reactiveui.net/).

## Copy Project
I am making a copy of my project folder, and calling it FXamRx. I also open up Visual Studio, the solution, projects, and all namespaces to FXamRx.  
Then build and run everything, to make sure that nothing has been broken.

<aside>
I have been using Reactice Extentions for a couple of months, and I have next to zero experiane with F#. These are both experiments for me, so I have no idea if I am doing this the "Right Way".
If anyone sees a better way anything can be done, please let me know. The main purpose of this is for me to explore and learn.
</aside>

ReactiveUI is an [MVVM](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel) framework, based on [Reactive Extensions](https://github.com/dotnet/reactive).

## Add ReactiveUI
Right click on the solution and select _Manage NuGet Pacgaes for Solution_, Search for [ReactiveUI.XamForms](https://www.nuget.org/packages/ReactiveUI.XamForms) and add it to both projects.

## Create a ViewModel
In the `Xamarin.Forms` project create a code file called `MyReactiveViewModel.fs`

Our viewmodel needs to inherit from `ReactiveObject` and `IRoutableViewModel`. It needs an `IScreen` (this is what ReactiveUI uses for switching between views and view models).

I am also going to add a message for now. This will be static, I want to get the basics working, then I will expand on it., so `MyReactiveViewModel.fs` ends up being
```fsharp
namespace Jon.FXamRx

open ReactiveUI
open Splat

type MyReactiveViewModel (?hostScreen: IScreen) =
    inherit ReactiveObject()

    member this.Message = "Welcom to a basic MVVM"

    interface IRoutableViewModel with
        member this.HostScreen: IScreen = if hostScreen.IsSome then hostScreen.Value else Locator.Current.GetService<IScreen>()
        member this.UrlPathSegment: string = ""
```

## Create a View
I am going to delete `Page1.fs`, I got the xaml page working, so I am not going to keep the old coded page. If you prefer using coded pages, please use it, but I am not going to show that here.

I make a copy of `Page2.xaml[.fs]` and rename it to `MyReactiveView.xaml[.fs]`.

The page needs to inherit from `ReactiveContentPage<T>` instead of the Xamlarin built in `ContentPage`. It also needs a type paramter that will point to the View Model. I also want to keep the label, but this time, instead of giving a default messgae I give it a name, to use for binding in the "code behind".

`MyReactiveView.xaml` now looks like this
```xml
<?xml version="1.0" encoding="utf-8"?>
<rxFroms:ReactiveContentPage xmlns="http://xamarin.com/schemas/2014/forms"
                             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
                             xmlns:rxFroms="clr-namespace:ReactiveUI.XamForms;assembly=ReactiveUI.XamForms"
                             xmlns:local="clr-namespace:Jon.FXamRx"
                             x:Class="Jon.FXamRx.MyReactiveView"
                             x:TypeArguments="local:MyReactiveViewModel">
    <Label x:Name="Message" VerticalOptions="Center" HorizontalOptions="Center" />
</rxFroms:ReactiveContentPage>
```

In `MyReactiveView.xaml.fs`  we just have to rename the type, and inherit from `ReactiveContentPage`
```fsharp
namespace Jon.FXamRx

open ReactiveUI.XamForms
open Xamarin.Forms.Xaml

type MyReactiveView () =
    inherit ReactiveContentPage<MyReactiveViewModel> ()
    let _ = base.LoadFromXaml(typeof<MyReactiveView>)
```

## AppBootstrapper
This is not going to just run, because we need to first tell the app what that "MainPage" is.

In ReactiveUI, it is common to use an AppBootstrapper. This also acts as out `IScreen` that gets sent to the View Model.

We create a new code file called `AppBootstrapper.fs`.  
In here we need to set up a very basic service locator. We will use `Splat` because it bundles with ReactiveUI. It might not be my favorite in some regards, but it is very quick to get going, and for a small project it does the job.
We need to connect `AppBootstrapper` to `IScreen`, and we need to connect our View to out View Model. We also need a small function that will create out MainPage.
```fsharp
namespace Jon.FXamRx

open ReactiveUI
open Splat
open ReactiveUI.XamForms

type AppBootstrapper () as this =
    inherit ReactiveObject ()

    let locator = Locator.CurrentMutable
    do
        locator.RegisterConstant (this :> IScreen)
        locator.Register(fun _ -> MyReactiveView() :> IViewFor<MyReactiveViewModel> )

    interface IScreen with
        member this.Router = RoutingState ()

    member this.CreateMainPage () = RoutedViewHost()
```

## App.fs
`App.fs` just needs to be wired up to create the Bootsrapper and grab the Main Page.
```fsharp
namespace Jon.FXamRx

open Xamarin.Forms

type App () =
    inherit Application (MainPage = AppBootstrapper().CreateMainPage())
```
<!--stackedit_data:
eyJoaXN0b3J5IjpbLTE0Mzc5MjY0MzMsMTg3Mjg3MzIyLC00Mz
cxODkzNTEsLTQ2ODU4NTc0MCwtMTI5NjIyMjc3NywtMTc2MTgz
OTQ0NCw2OTAwMzUxODUsMTU0NDU5NzMxMSwxMDM3Nzg0NTU5LC
0xNTE5OTAwODQsLTE4NzMyMDY1OTZdfQ==
-->