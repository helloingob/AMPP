using System;
using System.Globalization;
using Newtonsoft.Json;

namespace AMPP.Data
{
    public class DateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(Convert.ToDateTime(value).ToString("yyyy-dd-mm"));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            return DateTime.ParseExact(Convert.ToString(reader.Value), "dd.MM.yyyy", CultureInfo.InvariantCulture);
        }
    }
}