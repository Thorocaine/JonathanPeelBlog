It is time to add some actual/more reaxtive things to this. I am going to try a few simple examples.
 - A search box that will list a few predefined options.
 - A counter that will increment every 10 seconds.
 - A +/- button that will modify a number.

## Copy the project
I am going to copy my `FXamRx` project, but I won't rename anything.

## MyReactiveViewModel
Most the work will be done in `MyReactiveViewModel.fs`. Once that is done, the binding will be done in the view.

### Counter
`ReactiveUI` allows properties to be linked to observables. This is a type called `ObservableAsPropertyHelper`.

I have found a few ways that this can be defined. The one option is to make the variable mutable. I don't like this. In _C#` it is possible to do this with a readonly variable, so it seems strange needing to make it mutable in `F#`.


<!--stackedit_data:
eyJoaXN0b3J5IjpbLTIwNDMyNzQ3MDYsLTc2NzIyMjE0LC0xMT
Q0NTY3ODU2LDQ4NDc0NTQyMCwyODEyMzQ0MzldfQ==
-->