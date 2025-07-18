using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using Microsoft.Extensions.Logging;
using Sail.ComfyUi.Models;
using TupleAsJsonArray;

namespace Sail.ComfyUi;

public class ComfyUiClient
{
    private static readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters =
        {
            new TupleConverterFactory()
        }
    };
    
    private readonly HttpClient httpClient;
    private readonly ILogger<ComfyUiClient>? logger;
    private readonly ClientWebSocket? webSocket;
    private string ClientId { get; } = Guid.NewGuid().ToString();

    public ComfyUiClient(HttpClient httpClient, ILogger<ComfyUiClient>? logger = null)
    {
        if (httpClient.BaseAddress == null)
        {
            throw new ArgumentException(nameof(httpClient));
        }
        
        this.httpClient = httpClient;
        this.logger = logger;
        
    }

    private async Task Connect()
    {
        var uriBuilder = new UriBuilder(httpClient.BaseAddress ?? throw new InvalidOperationException());
        if (uriBuilder.Scheme == "http")
        {
            uriBuilder.Scheme = "ws";
        }
        else if(uriBuilder.Scheme == "https")
        {
            uriBuilder.Scheme = "wss";
        }

        await webSocket!.ConnectAsync(uriBuilder.Uri, httpClient, CancellationToken.None);
    }

    private async Task<UploadResponse> UploadCommon(string url, string name, Stream stream)
    {
        var multipart = new MultipartFormDataContent();
        multipart.Add(new StreamContent(stream), "image", name);
        multipart.Add(new StringContent("true"), "overwrite");
        multipart.Add(new StringContent("input"), "type");
        if (false)
        {
            string subfolder = "";
            multipart.Add(new StringContent(subfolder), "subfolder");
            string format = "";
            multipart.Add(new StringContent(format), "format");
        }

        var response = await this.httpClient.PostAsync(url, multipart);
        response.EnsureSuccessStatusCode();
        var result = await JsonSerializer.DeserializeAsync<UploadResponse>(await response.Content.ReadAsStreamAsync(), options)
                     ?? throw new JsonException();
        return result;
    }

    public async Task<UploadResponse> UploadImage(string name, Stream stream)
    {
        return await UploadCommon("upload/image", name, stream);
    }

    public async Task<UploadResponse> UploadMask(string name, Stream stream)
    {
        return await UploadCommon("upload/mask", name, stream);
    }

    public async Task<PromptResponse> Prompt(PromptRequest request)
    {
        string url = "/prompt";
        /*if (request.ClientId == null)
        {
            request = request with { ClientId = this.ClientId };
        }*/
        
        var jsonContent = JsonContent.Create(request, options: options);
        var response = await this.httpClient.PostAsync(url, jsonContent);
        response.EnsureSuccessStatusCode();
        var result = await JsonSerializer.DeserializeAsync<PromptResponse>(await response.Content.ReadAsStreamAsync(), options)
                     ?? throw new JsonException("null response");
        return result;
    }

    public async Task<QueueResponse> GetQueue()
    {
        string url = "/queue";
        var response = await this.httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var result = await JsonSerializer.DeserializeAsync<QueueResponse>(await response.Content.ReadAsStreamAsync(), options)
                     ?? throw new JsonException("null response");
        return result;
    }
    
    public async Task<HistoryResponse> GetHistory(int maxItems)
    {
        string url = $"/history?max_items={maxItems}";
        var response = await this.httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var result = await JsonSerializer.DeserializeAsync<HistoryResponse>(await response.Content.ReadAsStreamAsync(), options)
                     ?? throw new JsonException("null response");
        return result;
    }
    
    public async Task<Stream> ViewFile(Resource resource)
    {
        string url = $"/view?filename={HttpUtility.UrlEncode(resource.Filename)}&subfolder={HttpUtility.UrlEncode(resource.Subfolder)}&type={HttpUtility.UrlEncode(resource.Type)}";
        var response = await this.httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStreamAsync();
    }
}