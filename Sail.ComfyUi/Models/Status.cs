using System.Text.Json.Serialization;

namespace Sail.ComfyUi.Models;

public record Status(
    [property: JsonPropertyName("status_str")] string StatusStr,
    [property: JsonPropertyName("completed")] bool Completed,
    [property: JsonPropertyName("messages")] IReadOnlyList<Message> Messages);