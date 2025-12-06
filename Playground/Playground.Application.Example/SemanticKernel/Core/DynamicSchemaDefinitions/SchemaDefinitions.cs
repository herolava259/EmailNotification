using Microsoft.Extensions.VectorData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.Core.DynamicSchemaDefinitions;

public static class SchemaDefinitions
{
    public static readonly VectorStoreCollectionDefinition BookSectionDefinition = new VectorStoreCollectionDefinition
    {
        Properties = new List<VectorStoreProperty>
        {
            new VectorStoreKeyProperty("Id", typeof(string)),
            new VectorStoreDataProperty("Title", typeof(string)),
            new VectorStoreDataProperty("NoOfSection", typeof(ulong)),

            new VectorStoreDataProperty("Summary", typeof(string)),
            new VectorStoreVectorProperty("SummaryEmbedding", typeof(float), dimensions: 128)
            {DistanceFunction = DistanceFunction.CosineSimilarity, IndexKind = IndexKind.Hnsw },

            new VectorStoreDataProperty("Content", typeof(string)),
            new VectorStoreVectorProperty("ContentEmbedding", typeof(float), dimensions: 128)
            {DistanceFunction = DistanceFunction.CosineDistance, IndexKind = IndexKind.Hnsw },

        }
    };
}
