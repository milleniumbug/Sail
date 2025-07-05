using System.Text.Json.Serialization;

namespace Sail.LibreTranslate;

public record LibreTranslateResult(
    [property: JsonPropertyName("translatedText")] string TranslatedText);