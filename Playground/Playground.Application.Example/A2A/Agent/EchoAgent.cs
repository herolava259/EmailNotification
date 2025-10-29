using A2A;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.A2A.Agent;

public class EchoAgent : AgentBase
{
    public override Task<bool> AcceptMessage(AgentMessage message, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override Task<AgentResponse> ReplyImediatelyMessage(AgentMessage message, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    protected override Task<AgentCard> GetAgentCardAsync(string agentUrl, CancellationToken cancellationToken)
    {
        return Task.FromResult(new AgentCard()
        {
            Name = "Echo Agent",
            Description = "An agent that will echo every message it receives.",
            Url = agentUrl,
            Version = "1.0.0",
            DefaultInputModes = ["text"],
            DefaultOutputModes = ["text"],
            Capabilities = new AgentCapabilities() { Streaming = true },
            Skills = [],
        });
    }

    protected override Task<A2AResponse> ProceedMessageAsync(MessageSendParams msgSendParams, CancellationToken ct)
    {
        // Get incoming message text
        string request = msgSendParams.Message.Parts.OfType<TextPart>().First().Text;

        return Task.FromResult(new A2AResponseMessage
        {
            Role = MessageRole.Agent,
            MessageId = Guid.NewGuid().ToString(),
            ContextId = msgSendParams.Message.ContextId,
            Parts = [new TextPart() { Text = $"Echo: {request}" }]
        } as A2AResponse);
    }
}
