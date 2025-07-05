using System.Text.Json.Serialization;

namespace Sail.Oobabooga.Models;

public record ChatCompletionResponse
{
    [JsonPropertyName("choices")]
    public IReadOnlyList<ChatChoice> Choices { get; init; }
}