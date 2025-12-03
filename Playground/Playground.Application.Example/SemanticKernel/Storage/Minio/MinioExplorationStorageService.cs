using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.Storage.Minio;

public sealed class MinioExplorationStorageService
{
    private readonly IMinioClient _minio;
    private readonly MinioSettings _settings;

    public MinioExplorationStorageService(IMinioClient minio,
                                     IOptions<MinioSettings> options)
    {
        this._minio = minio;
        this._settings = options.Value;
    }

    public async Task UploadAsync(string objectName, Stream data)
    {
        await _minio.PutObjectAsync(
                new PutObjectArgs()
                        .WithBucket(_settings.Bucket)
                        .WithObject(objectName)
                        .WithStreamData(data)
                        .WithObjectSize(data.Length)
            );
    }

    public async Task<bool> BucketExistAsync()
    {
        return await _minio.BucketExistsAsync(
            new BucketExistsArgs().WithBucket(_settings.Bucket)
            );
    }



}
