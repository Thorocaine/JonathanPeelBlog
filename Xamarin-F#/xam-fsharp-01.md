I wanted to give F# a "go" in a Xamarin project, but Visual Studio (at least on Windows) does not have a Xamarin Forms template for F#. This means that I will need to create it myself.
I am sure it will be possible, and I will use a C# project as a guide.

I am in no way profficient in F#, but that is also because I have never really used it in a "real" project, and I would like to give it a good try.

In this post, I am going to attempt to create just the outline of the project.

# Create a new project
In Visual Studio, I am creating an _F#, .Net Standard Class Library_. This will become my Xamarin Forms library (called PCL before .Net Standard). I am naming my project `Jon.FXam`.

I then want to add `Xamarin.Forms` from NuGet.

Now I need to create my App "Class", I can't find any XAML for F#, so I will just have to try my best at coding it.

Rename `Library.fs` to `App.fs` and set its content to:
```fsharp
namespace Jon.FXam

open Xamarin.Forms

type App() =
    inherit Application()
```

It builds.

# Add and Android project
I am using Android because that is what I am most familar with, and it is what I use most the time for projects at work. If I get the need to use iOS I will update this, or add to it.  For now, it is Android only.

I use the _Blank App (Android)_ Template, in F#. I name it `Jon.FXam.Android`. Some people might prefer to use `.Droid` but `.Android` seems to be the current convention.

I add `Xamarin.Forms` through NuGet, I also add a project refernece to my base project. I then need to edit `MainActivity.fs`.
```fsharp
namespace Jon.FXam.Android

open Jon.FXam
open Android.App
open Xamarin.Forms
open Xamarin.Forms.Platform.Android

type Resources = Jon.FXam.Android.Resource

[<Activity (Label = "Jon.FXam.Android", MainLauncher = true, Icon = "@mipmap/icon")>]
type MainActivity () =
    inherit FormsAppCompatActivity()

    override this.OnCreate (bundle) =
        base.OnCreate (bundle)
        Forms.Init(this, bundle)
        this.LoadApplication(new App())
```

It builds.

# AppCompat
## Theme
App Compat needs a theme, so I copy the `styles.xml` file from another Xamarin project. This goes in  __`Resources`__ __>__ __`values`__, and needs the build action set to `AndroidResource`.
```xml
<?xml version="1.0" encoding="utf-8" ?>
<resources>
  <style name="MainTheme" parent="MainTheme.Base">
  </style>
  <!-- Base theme applied no matter what API -->
  <style name="MainTheme.Base" parent="Theme.AppCompat.Light.DarkActionBar">
    <!--<item name="android:colorActivatedHighlight">#00f</item>-->
    <!--If you are using revision 22.1 please use just windowNoTitle. Without android:-->
    <item name="windowNoTitle">true</item>
    <!--We will be using the toolbar so no need to show ActionBar-->
    <item name="windowActionBar">false</item>
    <!-- Set theme colors from http://www.google.com/design/spec/style/color.html#color-color-palette -->
    <!-- colorPrimary is used for the default action bar background -->
    <item name="colorPrimary">#2196F3</item>
    <!-- colorPrimaryDark is used for the status bar -->
    <item name="colorPrimaryDark">#1976D2</item>
    <!-- colorAccent is used as the default value for colorControlActivated
         which is used to tint widgets -->
    <item name="colorAccent">#FF4081</item>
    <!-- You can also set colorControlNormal, colorControlActivated
         colorControlHighlight and colorSwitchThumbNormal. -->
    <item name="windowActionModeOverlay">true</item>

    <item name="android:datePickerDialogTheme">@style/AppCompatDialogStyle</item>
  </style>
  <style name="AppCompatDialogStyle" parent="Theme.AppCompat.Light.Dialog">
    <item name="colorAccent">#FF4081</item>
  </style>
</resources>
```

## MainActivity
The attributes on MainActivity need to reference the theme, so it gets changed to
```fsharp
[<Activity (Label = "Jon.FXam.Android", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/MainTheme")>]
```

It builds.  
It also runs, but at the moment it is only a blank screen.

# Content Page
We are back to the Base Project.

I add `New Item` to the project,  and select _Source File_. There is no XAML option. That is something I will miss, but even in a C# project project XAML is optional.  I name it `Page1.fs`, and create a very basic outline of Xamarin Forms Page.
<!--stackedit_data:
eyJoaXN0b3J5IjpbLTE5MjgwOTk0NTYsLTE5NjY5NDE0NjksLT
E3MzAyMDE3NzQsMTUzNjU1NTAwNywyNjc1MTY2LDEwMTk3Mjky
NTEsLTQ1MzkyMTMzMCwzMjExOTA4OTddfQ==
-->