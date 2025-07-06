using System.Text.Json.Serialization;

namespace Sail.Automatic1111.Models;

public record InterrogateResponse(
    [property: JsonPropertyName("caption")] string Caption);