using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Playground.Application.Example.SemanticKernel.External.WebSearch;


public sealed class TavilySettings
{
    public string ApiKey { get; set; } = string.Empty;

    public string DomainUrl { get; set; } = "https://api.tavily.com";
}

public sealed class TavilyParamsQuery: IWebSearchQuery
{
    public static class SearchDepthArgs
    {
        public const string Basic = "basic";
        public const string Advanced = "advanced";
    }

    public static class TopicArgs
    {
        public const string News = "news";
        public const string General = "general";
    }
    [JsonPropertyName("query")]
    public string Query { get; set; } = "News";

    [JsonPropertyName("search_depth")]
    public string SearchDepth { get; set; } =  SearchDepthArgs.Basic;

    [JsonPropertyName("topic")]
    public string Topic { get; set; } = TopicArgs.General;

    [JsonPropertyName("days")]
    public int Days { get; set; } = 3;

    [JsonPropertyName("max_results")]
    public int MaxResults { get; set; } = 5;

    [JsonPropertyName("include_domains")]
    public List<string>? IncludeDomains { get; set; } = null;

    [JsonPropertyName("exclude_domains")]
    public List<string>? ExcludeDomains { get; set; } = null;

    [JsonPropertyName("include_answer")]
    public bool IncludeAnswer { get; set; } = false;

    [JsonPropertyName("include_raw_content")]
    public bool IncludeRawContent { get; set; } = false;

    [JsonPropertyName("include_images")]
    public bool IncludeImages { get; set; } = false;

    //implement later, after implement TavilyResultJsonConverter
    [JsonIgnore]
    public IDictionary<string, object> AdditionalArguments { get; init; } = new Dictionary<string, object>();
}

public sealed class TavilyResult: IWebSearchResult
{
    [JsonPropertyName("query")]
    public string Query { get; set; } = string.Empty;

    [JsonPropertyName("follow_up_questions")]
    public string? FollowUpQuestions { get; set; }

    [JsonPropertyName("answer")]
    public string? Answer { get; set; }

    [JsonPropertyName("images")]
    public List<byte[]>? Images { get; set; }

    [JsonPropertyName("results")]
    public ICollection<TavilyResultItem>? Results { get; set; }
}


public sealed class TavilyResultItem
{
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("score")]
    public float Score { get; set; }

    [JsonPropertyName("published_date")]
    public DateTimeOffset PublishedDate { get; set; }

    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [JsonPropertyName("raw_content")]
    public string? RawContent { get; set; }
}



public sealed class TavilySearchClient: ISearchClient<TavilyParamsQuery, TavilyResult>
{
    public static class TavilyPath
    {
        public const string Search = "search";
    }

    private readonly IHttpClientFactory _clientFactory;
    private readonly TavilySettings _settings;

    public TavilySearchClient(IOptions<TavilySettings> settingOptions, IHttpClientFactory clientFactory)
    {
        this._clientFactory = clientFactory;
        this._settings = settingOptions.Value!;
    }

    public Task<TavilyResult> SearchAsync(TavilyParamsQuery query, ulong timeout = 100)
    {
        static void SetApiKey(HttpRequestMessage _requestMessage, string bearerKey)
        {
            _requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerKey);
        }

        static void SetContent(HttpRequestMessage _requestMessge, TavilyParamsQuery _query)
        {
            _requestMessge.Content = new StringContent(JsonSerializer.Serialize(_query), Encoding.UTF8);
            _requestMessge.Content!.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        }


        // create httpclient with address = taivily-domain + "search"

        using var httpClient = _clientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(Path.Combine(this._settings.DomainUrl, TavilyPath.Search));

    }
}
