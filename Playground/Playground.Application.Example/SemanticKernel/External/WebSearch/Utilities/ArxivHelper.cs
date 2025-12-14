using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Playground.Application.Example.SemanticKernel.External.WebSearch.Utilities;

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

    // example for entryID do know it's format: "https://arxiv.org/abs/2107.05580v1"
    public static string GetShortId(this ArxivSearchResult result)
        => result.EntryId.Split("arxiv.org/abs/", StringSplitOptions.TrimEntries).Last();

    public static string GetDefaultFileName(this ArxivSearchResult result, string extension="pdf")
    {
        var stdTitle = string.IsNullOrEmpty(result.Title) ? "UNTITLED" : result.Title;

        return string.Join('.', result.GetShortId().Replace("/", "_"), Regex.Replace(stdTitle, @"[^\w]", "_", RegexOptions.NonBacktracking | RegexOptions.Compiled, TimeSpan.FromMilliseconds(150)), extension);
    }


    public static async Task<Stream> DownloadArxivPdf(this ArxivSearchResult result, HttpClient httpClient, string downloadDomain = "export.arxiv.org")
    {

        var pdfDomain = new Uri(result.PdfUrl);

        var pdfBuilder = new UriBuilder(pdfDomain);

        pdfBuilder.Host = downloadDomain;

        pdfDomain = pdfBuilder.Uri;

        var request = new HttpRequestMessage(HttpMethod.Get, pdfDomain);

        using var response = await httpClient.SendAsync(request);
        
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStreamAsync();
    }


    public static async Task<Stream> DownloadSource(this ArxivSearchResult result, HttpClient httpClient, string downloadDomain = "export.arxiv.org")
    {
        var pdfDomain = new Uri(result.PdfUrl);

        var pdfBuilder = new UriBuilder(pdfDomain);

        pdfBuilder.Host = downloadDomain;

        pdfDomain = pdfBuilder.Uri;

        var request = new HttpRequestMessage(HttpMethod.Get, pdfDomain);

        using var response = await httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStreamAsync();
    }
}
