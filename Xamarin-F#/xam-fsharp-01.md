I wanted to give F# a "go" in a Xamarin project, but Visual Studio (at least on Windows) does not have a Xamarin Forms template for F#. This means that I will need to create it myself.
I am sure it will be possible, and I will use a C# project as a guide.

I am in no way profficient in F#, but that is also because I have never really used it in a "real" project, and I would like to give it a good try.

In this post, I am going to attempt to create just the outline of the project.

# Create a new project
In Visual Studio, I am creating an _F#, .Net Standard Class Library_. This will become my Xamarin Forms library (called PCL before .Net Standard). I am naming my project `Jon.FXam`.

I then want to add `Xamarin.Forms` from NuGet.

Now I need to create my App "Class", I can't find any XAML for F#, so I will just have to try my best at coding it.

1. Rename `Library.fs` to `App.fs`
2. 
<!--stackedit_data:
eyJoaXN0b3J5IjpbLTQ1MzkyMTMzMCwzMjExOTA4OTddfQ==
-->