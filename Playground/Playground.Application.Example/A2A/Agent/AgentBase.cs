using A2A;

namespace Playground.Application.Example.A2A.Agent;

public record AgentResponse;

public record AgentMessage;

public class A2AResponseMessage : A2AResponse
{
    public A2AResponseMessage() : base("Client Response")
    {
    }

    public string MessageId { get; set; }

    public string? ContextId { get; set; }

    public MessageRole Role { get; set; }

    public TextPart[] Parts { get; set; } = [];
}

public abstract class AgentBase
{
    public void Attach(ITaskManager taskManager)
    {
        taskManager.OnMessageReceived = ProceedMessageAsync;
        taskManager.OnAgentCardQuery = GetAgentCardAsync;
    }
    public abstract Task<bool> AcceptMessage(AgentMessage message, CancellationToken cancellationToken = default);

    public abstract Task<AgentResponse> ReplyImediatelyMessage(AgentMessage message, CancellationToken  cancellationToken = default);

    protected abstract Task<A2AResponse> ProceedMessageAsync(MessageSendParams msgSendParams, CancellationToken ct);

    protected abstract Task<AgentCard> GetAgentCardAsync(string agentUrl, CancellationToken cancellationToken);
}
