using System.Text.Json.Serialization;

namespace Sail.ComfyUi.Models;

public class HistoryResponseConverter : JsonMappingConverter<IReadOnlyDictionary<Guid, HistoryEntry>, HistoryResponse>
{
    protected override HistoryResponse Map(IReadOnlyDictionary<Guid, HistoryEntry> value)
    {
        return new HistoryResponse(value);
    }

    protected override IReadOnlyDictionary<Guid, HistoryEntry> Unmap(HistoryResponse value)
    {
        return value.HistoryEntries;
    }
}

[JsonConverter(typeof(HistoryResponseConverter))]
public record HistoryResponse(
    IReadOnlyDictionary<Guid, HistoryEntry> HistoryEntries);