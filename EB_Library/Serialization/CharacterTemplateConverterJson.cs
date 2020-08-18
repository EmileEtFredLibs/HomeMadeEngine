using HomeMadeEngine.Templates;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGamePlayer.Serialization
{
    public class CharacterTemplateConverterJson : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(CharacterTemplate);
        }
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value != null)
            {
                var charT = (CharacterTemplate)value;
                writer.WriteStartObject();
                writer.WritePropertyName("Level");
                serializer.Serialize(writer, charT.Level);
                writer.WriteEndObject();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (existingValue != null)
            {
                while (reader.Read())
                {
                    if (reader.TokenType != JsonToken.PropertyName) break;
                }
                
            }
            return new CharacterTemplate();
        }
    }
}

