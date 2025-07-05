using System.Text.Json.Serialization;

namespace Sail.ComfyUi.Models;

public record MessageData(
    [property: JsonPropertyName("prompt_id")] Guid PromptId,
    [property: JsonPropertyName("timestamp")] long Timestamp,
    [property: JsonPropertyName("nodes")] IReadOnlyList<string> Nodes);