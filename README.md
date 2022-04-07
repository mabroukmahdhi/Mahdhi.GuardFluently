<h1 align=center>
<img src="https://github.com/mabroukmahdhi/Mahdhi.GuardFluently/blob/main/Assets/logo/logo.svg" >
</h1>

### Nuget
![Nuget](https://img.shields.io/nuget/v/Mahdhi.GuardFluently.Core)
![Nuget](https://img.shields.io/nuget/dt/Mahdhi.GuardFluently.Core) 

### Social
![Twitter URL](https://img.shields.io/twitter/url?style=social&url=https%3A%2F%2Fgithub.com%2Fmabroukmahdhi%2FMahdhi.GuardFluently)
![Twitter Follow](https://img.shields.io/twitter/follow/Mabrouk_Mahdhi?style=social)
# Intoduction

This is a simple library that helps you to use [Microsoft.Toolkit.Diagnostics.Guard](https://docs.microsoft.com/en-us/dotnet/api/microsoft.toolkit.diagnostics.guard?view=win-comm-toolkit-dotnet-7.0) extensions with more fluency.

The idea is not new, I tried to copy some code and personalize some code from the project [FluentAssertions](https://github.com/fluentassertions/fluentassertions). 

So thank you [FluentAssertions' guys](https://github.com/fluentassertions/fluentassertions/graphs/contributors) ❤️❤️❤️

# How to use?

Let's say we have a service ```SomeService``` that defince the following method: 
```c#
 public void DoSomeCall(string name, object length)
{ 
  // do something
}
```
Here you will sure need to make some checks for your parameters ```name``` and ```length```. 

Let's say :
- the ```name``` shouldn't be null and should have at least 10 chars.
- the ```length``` should be assignable to the type ```int```

In this case your check can look like this,
```c#
public void DoSomeCall(string name, object length)
{
  //guard
  name.Should()
      .NotBeNullOrWhiteSpace()
      .And
      .HaveLengthGreaterThan(10);

  length.Should().BeAssignableTo(typeof(int));

 // do something
}
```
In this example there are three exceptions that can be thrown:
- ```System.ArgumentNullException```: Thrown if ```name``` is null.
- ```System.ArgumentException```: Thrown if ```name``` is empty or whitespace.
- ```System.ArgumentException```: Thrown if ```length``` is not assignable to the type  ```int```.
