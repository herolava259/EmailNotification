using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.Storage.Minio;

public sealed record MinioConnectionConfiguration(string Endpoint, string AccessKey, string SecretKey)
{
    public static MinioConnectionConfiguration Default
        => new(Endpoint: "http://localhost:9000", AccessKey: "minioadmin", SecretKey: "minioadmin");
}
