using System;
using Newtonsoft.Json;

namespace DdSG {

    public class StixIdConverter: JsonConverter {

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            serializer.Serialize(writer, ((StixId) value).GetStixIdString());
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.Null) {
                return string.Empty;
            }

            var idString = (string) serializer.Deserialize(reader, typeof(string));
            string[] typeAndId = idString.Split("--");
            return new StixId {
                Type = (StixType) Enum.Parse(typeof(StixType), typeAndId[0].KebabToPascalCase()),
                Id = typeAndId[1]
            };
        }

        public override bool CanConvert(Type objectType) {
            return false;
        }

    }

}
