using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sail.ComfyUi;

public record PromptRequest(
    [property: JsonPropertyName("prompt")] JsonDocument Prompt,
    [property: JsonPropertyName("client_id")] string ClientId
);