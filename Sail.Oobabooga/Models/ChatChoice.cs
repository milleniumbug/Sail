using System.Text.Json.Serialization;

namespace Sail.Oobabooga.Models;

public record ChatChoice
{
    [JsonPropertyName("index")]
    public int Index { get; init; }
    
    [JsonPropertyName("finish_reason")]
    public string FinishReason { get; init; }
    
    [JsonPropertyName("message")]
    public ChatMessage Message { get; init; }
}