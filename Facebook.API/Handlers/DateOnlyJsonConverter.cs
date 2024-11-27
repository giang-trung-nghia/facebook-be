using System.Text.Json;
using System.Text.Json.Serialization;

namespace Facebook.API.Handlers
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private const string DateFormat = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? dateString = reader.GetString();

            if (string.IsNullOrWhiteSpace(dateString))
            {
                throw new JsonException("Date string is null or empty.");
            }

            // Try parsing as DateOnly
            if (DateOnly.TryParseExact(dateString, DateFormat, null, System.Globalization.DateTimeStyles.None, out var dateOnly))
            {
                return dateOnly;
            }

            // Fallback: Try parsing as DateTime and converting to DateOnly
            if (DateTime.TryParse(dateString, out var dateTime))
            {
                return DateOnly.FromDateTime(dateTime);
            }

            throw new JsonException($"Unable to parse '{dateString}' as DateOnly.");
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DateFormat));
        }
    }

}