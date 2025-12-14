using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.External.WebSearch;

#region models

public sealed class ArxivSearchQuery: IWebSearchQuery
{
    [JsonPropertyName("query")]
    public string Query { get; set; }

    [JsonPropertyName("id_list")]
    public List<string> Ids { get; set; }

    [JsonPropertyName("max_results")]
    public int? MaxResults { get; set; }

    [JsonPropertyName("sort_by")]
    public string SortBy { get; set; } = SortCriterion.Relevance;

    [JsonPropertyName("sort_order")]
    public string SortOrderBy { get; set; } = SortOrder.Ascending;
}


public sealed class ArxivSearchResult: IWebSearchResult
{
    [JsonPropertyName("entry_id")]
    public string EntryId { get; set; }

    [JsonPropertyName("updated")]
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.MinValue;

    [JsonPropertyName("published")]
    public DateTimeOffset PublishedDate { get; set; } = DateTimeOffset.MinValue;

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("authors")]
    public List<string> Authors { get; set; }

    [JsonPropertyName("summary")]
    public string Summary { get; set; }

    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    [JsonPropertyName("journal_ref")]
    public string? JournalReference { get; set; }

    [JsonPropertyName("doi")]
    public string? Doi {  get; set; }

    [JsonPropertyName("primary_category")]
    public string PrimaryCategory { get; set; } = "Computer Science";

    [JsonPropertyName("categories")]
    public List<string> Categories { get; set; } = new();

    [JsonPropertyName("links")]
    public List<string> Links { get; set; } = new();


    [JsonPropertyName("pdf_url")]
    public string PdfUrl { get; set; } = string.Empty;


    public sealed class Author
    {
        public string Name { get; set; }


    }

    public sealed class Link
    {
        public string Href { get; set; }

        public string? Title { get; set; }

        public string Rel { get; set; }

        public string ContentType { get; set; }
    }

    public class MissingFieldException: Exception
    {
        public MissingFieldException(string missingField): base("Entry from arXiv missing required info")
        {
            
        }
    }

}

public static class SortCriterion
{
    public const string Relevance = "relevance";
    public const string LastUpdateData = "lastUpdatedDate";
    public const string SubmittedDate = "submittedDate";
}

public static class SortOrder
{
    public const string Ascending = "ascending";
    public const string Descending = "descending";
}


#endregion

public sealed class ArxivSearchClient(HttpClient httpClient)
{

}
