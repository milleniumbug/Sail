using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Sail.Automatic1111.Models;

namespace Sail.Automatic1111;

public class Automatic1111Client(HttpClient api)
{
    private JsonSerializerOptions jsonSerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public async Task<InterrogateResponse> Interrogate(InterrogateRequest request)
    {
        var url = "/sdapi/v1/interrogate";
        var requestString = JsonSerializer.Serialize(request, jsonSerializerOptions);
        //logger.LogInformation("Sending to {Url}: {Request}", url, requestString);
        var requestContent = new StringContent(requestString, Encoding.UTF8, "application/json");
        
        var response = await api.PostAsync(url, requestContent);
        var responseString = await response.Content.ReadAsStringAsync();
        //logger.LogInformation("Received from {Url}: {Request}", url, responseString);
        
        response.EnsureSuccessStatusCode();
        return JsonSerializer.Deserialize<InterrogateResponse>(responseString, jsonSerializerOptions) ?? throw new JsonException("null response");
    }

    public async Task<TextToImageResponse> TextToImage(TextToImageRequest request)
    {
        var url = "/sdapi/v1/txt2img";
        var requestString = JsonSerializer.Serialize(request, jsonSerializerOptions);
        //logger.LogInformation("Sending to {Url}: {Request}", url, requestString);
        var requestContent = new StringContent(requestString, Encoding.UTF8, "application/json");
        
        var response = await api.PostAsync(url, requestContent);
        var responseString = await response.Content.ReadAsStringAsync();
        //logger.LogInformation("Received from {Url}: {Request}", url, responseString);
        
        response.EnsureSuccessStatusCode();
        return JsonSerializer.Deserialize<TextToImageResponse>(responseString, jsonSerializerOptions) ?? throw new JsonException("null response");
    }
    
    public async Task<ImageToImageResponse> ImageToImage(ImageToImageRequest request)
    {
        var url = "/sdapi/v1/img2img";
        var requestString = JsonSerializer.Serialize(request, jsonSerializerOptions);
        //logger.LogInformation("Sending to {Url}: {Request}", url, requestString);
        var requestContent = new StringContent(requestString, Encoding.UTF8, "application/json");
        
        var response = await api.PostAsync(url, requestContent);
        var responseString = await response.Content.ReadAsStringAsync();
        //logger.LogInformation("Received from {Url}: {Request}", url, responseString);
        
        response.EnsureSuccessStatusCode();
        return JsonSerializer.Deserialize<ImageToImageResponse>(responseString, jsonSerializerOptions) ?? throw new JsonException("null response");
    }
}