using System.Text.Json.Serialization;

namespace Sail.ComfyUi.Models;

public record HistoryEntry(
    [property: JsonPropertyName("prompt")] PromptEntry Prompt,
    [property: JsonPropertyName("outputs")] IReadOnlyDictionary<string, Output> Outputs,
    [property: JsonPropertyName("status")] Status Status,
    [property: JsonPropertyName("meta")] Meta Meta);