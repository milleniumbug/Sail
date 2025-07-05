using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sail.ComfyUi;

public record PromptResponse(
    [property: JsonPropertyName("prompt_id")] Guid PromptId,
    [property: JsonPropertyName("number")] int Subfolder,
    [property: JsonPropertyName("node_errors")] JsonDocument Output
);