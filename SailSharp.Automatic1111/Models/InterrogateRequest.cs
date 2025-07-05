using System.Text.Json.Serialization;

namespace Sail.Automatic1111.Models;

public record InterrogateRequest(
    [property: JsonPropertyName("model")] string Model,
    [property: JsonPropertyName("image")] byte[] Image);