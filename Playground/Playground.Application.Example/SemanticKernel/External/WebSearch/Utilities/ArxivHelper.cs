using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.External.WebSearch.Utilities
{
    internal static class ArxivHelper
    {
        public sealed class ArxivDateTimeConverter : JsonConverter<DateTimeOffset>
        {
            private readonly string _format = ""; // TODO Config later
            public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }
        }
    }
}
