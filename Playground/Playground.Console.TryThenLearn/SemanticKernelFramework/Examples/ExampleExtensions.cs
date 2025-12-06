using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Console.TryThenLearn.SemanticKernelFramework.Examples;


public static class ExampleExtensions
{
    public static IConfiguration GetConfiguration(this IExample example)
    {
        return new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();
    }

    public static string GetOpenAIApiKey(this IExample example)
    {
        return example.GetConfiguration()!["OpenAIKey"]!;
    }

    public static (string, string) GetDefaultOpenAISettings(this IExample example)
    {
        var config = example.GetConfiguration()!;


        return (config["OpenAIKey"]!, config["ModelName"]!);
    }

    public static Kernel CreateKernelWithOpenAI(this IExample example)
    {
        var (modelId, apiKey) = example.GetDefaultOpenAISettings();

        IKernelBuilder builder = Kernel.CreateBuilder();

        builder.AddOpenAIChatClient(modelId, apiKey);

        return builder.Build();

    }

}
