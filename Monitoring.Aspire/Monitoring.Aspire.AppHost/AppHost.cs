var builder = DistributedApplication.CreateBuilder(args);

//string tavilyDomain = "https://api.tavily.com";

//// add external service for service discovery

//var tavilyApi = builder.AddExternalService("tavily-search", new Uri(tavilyDomain));


var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.Monitoring_Aspire_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.Monitoring_Aspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddProject<Projects.IAM_Web>("iam-web");

builder.AddProject<Projects.IAM_Authentication_API>("iam-authentication-api");

builder.AddProject<Projects.IAM_Authorization_API>("iam-authorization-api");

builder.AddProject<Projects.Playground_API>("playground-api");
       //.WithReference(tavilyApi);

builder.Build().Run();
