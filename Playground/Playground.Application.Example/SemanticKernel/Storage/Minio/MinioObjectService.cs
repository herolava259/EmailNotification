using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Playground.Application.Example.SemanticKernel.Storage.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.Storage.Minio;

public sealed class MinioObjectService : IObjectStorageService
{
    private readonly IMinioClient _client;
    private readonly MinioSettings _settings;

    public MinioObjectService(IMinioClient client, IOptions<MinioSettings> options)
    {
        this._client = client;
        this._settings = options.Value;
    }
    public Task<(string, byte[])> DownloadAsync(string objectName)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistBucketAsync(string bucketName)
    {
        var args = new BucketExistsArgs()
                            .WithBucket(bucketName);

        return await _client.BucketExistsAsync(args).ConfigureAwait(false);
    }

    public Task<bool> ExistObjectAsync(string objectName)
    {
        throw new NotImplementedException();
    }

    public async Task MakeBucketAsync(string buckeName)
    {
        var args = new MakeBucketArgs()
                        .WithBucket(buckeName)
                        .WithLocation(_settings.DefaultLocation)
                        ;
        await _client.MakeBucketAsync(args);

    }

    public Task<bool> UploadAsync(string objectName, Stream data)
    {
        throw new NotImplementedException();
    }
}
