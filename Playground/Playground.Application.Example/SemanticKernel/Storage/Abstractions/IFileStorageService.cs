using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.Storage.Abstractions;

public interface IFileStorageService
{
    Task UploadAsync(string filePath, Stream data);

    Task<byte[]> DownloadAsync(string filePath);


}
