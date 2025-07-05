using System.Reflection;
using System.Text.Json;
using Sail.ComfyUi.Models;
using TupleAsJsonArray;

namespace Sail.Tests.ComfyUi;

public class ComfyUiTests
{
    [Fact]
    public void QueueDeserialization()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        var a = assembly.GetManifestResourceNames();
        using Stream responseStream = assembly.GetManifestResourceStream("Sail.Tests.ComfyUi.QueueResponse.json") ?? throw new InvalidOperationException();
        var queueResponse = JsonSerializer.Deserialize<QueueResponse>(responseStream, new JsonSerializerOptions()
        {
            Converters =
            {
                new TupleConverterFactory()
            }
        })!;
        Assert.Equal(new Guid("df0b7f9e-7bee-4c3e-9745-87b503541049"), queueResponse.Running[0].PromptId);
    }
    
    [Fact]
    public void HistoryDeserialization()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        using Stream responseStream = assembly.GetManifestResourceStream("Sail.Tests.ComfyUi.HistoryResponse.json") ?? throw new InvalidOperationException();
        var historyResponse = JsonSerializer.Deserialize<HistoryResponse>(responseStream, new JsonSerializerOptions()
        {
            Converters =
            {
                new TupleConverterFactory()
            }
        })!;
        var historyEntry = historyResponse.HistoryEntries[new Guid("df0b7f9e-7bee-4c3e-9745-87b503541049")];
        Assert.Equal("success", historyEntry.Status.StatusStr);
        Assert.True(historyEntry.Status.Completed);
        Assert.Equal("execution_start", historyEntry.Status.Messages[0].Description);
        Assert.Equal(1751665624677L, historyEntry.Status.Messages[0].MessageData.Timestamp);
        Assert.Equal(1751665624677L, historyEntry.Status.Messages[0].MessageData.Timestamp);
        Assert.Equal("redraw_part_00003.png", historyEntry.Outputs["227"].Images[0].Filename);
        Assert.Equal("output", historyEntry.Outputs["227"].Images[0].Type);
    }
}