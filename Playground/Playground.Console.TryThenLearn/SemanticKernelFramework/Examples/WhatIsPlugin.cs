using Microsoft.SemanticKernel;
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

            kernelBuilder.Plugins.AddFromType<Pirate>();


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
            TStory Visit();


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

        public enum CrewMemberRole
        {
            Captain, // ex: Luffy
            FirstMate, // ex: Zoro
            Pilot, // ex: nami
            ShipWright, // ex, franky
            ChiefSteward, // ex, sanji
            Sailor, // robin, chopper
            Helmsman, // ex: franky, zoro, sanji
            Marksman,// ussop

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
            Story IPirate<Story>.Visit()
            {
                return new Story("A long long story....");
            }

            public ICollection<string> Skills { get; set; }

            public string PersonalityTrait { get; set; }

            public CrewMemberRole Role { get; set; }

            public Story Biography { get; set; }

            public PirateInstinctType Instinct { get; set; } = PirateInstinctType.Neutral;

            public ICollection<string> Hobbies { get; set; } 
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
