using System.Text.Json.Serialization;

namespace Sail.Oobabooga.Models;

public record ChatMessage
{
    [JsonPropertyName("role")]
    public string Role { get; init; }
    
    [JsonPropertyName("content")]
    public string Content { get; init; }
}