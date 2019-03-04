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

type MyReactiveViewModel (hostScreen: IScreen) =
    inherit ReactiveObject()

    member this.Message = "Welcom to a basic MVVM"
        
    interface IRoutableViewModel with
        member this.HostScreen: IScreen = hostScreen
        member this.UrlPathSegment: string = ""
```

## Create a View
I am going to delete `Page1.fs`, I got the xaml page working, so I am not going to keep the old coded page. If you prefer using coded pages, please use it, but I am not going to show that here.

I make a copy of `Page2.xaml[.fs]` and rename it to `MyReactiveView.xaml[.fs]`.


<!--stackedit_data:
eyJoaXN0b3J5IjpbLTI1NzE0MDMwMiwxNTQ0NTk3MzExLDEwMz
c3ODQ1NTksLTE1MTk5MDA4NCwtMTg3MzIwNjU5Nl19
-->