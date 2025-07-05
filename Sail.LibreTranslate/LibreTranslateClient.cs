using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Sail.LibreTranslate;

public class LibreTranslateClient(HttpClient httpClient)
{
    public async Task<LibreTranslateResult> Translate(LibreTranslateQuery query)
    {
        var response = await httpClient.PostAsJsonAsync("/translate", query);
        
        while (response.StatusCode == HttpStatusCode.TooManyRequests)
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
        }

        response.EnsureSuccessStatusCode();

        var result = await JsonSerializer.DeserializeAsync<LibreTranslateResult>(
            await response.Content.ReadAsStreamAsync())
            ?? throw new InvalidDataException();

        return result;
    }
}