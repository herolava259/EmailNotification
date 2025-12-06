
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.Extensions.Logging;
using System.Text.Json.Nodes;
using System.Net;
using Playground.Application.Example.SemanticKernel.External.WebSearch.Settings;


namespace Playground.Application.Example.SemanticKernel.External.WebSearch;

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
    private readonly ILogger<TavilySearchClient> _logger;
    private readonly TavilySettings _settings;

    public TavilySearchClient(IOptions<TavilySettings> settingOptions, IHttpClientFactory clientFactory, ILogger<TavilySearchClient> logger)
    {
        this._clientFactory = clientFactory;
        this._logger = logger;
        this._settings = settingOptions.Value!;
    }

    public async Task<TavilyResult> SearchAsync(TavilyParamsQuery query, ulong timeout = 100)
    {

        static void SetContent(HttpRequestMessage _requestMessge, TavilyParamsQuery _query)
        {
            _requestMessge.Content = new StringContent(JsonSerializer.Serialize(_query), Encoding.UTF8);
            _requestMessge.Content!.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        }


        // create httpclient with address = taivily-domain + "search"

        using var httpClient = _clientFactory.CreateClient("tavily");

        var request = new HttpRequestMessage(HttpMethod.Post, Path.Combine(_settings.DomainUrl, TavilyPath.Search));

        SetContent(request, query);

        // timeout for long request 
        using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds((double)timeout));
        var resp = await httpClient.SendAsync(request, cts.Token);

        if (resp.StatusCode == System.Net.HttpStatusCode.OK)
        {
            _logger.LogInformation("Search with with Tavily is succeed!");
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(await resp.Content.ReadAsStringAsync()) 
                                ?? throw new NullReferenceException("No content response");

            

            if(!jsonObj.TryGetPropertyValue("results", out _))
            {
                jsonObj["results"] = new JsonArray();
            }
            return jsonObj.Deserialize<TavilyResult>()!;
        }
        else if (resp.StatusCode == HttpStatusCode.TooManyRequests)
        {
            var errorDetail = "Too Many requests!!!";
            try
            {
                errorDetail = JsonSerializer.Deserialize<JsonObject>(await resp.Content.ReadAsStringAsync())!["detail"]!["error"]!.ToJsonString();
            }
            catch(Exception ex)
            {
                _logger.LogError(message:"Parse Error:detail failed", exception:ex);
            }

            _logger.LogWarning("Too many requests to tavily search!!!. Error: {@errorDetail}", errorDetail);

            throw new HttpRequestException(HttpRequestError.SecureConnectionError, message: errorDetail, statusCode: HttpStatusCode.TooManyRequests);
        }
        else if (resp.StatusCode == HttpStatusCode.Unauthorized)
        {

            _logger.LogWarning("ApiKey is not corrected. HttpStatusCode: Unauthorized-401");

            throw new HttpRequestException(HttpRequestError.SecureConnectionError, statusCode: HttpStatusCode.Unauthorized);
        }
        else
            throw new HttpRequestException(HttpRequestError.Unknown);
    }
}
