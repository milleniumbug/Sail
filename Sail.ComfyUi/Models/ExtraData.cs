using System.Text.Json.Serialization;

namespace Sail.ComfyUi.Models;

public record ExtraData(
    [property: JsonPropertyName("extra_pnginfo")] ExtraPngInfo ExtraPngInfo);