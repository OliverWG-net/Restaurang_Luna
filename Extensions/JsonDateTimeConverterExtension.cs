using Microsoft.Identity.Client;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Restaurang_luna.Extensions
{
    public class JsonDateTimeConverterExtension : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTimeOffset.Parse(reader.GetString()!, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
        }
        
        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.DateTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture));
        }
         
    }
}
