using System.Text.Json.Serialization;

namespace Sail.ComfyUi.Models;

public class MessageJsonConverter : JsonMappingConverter<
    (string Description, MessageData MessageData),
    Message>
{
    protected override Message Map((string Description, MessageData MessageData) value)
    {
        return new Message(value.Description, value.MessageData);
    }

    protected override (string Description, MessageData MessageData) Unmap(Message value)
    {
        return (value.Description, value.MessageData);
    }
}

[JsonConverter(typeof(MessageJsonConverter))]
public record Message(
    string Description,
    MessageData MessageData);