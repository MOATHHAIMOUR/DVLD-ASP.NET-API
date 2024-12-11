using System.Text.Json;

namespace DVLD.Application.Common.Converters
{
    public class NumericEnumConverter<T> : System.Text.Json.Serialization.JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return (T)Enum.ToObject(typeof(T), reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(Convert.ToInt32(value));
        }
    }

}
