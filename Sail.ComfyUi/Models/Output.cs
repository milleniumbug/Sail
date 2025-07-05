using System.Text.Json.Serialization;

namespace Sail.ComfyUi.Models;

public record Output(
    [property: JsonPropertyName("images")] IReadOnlyList<Resource> Images);