using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using CharacterConsole.Models;

namespace CharacterConsole.Services
{
    public class CharacterConverter : JsonConverter<Character>
    {
        public override Character Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();

            var character = new Character();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    return character;

                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException();

                string propertyName = reader.GetString();
                reader.Read();

                switch (propertyName)
                {
                    case "Name":
                        character.Name = reader.GetString();
                        break;
                    case "Class":
                        character.CharacterClass = reader.GetString();
                        break;
                    case "Level":
                        if (reader.TokenType == JsonTokenType.String)
                        {
                            if (int.TryParse(reader.GetString(), out int level))
                            {
                                character.Level = level;
                            }
                            else
                            {
                                throw new JsonException("Invalid value for Level");
                            }
                        }
                        else if (reader.TokenType == JsonTokenType.Number)
                        {
                            character.Level = reader.GetInt32();
                        }
                        break;
                    case "HP":
                        if (reader.TokenType == JsonTokenType.String)
                        {
                            if (int.TryParse(reader.GetString(), out int hp))
                            {
                                character.HitPoints = hp;
                            }
                            else
                            {
                                throw new JsonException("Invalid value for HP");
                            }
                        }
                        else if (reader.TokenType == JsonTokenType.Number)
                        {
                            character.HitPoints = reader.GetInt32();
                        }
                        break;
                    case "Equipment":
                        var equipmentString = reader.GetString();
                        character.Equipment = equipmentString?.Split('|');
                        break;
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Character value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("Name", value.Name);
            writer.WriteString("Class", value.CharacterClass);
            writer.WriteNumber("Level", value.Level);
            writer.WriteNumber("HP", value.HitPoints);
            writer.WriteString("Equipment", value.Equipment != null ? string.Join('|', value.Equipment) : string.Empty);
            writer.WriteEndObject();
        }
    }
}
