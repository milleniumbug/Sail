using System.Text.Json.Serialization;

namespace Sail.ComfyUi.Models;

public record UploadResponse(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("subfolder")] string Subfolder,
    [property: JsonPropertyName("type")] string Output
);