Next, I want to add [ReactiveUI](https://reactiveui.net/).  
  

## Copy Project  

I am making a copy of my project folder and calling it FXamRx. I also open up Visual Studio, the solution, projects, and all namespaces to FXamRx.  
Then build and run everything, to make sure that nothing has been broken.  
  

<aside>  
I have been using Reactive Extensions for a couple of months, and I have next to zero experience with F#. These are both experiments for me, so I have no idea if I am doing this the "Right Way".  
If anyone sees a better way anything can be done, please let me know. The main purpose of this is for me to explore and learn.  
</aside>  
  

ReactiveUI is an [MVVM](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel) framework, based on [Reactive Extensions](https://github.com/dotnet/reactive).  
  

## Add ReactiveUI  

Right click on the solution and select _Manage NuGet Packages for Solution_, Search for [ReactiveUI.XamForms](https://www.nuget.org/packages/ReactiveUI.XamForms) and add it to both projects.  
  

## Create a ViewModel  

In the `Xamarin.Forms` project create a code file called `MyReactiveViewModel.fs`  
  

Our ViewModel needs to inherit from `ReactiveObject` and `IRoutableViewModel`. It needs an `IScreen` (this is what ReactiveUI uses for switching between views and view models).  
  

I am also going to add a message for now. This will be static, I want to get the basics working, then I will expand on it., so `MyReactiveViewModel.fs` ends up being  

```fsharp  
namespace  Jon.FXamRx  
  
open  ReactiveUI  
open  Splat  
  
type  MyReactiveViewModel  (?hostScreen:  IScreen)  =  
inherit  ReactiveObject()  
new()  =  MyReactiveViewModel(null)  
  
member  this.Message  =  "Welcome to a basic MVVM"  
  
interface  IRoutableViewModel  with  
member  this.HostScreen:  IScreen  =  if  hostScreen.IsSome  then  hostScreen.Value  else  Locator.Current.GetService<IScreen>()  
member  this.UrlPathSegment:  string  =  ""  
```  
  

## Create a View  

I am going to delete `Page1.fs`, I got the XAML page working, so I am not going to keep the old coded page. If you prefer using coded pages, please use it, but I am not going to show that here.  
  

I make a copy of `Page2.xaml[.fs]` and rename it to `MyReactiveView.xaml[.fs]`.  
  

The page needs to inherit from `ReactiveContentPage<T>` instead of the Xamlarin built in `ContentPage`. It also needs a _type paramter_ that will point to the View Model. I also want to keep the label, but this time, instead of giving a default message I give it a name, to use for binding in the "code behind".  
  

`MyReactiveView.xaml` now looks like this  

```xml  
<?xml version="1.0" encoding="utf-8"?>  
<rxFroms:ReactiveContentPage  xmlns="http://xamarin.com/schemas/2014/forms"  
xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"  
xmlns:rxFroms="clr-namespace:ReactiveUI.XamForms;assembly=ReactiveUI.XamForms"  
xmlns:local="clr-namespace:Jon.FXamRx"  
x:Class="Jon.FXamRx.MyReactiveView"  
x:TypeArguments="local:MyReactiveViewModel">  
<Label  x:Name="Message"  VerticalOptions="Center"  HorizontalOptions="Center"  />  
</rxFroms:ReactiveContentPage>  
```  
  

In `MyReactiveView.xaml.fs` we just have to rename the type, inherit from `ReactiveContentPage`, and bind the control to the View Model  

```fsharp  
namespace  Jon.FXamRx  
  
open  ReactiveUI.XamForms  
open  Xamarin.Forms.Xaml  
open  ReactiveUI  
open  Xamarin.Forms  
  
type  MyReactiveView  ()  as  this  =  
inherit  ReactiveContentPage<MyReactiveViewModel>  ()  
let  _  =  base.LoadFromXaml(typeof<MyReactiveView>)  
let  message  =  base.FindByName<Label>("Message")  
  
override  __.OnAppearing()  =  
base.OnAppearing()  
this.OneWayBind  (this.ViewModel,  (fun  vm  ->  vm.Message),  (fun  v  ->  (v.Message  :  Label).Text))  |>  ignore  
  
member  val  Message  =  message  with  get  
```  
  

I know that I am ignoring a subscription, and this should be disposed, but for now, I am not going to worry about that.  
  

## AppBootstrapper  

This is not going to just run, because we need to first tell the app what that "MainPage" is.  
  

In ReactiveUI, it is common to use an AppBootstrapper. This also acts as out `IScreen` that gets sent to the View Model.  
  

We create a new code file called `AppBootstrapper.fs`.  
In here we need to set up a very basic service locator. We will use `Splat` because it bundles with ReactiveUI. It might not be my favourite in some regards, but it is very quick to get going, and for a small project it does the job.  
We need to connect `AppBootstrapper` to `IScreen`, and we need to connect our View to our View Model. We also need a small function that will create our MainPage.  

```fsharp  
namespace  Jon.FXamRx  
  
open  ReactiveUI  
open  Splat  
open  ReactiveUI.XamForms  
open  Jon.FXamRx  
open  Xamarin.Forms  
  
type  AppBootstrapper()  as  this  =  
inherit  ReactiveObject()  
  
let  router  =  new  RoutingState()  
  
do  
Locator.CurrentMutable.RegisterConstant<IScreen>  this  
Locator.CurrentMutable.Register  (fun  ()  ->  new  MyReactiveView()  :>  IViewFor<MyReactiveViewModel>)  
router.NavigateAndReset.Execute(MyReactiveViewModel())  |>  ignore  
  
member  __.CreateMainPage()  =  
new  RoutedViewHost()  :>  Page  
  
interface  IScreen  with  
member  __.Router  
with  get()  =  
router  
```  
  

## App.fs  

`App.fs` just needs to be wired up to create the Bootstrapper and grab the Main Page.  

```fsharp  
namespace  Jon.FXamRx  
  
open  Xamarin.Forms  
  
type  App  ()  =  
inherit  Application  (MainPage  =  AppBootstrapper().CreateMainPage())  
```  
  

## Conclusion  

This now builds, and the expected message is shown on the screen. It might seem like a lot of work for just a message, but this outline should make the next steps of any apps a little easier.  
  

The code, up to this point, is available [on GitHub](https://github.com/Thorocaine/JonathanPeelBlog/tree/master/Xamarin-F%23/FXamRx).
<!--stackedit_data:
eyJwcm9wZXJ0aWVzIjoidGl0bGU6ICdYYW1hcmluIEZvcm1zLC
B3aXRoIEYjIC0gUGFydCAyLCBSZWFjdGl2ZVVJJ1xuYXV0aG9y
OiBKb25hdGhhbiBQZWVsXG50YWdzOiAnLk5ldCwgRiMsIFhhbW
FyaW4sIFhhbWFyaW4uRm9ybXMsIFJlYWN0aXZlVUksIFJlYWN0
aXZlIEV4dGVuc2lvbnMsIFJ4J1xuIiwiaGlzdG9yeSI6Wzc1OD
EzNzkwOCw3MjE4MTAyMTEsLTcyOTExMzY3NCwtMTA3MzUwMjcz
NSwtMTQzNzkyNjQzMywxODcyODczMjIsLTQzNzE4OTM1MSwtND
Y4NTg1NzQwLC0xMjk2MjIyNzc3LC0xNzYxODM5NDQ0LDY5MDAz
NTE4NSwxNTQ0NTk3MzExLDEwMzc3ODQ1NTksLTE1MTk5MDA4NC
wtMTg3MzIwNjU5Nl19
-->