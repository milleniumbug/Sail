using System.Text.Json.Serialization;

namespace Sail.ComfyUi.Models;

public record Resource(
    [property: JsonPropertyName("filename")] string Filename,
    [property: JsonPropertyName("subfolder")] string Subfolder,
    [property: JsonPropertyName("type")] string Type);