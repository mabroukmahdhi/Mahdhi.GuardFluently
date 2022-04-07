// See https://aka.ms/new-console-template for more information
using Mahdhi.GuardFluently.Sample;

SomeService someService = new();
try
{
    someService.DoSomeCall("A", 7);
    Console.WriteLine("No exception...");
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

Console.WriteLine("##########################################");

try
{
    someService.DoSomeCall("My size is over that 10", 7.5f);
    Console.WriteLine("No exception...");
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

Console.WriteLine("##########################################");


try
{
    someService.DoSomeCall("My size is over that 10", 7);
    Console.WriteLine("No exception...");
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

Console.WriteLine("##########################################");
