using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Console.TryThenLearn.SemanticKernelFramework.Examples;

public class HowToCreateKernel: IExample
{
    public async Task RunExample()
    {
        var (apiKey, modelName) = this.GetDefaultOpenAISettings();
        var kernel = Kernel.CreateBuilder().AddOpenAIChatClient(modelId: modelName, apiKey: apiKey).Build();

        while(true)
        {
            System.Console.Write("Prompt Input: ");
            var input = System.Console.ReadLine();

            if (input == "exit")
                break;
            if (string.IsNullOrEmpty(input) || input.Trim() == string.Empty) 
                continue;

            if(input.Contains("topic"))
            {
                KernelArguments aargs = new() { { "topic", "travel" } };

                System.Console.WriteLine(await kernel.InvokePromptAsync("What place is the {{$topic}}", aargs));

                System.Console.WriteLine();

                // streaming read
                await foreach (var update in kernel.InvokePromptStreamingAsync("What place is the {{$topic}}? Provide a detailed explanation.", aargs))
                {
                    System.Console.Write(update);
                }
                continue;
            }

            // example with temperature, max-tokens

            KernelArguments eargs = new(new OpenAIPromptExecutionSettings { MaxTokens = 250, Temperature = 0.5 }) { { "topic", "tech" } };

            System.Console.WriteLine(await kernel.InvokePromptAsync(input + "\n Tell me more about {{$topic}}", eargs));


        }
    }
}
