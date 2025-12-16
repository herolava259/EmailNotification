using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Playground.Console.TryThenLearn.SemanticKernelFramework.Examples
{
    internal sealed class WhatIsPlugin : IExample
    {
        public async Task RunExample()
        {
            // create kernel 

            IKernelBuilder kernelBuilder = Kernel.CreateBuilder();

            var (modelId, apiKey) = this.GetDefaultOpenAISettings();

            kernelBuilder.AddOpenAIChatClient(modelId: modelId, apiKey: apiKey)
                         ;

            kernelBuilder.Plugins.AddFromType<Arc>();

            var luffy = new Pirate
            {
                Name = "Luffy",
                Abilities = ["Third-One", "Third-Two", "Third-Three"],
                Instinct =PirateInstinctType.Good,
                Role = CrewMemberRole.Captain,
            };


            var chopper = new Pirate
            {
                Name = "Luffy",
                Abilities = ["heal", "Third-Two", "Third-Three"],
                Instinct = PirateInstinctType.Good,
                Role = CrewMemberRole.Sailor,
            };

            kernelBuilder.Plugins.AddFromObject(luffy, pluginName: "luffy");

            kernelBuilder.Plugins.AddFromObject(chopper, pluginName: "chopper");

            KernelFunction getTime = KernelFunctionFactory.CreateFromMethod((int days) => AswerTime(days), functionName: nameof(AswerTime),
                                                                            description: "Answer about time now or number of days ago or next days");

            kernelBuilder.Plugins.AddFromFunctions("Utilities", [getTime]);

            Kernel kernel = kernelBuilder.Build();

            var questOne = "What time is it?";
            OpenAIPromptExecutionSettings settings = new() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() };
            System.Console.WriteLine(questOne);
            System.Console.WriteLine($"Answer: {await kernel.InvokePromptAsync(questOne, new(settings))}" );

            var questTwo = "Introduce about Luffy";

            System.Console.WriteLine(questTwo);
            System.Console.WriteLine($"Answer: {await kernel.InvokePromptAsync(questOne)}");


            var questThree = "Luffy Tell me about story in Wano?";

            
            System.Console.WriteLine(questThree);
            System.Console.WriteLine($"Answer: {await kernel.InvokePromptAsync(questThree)}");


        }

        [KernelFunction]
        [Description("Answer about time now or number of days ago or next days")]
        public string AswerTime(int days)
        {
            return DateTime.UtcNow.AddDays(days).ToString("yyyy/MM/dd");
        }


        public interface IStory
        {
            public string Content { get; }
        }

        public class Ship
        {
            public int Width { get; set; }

            public int Height { get; set; }

            public int Depth { get; set; }

            public string Name { get; set; }
        }

        public interface IPirateCrew
        {
            public ICollection<IPirate<Story>> Members { get;}
        }

        public interface IIsland
        {
            void Accept(IPirate<Story> pirate);

            void Accept(IPirateCrew crew);
        }

        public interface IPirate<TStory>
            where TStory : IStory
        {
            TStory Visit(string island);


        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum PirateInstinctType
        {
            [Description("A good pirtate type")]
            Good,

            [Description("A bad guy or lady")]
            Bad,

            [Description("Neither wickness or good")]
            Neutral,
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum CrewMemberRole
        {
            [Description($"This is role {nameof(Captain)} in the ship")]
            Captain, // ex: Luffy

            [Description($"This is role {nameof(FirstMate)}  in the ship")]
            FirstMate, // ex: Zoro

            [Description($"This is {nameof(Pilot)} in the ship")]
            Pilot, // ex: nami

            [Description($"This is {nameof(ShipWright)} in the ship")]
            ShipWright, // ex, franky

            [Description($"This is {nameof(ChiefSteward)} in the ship")]
            ChiefSteward, // ex, sanji

            [Description($"This is {nameof(Sailor)} in the ship")]
            Sailor, // robin, chopper, brook

            [Description($"This is {nameof(Helmsman)} in the ship")]
            Helmsman, // ex: franky, zoro, sanji

            [Description($"This is {nameof(Marksman)} in the ship")]
            Marksman,// ussop

            [Description($"This is {nameof(MasterOfAll)} in the ship")]
            MasterOfAll, // mihawk
        }


        public class Story(string content) : IStory
        { 
            public string Content => content;

            public string Opening { get; set; }

            public string Body { get; set; }

            public string Ending { get; set; }
        }

        public class Pirate : IPirate<Story>
        {
            [KernelFunction]
            [Description("The pirate will tell you a story!!!")]
            Story IPirate<Story>.Visit(string island)
            {
                return new Story($"A long long story in {island}....");
            }


            public string Name { get; set; }

            public ICollection<string> Abilities { get; set; }

            public string PersonalityTrait { get; set; }

            public CrewMemberRole Role { get; set; }

            public Story Biography { get; set; }

            public PirateInstinctType Instinct { get; set; } = PirateInstinctType.Neutral;

            public ICollection<string> Hobbies { get; set; } = [];



            [KernelFunction]
            [Description("Introduction of the pirate")]
            public string Introduction()
            {
                return $@"
                    Name: {Name}.
                    PersonalityTrait {PersonalityTrait}
                    Instinct: {Instinct.ToString()}
";
            }

        }

        public sealed class Arc
        {
            [KernelFunction]
            [Description("Make story for a crew of priate while they are visiting a wano island.")]
            public Story MakeWanoArcStory()
            {
                return new Story("a story...");
            }
        }
    }
}
