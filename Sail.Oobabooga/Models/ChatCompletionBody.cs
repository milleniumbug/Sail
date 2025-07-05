using System.Text.Json.Serialization;

namespace Sail.Oobabooga.Models;

public record ChatCompletionBody
{
    [JsonPropertyName("messages")]
    public IReadOnlyList<ChatMessage> Messages { get; init; }

    [JsonPropertyName("mode")]
    public string Mode { get; init; }
    
    [JsonPropertyName("max_tokens")]
    public int MaxTokens { get; init; }
}