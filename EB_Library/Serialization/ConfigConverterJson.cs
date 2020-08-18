using ConsoleGamePlayer.ConsoleInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ConsoleGamePlayer.Serialization
{
    public class ConfigConverterJson : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Config);
        }
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value != null)
            {

            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (existingValue != null)
            {

            }
            return new Config();
        }
    }
}
