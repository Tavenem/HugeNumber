using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tavenem.HugeNumbers;

/// <summary>
/// Converts a <see cref="HugeNumber"/> to or from JSON.
/// </summary>
public class HugeNumberConverter : JsonConverter<HugeNumber>
{
    /// <summary>Determines whether the specified type can be converted.</summary>
    /// <param name="typeToConvert">The type to compare against.</param>
    /// <returns>
    /// <see langword="true" /> if the type can be converted; otherwise, <see langword="false"
    /// />.
    /// </returns>
    public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(HugeNumber);

    /// <summary>Reads and converts the JSON to type <see cref="HugeNumber"/>.</summary>
    /// <param name="reader">The reader.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    /// <returns>The converted value.</returns>
    public override HugeNumber Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => HugeNumber.Parse(reader.GetString(), NumberStyles.Float | NumberStyles.Any, NumberFormatInfo.InvariantInfo);

    /// <summary>Writes a specified value as JSON.</summary>
    /// <param name="writer">The writer to write to.</param>
    /// <param name="value">The value to convert to JSON.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    public override void Write(Utf8JsonWriter writer, HugeNumber value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(NumberFormatInfo.InvariantInfo));
}
