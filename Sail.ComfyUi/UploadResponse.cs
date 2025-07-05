using System.Text.Json.Serialization;

namespace Sail.ComfyUi;

public record UploadResponse(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("subfolder")] string Subfolder,
    [property: JsonPropertyName("output")] string Output
);