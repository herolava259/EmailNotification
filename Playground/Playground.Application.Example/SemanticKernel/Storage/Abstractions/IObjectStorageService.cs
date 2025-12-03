using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.Storage.Abstractions;

public interface IObjectStorageService
{

    Task<bool> UploadAsync(string objectName, Stream data);

    Task<(string, byte[])> DownloadAsync(string objectName);


    Task<bool> MakeBucketAsync(string buckeName);

    Task<bool> ExistObjectAsync(string objectName);


    Task<bool> ExistBucketAsync(string buckeName);
}
