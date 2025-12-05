// See https://aka.ms/new-console-template for more information
using Playground.Console.TryThenLearn.OpenAI;

Console.WriteLine("Hello, World!");


// Test Chat Example 

var chatLoop = new ChatAppExample();

chatLoop.Setup();

await  chatLoop.RunAsync();