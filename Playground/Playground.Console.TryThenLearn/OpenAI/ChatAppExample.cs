

using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IChatClient = Microsoft.Extensions.AI.IChatClient;

namespace Playground.Console.TryThenLearn.OpenAI
{
    internal class ChatAppExample
    {
        private readonly List<ChatMessage> _chatHistory = [new ChatMessage(ChatRole.System ,"""
            You are a friendly hiking enthusiast who helps people discover fun hikes in their area.
            You introduce yourself when first saying hello.
            When helping people out, you always ask them for this information
            to inform the hiking recommendation you provide:

            1. The location where they would like to hike
            2. What hiking intensity they are looking for

            You will then provide three suggestions for nearby hikes that vary in length
            after you get that information. You will also share an interesting fact about
            the local nature on the hikes when making a recommendation. At the end of your
            response, ask if there is anything else you can help with.
        """)];

        private IChatClient _chatClient;

        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .AddUserSecrets<Program>()
                                    .Build();

            string modelName = config["ModelName"]!;

            string key = config["OpenAIKey"];

            this._chatClient = new OpenAIClient(key)
                                        .GetChatClient(modelName).AsIChatClient();
        }
        public async Task RunAsync()
        {
            
            while(true)
            {
                System.Console.WriteLine("Your prompt");

                string? userPrompt = System.Console.ReadLine();

                _chatHistory.Add(new ChatMessage(ChatRole.User, userPrompt));

                System.Console.WriteLine("AI Response:");
                string response = "";
                await foreach (ChatResponseUpdate item in
                    _chatClient.GetStreamingResponseAsync(_chatHistory))
                {
                    System.Console.Write(item.Text);
                    response += item.Text;
                }
                _chatHistory.Add(new ChatMessage(ChatRole.Assistant, response));
                System.Console.WriteLine();
            }

        }
    }
}
