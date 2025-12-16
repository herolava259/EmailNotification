using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.Storage.Abstractions;

public interface IDataModel { }

public interface ISchema { }

public interface IDDLInteractor<TSchema>
    where TSchema: ISchema
{
    Task DeclareSchemaAsync(TSchema schema, string schemaName);

    Task DropSchemaAsync(string schemaName);

    Task ExistSchemaAsync(string schemaName);

    Task RenameSchemaAsync(string schemaName, string newName);

    Task CreatePartitionAsync(string partitionName);


}
