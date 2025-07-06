using System.Text.Json.Serialization;

namespace Sail.Automatic1111.Models;

public record ImageToImageResponse(
    [property: JsonPropertyName("images")] IEnumerable<byte[]> Images,
    [property: JsonPropertyName("info")] string Info);