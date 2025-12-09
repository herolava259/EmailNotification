using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground.Application.Example.SemanticKernel.External.WebSearch.Settings;
using Polly;
using Polly.CircuitBreaker;
using Polly.Hedging;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.RateLimiting;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.External.WebSearch.DependencyInjections;

public static class WebSearchDependencyInjection
{
    public static IServiceCollection AddSettingOptions(IServiceCollection services, IConfiguration configuration)
        => services.Configure<TavilySettings>(configuration.GetSection("Tavily")); // tavily config
    public static IServiceCollection AddHttpClientForTavilySearch(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = new TavilySettings();

        configuration.GetSection("Tavily").Bind(settings);

        services.AddHttpClient("tavily-client", httpClient =>
        {
            httpClient.BaseAddress = new Uri(settings.DomainUrl);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", settings.ApiKey);
        });

        return services;
    }


    // timeout while request with resilency package

    public static IServiceCollection AddResilenceHttpClientForSearch(this IServiceCollection services
        //// waiting time to responds
        //,double timeoutMilisecond = 1000.0,
        //// for retry
        //uint maxRetryAttempts = 5,
        //double delayTime = 300,
        //// rate-limiting from client, no send many request within a specific time
        //uint permitLimit = 100,
        //uint segmentPerWindow = 4,
        //double window = 1,
        //// circuit breaker
        //double failureRatio = 0.5,
        //uint minimumThroughput = 10,
        //double breakDuration = 15
        )
    {
        // arxiv
        services.AddHttpClient<ArxivSearchClient>(client => client.BaseAddress = new Uri("https://arxiv.org"))
                .AddStandardResilienceHandler();
                //.AddResilienceHandler("arxiv-resilence-pipeline", pipeline =>
                //{
                //    // Note: UseJitter param meaning that
                //    // if at a specific time, for ex is in 10ms, has 50 reqs waiting to 10ms further they will send requests, in the setting UseJitter=false
                //    // then At the same time 50 requests will be sent to the service, It's overwhemling/ overload the service
                //    // So that one use UseJitter = True, it add a little randomness time delta for next retry time
                //    // (next_retry_time_of_a_req = next_retry_time + small_time_delta(randomness for each req))
                //    // to times of all retry request in the 
                //    // circle are different, prevent many requests be sent concurently
                //    // benefit: stretch throughput distribution, prevent being suspected of being a DOS attack

                //    pipeline.AddRetry(new RetryStrategyOptions<HttpResponseMessage>
                //    {
                //        ShouldHandle = new PredicateBuilder<HttpResponseMessage>().Handle<Exception>(),
                //        Delay = TimeSpan.FromMilliseconds(delayTime),
                //        MaxRetryAttempts = (int)maxRetryAttempts,
                //        BackoffType = DelayBackoffType.Exponential,
                //        UseJitter = true
                //    });
                //    pipeline.AddTimeout(TimeSpan.FromMilliseconds(timeoutMilisecond));

                //    pipeline.AddRateLimiter(new SlidingWindowRateLimiter(new()
                //    {
                //        PermitLimit = (int)permitLimit,
                //        SegmentsPerWindow = (int)segmentPerWindow,
                //        Window = TimeSpan.FromSeconds(window)
                //    }));

                //    // fallback if all is failure
                //    pipeline.AddFallback(new Polly.Fallback.FallbackStrategyOptions<HttpResponseMessage>
                //    {

                //        FallbackAction = _ => Outcome.FromResultAsValueTask<HttpResponseMessage>(new HttpResponseMessage(System.Net.HttpStatusCode.NotFound))
                //    });

                //    // circuit breaker,
                //    // prevent retry to a http-server if many failure request to is too high
                //    // include 3 states
                //    // 1 Open: If our http clients to a service have failure requests ratio too high (default > 0.5) within specified intervals
                //    // ,resilency-pipeline with circuit-breaket will prevent send requests to the service by way: 
                //    // switch to open state. In this state, future request to the service will be prevented
                //    // 2 Half-Open state: After specific timeout period or inverval mentioned above, the circuit-breaker swith to this state.
                //    // Meaning: It allows a limited number of test requests to go through the service. If the test request succeed, it think / or consider that
                //    // the service is recovered and available for accept requests. So that, It (circuit-breaker) switch to closed state meaning that 
                //    // able to send requests to the external service
                //    // if fail, it turn back the open state.
                //    // The circuit breaker help the external service has time to recover after they crashes.


                //    pipeline.AddCircuitBreaker(new CircuitBreakerStrategyOptions<HttpResponseMessage>
                //    {
                //        FailureRatio = failureRatio,
                //        MinimumThroughput = (int)minimumThroughput,
                //        SamplingDuration = TimeSpan.FromSeconds(breakDuration),
                //        BreakDuration = TimeSpan.FromSeconds(15)
                //    });

                //    // hedging
                //    pipeline.AddHedging(new HedgingStrategyOptions<HttpResponseMessage>
                //    {
                //        MaxHedgedAttempts = 3, // create 3 attempts if the first wait too long
                //        DelayGenerator = args =>
                //        {
                //            var delay = args.AttemptNumber switch
                //            {
                //                0 or 1 => TimeSpan.Zero,
                //                _ => TimeSpan.FromSeconds(-1)
                //            };
                //            return new ValueTask<TimeSpan>(delay);
                //        }
                //    });


                //});
        return services;
    }
}
