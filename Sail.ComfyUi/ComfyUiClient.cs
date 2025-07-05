using System.Net.WebSockets;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Sail.ComfyUi;

public class ComfyUiClient
{
    private readonly HttpClient httpClient;
    private readonly ILogger<ComfyUiClient> logger;
    private readonly ClientWebSocket? webSocket;
    private string ClientId { get; } = Guid.NewGuid().ToString();

    public ComfyUiClient(HttpClient httpClient, ILogger<ComfyUiClient> logger)
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
        multipart.Add(new StreamContent(stream), "file", name);
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
        var result = await JsonSerializer.DeserializeAsync<UploadResponse>(await response.Content.ReadAsStreamAsync())
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

    public async Task Prompt(JsonDocument json)
    {
        string url = "/prompt";
        
    }

    public async Task GetQueue()
    {
        string url = "/queue";
        var response = await this.httpClient.GetAsync(url);
    }
    
    public async Task GetHistory()
    {
        
    }
}