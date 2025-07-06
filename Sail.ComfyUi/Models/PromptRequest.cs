using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sail.ComfyUi.Models;

public record PromptRequest(
    [property: JsonPropertyName("prompt")] Workflow Prompt,
    [property: JsonPropertyName("client_id")] string? ClientId = null,
    [property: JsonPropertyName("extra_data")] ExtraData? ExtraData = null);