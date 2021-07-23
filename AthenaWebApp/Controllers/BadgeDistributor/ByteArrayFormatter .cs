using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Controllers.BadgeDistributor
{
    public class ByteArrayFormatter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(byte[]);
        }
        public override bool CanRead
        {
            get
            {
                return false;
            }
        }
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var byteArray = (byte[])value;
            writer.WriteStartArray();
            if (byteArray != null && byteArray.Length > 0)
            {
                foreach (var b in byteArray)
                {
                    writer.WriteValue(b);
                }
            }
            writer.WriteEndArray();
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
