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
<!--stackedit_data:
eyJoaXN0b3J5IjpbMTU0NDU5NzMxMSwxMDM3Nzg0NTU5LC0xNT
E5OTAwODQsLTE4NzMyMDY1OTZdfQ==
-->