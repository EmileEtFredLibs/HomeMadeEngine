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
            CharacterTemplate charT;
            if (value != null)
                charT = (CharacterTemplate)value;
            else
                charT = new CharacterTemplate();
            writer.WriteStartObject();
            writer.WritePropertyName("Level");
            serializer.Serialize(writer, charT.Level);

            writer.WritePropertyName("Experience");
            serializer.Serialize(writer, charT.Experience);

            writer.WritePropertyName("CurrentHp");
            serializer.Serialize(writer, charT.CurrentHp);

            writer.WritePropertyName("MaxHp");
            serializer.Serialize(writer, charT.MaxHp);

            writer.WritePropertyName("Shield");
            serializer.Serialize(writer, charT.Shield);

            writer.WritePropertyName("ShieldTimer");
            serializer.Serialize(writer, charT.ShieldTimer);

            writer.WritePropertyName("RessourceType");
            serializer.Serialize(writer, charT.RessourceType);

            writer.WritePropertyName("MaxRessource");
            serializer.Serialize(writer, charT.MaxRessource);

            writer.WritePropertyName("IsDead");
            serializer.Serialize(writer, charT.IsDead);

            writer.WritePropertyName("Stats");
            writer.WriteStartArray();
            writer.WriteStartObject();
            writer.WritePropertyName("Name");
            serializer.Serialize(writer, charT.Stats[0].Name);
            writer.WritePropertyName("Stat");
            serializer.Serialize(writer, charT.Stats[0].Stat);
            writer.WritePropertyName("Dmg");
            serializer.Serialize(writer, charT.Stats[0].Dmg);
            writer.WritePropertyName("Flat");
            serializer.Serialize(writer, charT.Stats[0].Flat);
            writer.WritePropertyName("Multi");
            serializer.Serialize(writer, charT.Stats[0].Multi);
            writer.WriteEndObject();
            writer.WriteEndArray();

            writer.WritePropertyName("Equipements");
            writer.WriteStartArray();
            writer.WriteStartObject();
            writer.WritePropertyName("Name");
            serializer.Serialize(writer, charT.Equipements[0].Name);
            writer.WritePropertyName("Dmg");
            serializer.Serialize(writer, charT.Equipements[0].Slot);
            writer.WritePropertyName("Stat");
            serializer.Serialize(writer, charT.Equipements[0].Rarity);
            writer.WriteStartArray();
            writer.WritePropertyName("Flat");
            serializer.Serialize(writer, charT.Equipements[0].Stats);
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.WriteEndArray();

            writer.WritePropertyName("Actions");
            writer.WriteStartArray();
            writer.WriteStartObject();
            writer.WritePropertyName("Name");
            serializer.Serialize(writer, charT.Actions[0].Name);
            writer.WritePropertyName("Stat");
            serializer.Serialize(writer, charT.Actions[0].Cost);
            writer.WritePropertyName("Dmg");
            serializer.Serialize(writer, charT.Actions[0].Index);
            writer.WriteEndObject();
            writer.WriteEndArray();

            writer.WritePropertyName("Buffs");
            writer.WriteStartArray();
            writer.WriteStartObject();
            writer.WritePropertyName("Name");
            serializer.Serialize(writer, charT.Buffs[0].Name);
            writer.WritePropertyName("Timer");
            serializer.Serialize(writer, charT.Buffs[0].Timer);
            writer.WriteStartArray();
            writer.WritePropertyName("Stat");
            serializer.Serialize(writer, charT.Buffs[0].Stat);
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.WriteEndArray();

            writer.WritePropertyName("Debuffs");
            writer.WriteStartArray();
            writer.WriteStartObject();
            writer.WritePropertyName("Name");
            serializer.Serialize(writer, charT.Debuffs[0].Name);
            writer.WritePropertyName("Timer");
            serializer.Serialize(writer, charT.Debuffs[0].Timer);
            writer.WriteStartArray();
            writer.WritePropertyName("Stat");
            serializer.Serialize(writer, charT.Debuffs[0].Stat);
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.WriteEndArray();

            writer.WritePropertyName("Postion");
            writer.WriteStartObject();
            writer.WritePropertyName("X");
            serializer.Serialize(writer, charT.Position.X);
            writer.WritePropertyName("Y");
            serializer.Serialize(writer, charT.Position.Y);
            writer.WritePropertyName("Z");
            serializer.Serialize(writer, charT.Position.Z);
            writer.WriteEndObject();

            writer.WritePropertyName("Velocity");
            writer.WriteStartObject();
            writer.WritePropertyName("X");
            serializer.Serialize(writer, charT.Velocity.X);
            writer.WritePropertyName("Y");
            serializer.Serialize(writer, charT.Velocity.Y);
            writer.WritePropertyName("Z");
            serializer.Serialize(writer, charT.Velocity.Z);
            writer.WriteEndObject();

            writer.WritePropertyName("Acceleration");
            writer.WriteStartObject();
            writer.WritePropertyName("X");
            serializer.Serialize(writer, charT.Acceleration.X);
            writer.WritePropertyName("Y");
            serializer.Serialize(writer, charT.Acceleration.Y);
            writer.WritePropertyName("Z");
            serializer.Serialize(writer, charT.Acceleration.Z);
            writer.WriteEndObject();

            writer.WritePropertyName("Gravity");
            writer.WriteStartObject();
            writer.WritePropertyName("X");
            serializer.Serialize(writer, charT.Gravity.X);
            writer.WritePropertyName("Y");
            serializer.Serialize(writer, charT.Gravity.Y);
            writer.WritePropertyName("Z");
            serializer.Serialize(writer, charT.Gravity.Z);
            writer.WriteEndObject();

            writer.WriteEndObject();
        }
        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            CharacterTemplate charT;
            if (existingValue != null)
            {
                while (reader.Read())
                {
                    if (reader.TokenType != JsonToken.PropertyName) break;
                }
                
            }
            else
                charT = new CharacterTemplate();
            return new CharacterTemplate();
        }
    }
}

