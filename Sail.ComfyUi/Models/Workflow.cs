using System.Text.Json.Serialization;

namespace Sail.ComfyUi.Models;

public class Workflow
{
    [JsonExtensionData]
    public IDictionary<string, object>? ExtensionData { get; init; }
}