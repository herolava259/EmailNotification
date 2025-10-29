using A2A;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.ServerSentEvents;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.A2A.Client;

public sealed class EchoClient
{

    public async Task RunExample()
    {
        A2ACardResolver cardResolver = new(new Uri("http://localhost:500/"));

        AgentCard echoAgentCard = await cardResolver.GetAgentCardAsync();

        Console.WriteLine($"Connected to agent: {echoAgentCard.Name}");
        Console.WriteLine($"Description: {echoAgentCard.Description}");
        Console.WriteLine($"Streaming support: {echoAgentCard.Capabilities?.Streaming}");

        var agentClient = new A2AClient(new Uri(echoAgentCard.Url));

        var userMessage = new AgentMessage
        {
            Role = MessageRole.User,
            MessageId = Guid.NewGuid().ToString(),
            Parts = [new TextPart { Text = "Hello from the A2A client!"}]
        };

        Console.WriteLine("\n=== Non-Streaming Communication ===");
        AgentMessage agentResponse = (AgentMessage)await agentClient.SendMessageAsync(new MessageSendParams { Message = userMessage });
        Console.WriteLine($"Received response: {((TextPart)agentResponse.Parts[0]).Text}");


        await foreach(SseItem<A2AEvent> sseItem in agentClient.SendMessageStreamingAsync(new MessageSendParams { Message = userMessage}))
        {
            AgentMessage streamingResponse = (AgentMessage)sseItem.Data;
            Console.WriteLine($"Received streaming chunk: {((TextPart)streamingResponse.Parts[0]).Text}");
        }
    }
}
