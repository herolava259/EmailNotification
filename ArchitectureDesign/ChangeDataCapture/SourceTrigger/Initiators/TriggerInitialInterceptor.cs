using Grpc.Core;
using Grpc.Core.Interceptors;
using MassTransit.Initializers;
using Microsoft.Extensions.Configuration;


namespace ChangeDataCapture.SourceTrigger.Initiators;

public sealed class TriggerInitialServerInterceptor(ITriggerContext _triggerContext): Interceptor
{
    private void InitializeContext(string correlationId, string agentId)
    {
        _triggerContext.Initialize(TriggerSourceType.OtherService, TriggerType.Grpc, agentId, correlationId);
    }

    private void OnBeforeHandling(ServerCallContext context)
    {
        var corrId = context.RequestHeaders.GetValue(TriggerConstantKeys.CorrelationIdKey.ToLower())
                            ?? Guid.NewGuid().ToString();

        var serviceId = context.RequestHeaders.GetValue(TriggerConstantKeys.ServiceIdKey.ToLower())
                            ?? Guid.NewGuid().ToString();

        InitializeContext(corrId, serviceId);

        context.ResponseTrailers.Add(TriggerConstantKeys.CorrelationIdKey, corrId);
        context.ResponseTrailers.Add(TriggerConstantKeys.ServiceIdKey, serviceId);
    }

    public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
    {
        OnBeforeHandling(context);

        return base.UnaryServerHandler(request, context, continuation);
    }

    public override Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, ServerCallContext context, ClientStreamingServerMethod<TRequest, TResponse> continuation)
    {
        OnBeforeHandling(context);

        return base.ClientStreamingServerHandler(requestStream, context, continuation);
    }

    public override Task ServerStreamingServerHandler<TRequest, TResponse>(TRequest request, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, ServerStreamingServerMethod<TRequest, TResponse> continuation)
    {
        OnBeforeHandling(context);
        return base.ServerStreamingServerHandler(request, responseStream, context, continuation);
    }

    public override Task DuplexStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, DuplexStreamingServerMethod<TRequest, TResponse> continuation)
    {
        OnBeforeHandling(context);

        return base.DuplexStreamingServerHandler(requestStream, responseStream, context, continuation);
    }
}

public class TriggerInitialClientInterceptor(ITriggerContext _triggerContext,
                                                    IConfiguration _configuration) : Interceptor
{
    protected ClientInterceptorContext<TRequest, TResponse> AttachTriggerContext<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context)
        where TRequest: class
        where TResponse: class
    {
        var headers = context.Options.Headers ?? new Metadata();


        headers.Add(TriggerConstantKeys.CorrelationIdKey.ToLower(), _triggerContext.CorrelationId ?? Guid.NewGuid().ToString());
        headers.Add(TriggerConstantKeys.ServiceIdKey,
                (string)(_configuration.GetValue(typeof(string), "ServiceId")
                            ?? string.Empty));


        var newOptions = context.Options.WithHeaders(headers);

        var newContext = new ClientInterceptorContext<TRequest, TResponse>(
                                context.Method,
                                context.Host,
                                newOptions);

        return newContext;
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        return base.AsyncUnaryCall(request, AttachTriggerContext(context), continuation);
    }


    public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncServerStreamingCallContinuation<TRequest, TResponse> continuation)
    {
        
        return base.AsyncServerStreamingCall(request, AttachTriggerContext(context), continuation);
    }

    public override AsyncClientStreamingCall<TRequest, TResponse> AsyncClientStreamingCall<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context, AsyncClientStreamingCallContinuation<TRequest, TResponse> continuation)
    {
        return base.AsyncClientStreamingCall(AttachTriggerContext(context), continuation);
    }

    public override AsyncDuplexStreamingCall<TRequest, TResponse> AsyncDuplexStreamingCall<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context, AsyncDuplexStreamingCallContinuation<TRequest, TResponse> continuation)
    {
        return base.AsyncDuplexStreamingCall(AttachTriggerContext(context), continuation);
    }

    public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        return base.BlockingUnaryCall(request, AttachTriggerContext(context), continuation);
    }
}
