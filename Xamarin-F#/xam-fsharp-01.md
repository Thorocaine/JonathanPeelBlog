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
namespace Xamarin.Forms

type App() =
    inherit Application()
```

This builds.

# Add and Android project
I am using Android because that is what I am most familar with, and it is what I use most the time for projects at work. If I get the need to use iOS I will update this, or add to it.  For now, it is Android only.

I use the _Blank App (Android)_ Template, in F#. I name it `Jon.FXam.Android`. Some people might prefer to use `.Droid` but `.Android` seems to be the current convention.

1. I add a project refernece to my base project.
2. I then ne
<!--stackedit_data:
eyJoaXN0b3J5IjpbMTU4MDY0OTIzMiwxMDE5NzI5MjUxLC00NT
M5MjEzMzAsMzIxMTkwODk3XX0=
-->