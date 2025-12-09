using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.External.WebSearch;

#region models


public sealed class ArxivSearchResult: IWebSearchQuery
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

}

public static class SortCriterion
{
    public const string Relevance = "relevance";
}

#endregion

public sealed class ArxivSearchClient(HttpClient httpClient)
{

}
