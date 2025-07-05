using System.Text.Json.Serialization;

namespace Sail.ComfyUi;

public record QueueResponse(
    [property: JsonPropertyName("queue_running")] IReadOnlyList<QueueEntry> Running);