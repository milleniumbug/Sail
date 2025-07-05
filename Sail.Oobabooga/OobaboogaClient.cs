using System.Net.Http.Json;
using System.Text.Json;
using Sail.Oobabooga.Models;

namespace Sail.Oobabooga;

public class OobaboogaClient(HttpClient client)
{
    public async Task<string> Instruct(string prompt)
    {
        var response = await client.PostAsJsonAsync("/v1/chat/completions", new ChatCompletionBody
        {
            Messages = new[]
            {
                new ChatMessage
                {
                    Role = "user",
                    Content = prompt,
                }
            },
            Mode = "instruct",
            MaxTokens = 128000,
        });

        response.EnsureSuccessStatusCode();
        
        string rawResult = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ChatCompletionResponse>(rawResult)
                     ?? throw new JsonException("null response");
        return result.Choices[0].Message.Content;
    }
}