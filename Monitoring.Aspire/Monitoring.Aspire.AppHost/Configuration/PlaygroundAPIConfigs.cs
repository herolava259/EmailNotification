using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring.Aspire.AppHost.Configuration;

public static class PlaygroundAPIConfigs
{

    public static IResourceBuilder<ProjectResource> ConfigMonitoringPlaygroundAPI(this  IDistributedApplicationBuilder builder)
    {
        var db = builder.AddPostgres("postgres")
                        .AddDatabase("distributed-locking");

        var redis = builder.AddRedis("redis");


        return builder.AddProject<Projects.Playground_API>("playground-api")
                                   .WithHttpEndpoint(5001, name: "public")
                                   .WithReference(db)
                                   .WithReference(redis)
                                   .WaitFor(db)
                                   .WaitFor(redis);

    }
}
