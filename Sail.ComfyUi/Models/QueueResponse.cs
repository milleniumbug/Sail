using System.Text.Json.Serialization;

namespace Sail.ComfyUi.Models;

public class QueueConverter : JsonMappingConverter<
    (int Number, Guid PromptId, Workflow Workflow, ExtraData ExtraData, IReadOnlyList<string> Outputs),
    PromptEntry>
{
    protected override PromptEntry? Map((int Number, Guid PromptId, Workflow Workflow, ExtraData ExtraData, IReadOnlyList<string> Outputs) value)
    {
        return new PromptEntry(value.Number, value.PromptId, value.Workflow, value.ExtraData, value.Outputs);
    }

    protected override (int Number, Guid PromptId, Workflow Workflow, ExtraData ExtraData, IReadOnlyList<string> Outputs) Unmap(PromptEntry value)
    {
        return (value.Number, value.PromptId, value.Workflow, value.ExtraData, value.Outputs);
    }
}

[JsonConverter(typeof(QueueConverter))]
public record PromptEntry(
    int Number,
    Guid PromptId,
    Workflow Workflow,
    ExtraData ExtraData,
    IReadOnlyList<string> Outputs);

public record QueueResponse(
    [property: JsonPropertyName("queue_running")] IReadOnlyList<PromptEntry> Running,
    [property: JsonPropertyName("queue_pending")] IReadOnlyList<PromptEntry> Pending);