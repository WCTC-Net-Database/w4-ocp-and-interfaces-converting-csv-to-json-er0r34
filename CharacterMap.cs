using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System.Linq;
using CharacterConsole.Models;

namespace CharacterConsole
{
    public sealed class CharacterMap : ClassMap<Character>
    {
        public CharacterMap()
        {
            Map(m => m.Name).Name("Name").Convert(row =>
            {
                var name = row.Row.GetField("Name");
                if (!string.IsNullOrEmpty(name))
                {
                    var parts = name.Split(' ');
                    if (parts.Length > 1)
                    {
                        var firstName = parts[0];
                        var lastName = string.Join(" ", parts.Skip(1));
                        var formattedName = $"{lastName}, {firstName}";
                        if (formattedName.Contains(' ') || formattedName.Contains(','))
                        {
                            return $"\"{formattedName}\"";
                        }
                        return formattedName;
                    }
                }
                return name;
            });

            Map(m => m.CharacterClass).Name("Class");
            Map(m => m.Level).Name("Level");
            Map(m => m.HitPoints).Name("HP");
            Map(m => m.Equipment).Name("Equipment").Convert(row =>
            {
                var equipmentField = row.Row.GetField("Equipment");
                if (string.IsNullOrEmpty(equipmentField))
                {
                    return new string[0];
                }
                var equipment = equipmentField.Split('|')
                    .Select(e => e.Contains(' ') ? $"\"{e}\"" : e)
                    .ToArray();
                return equipment;
            });
        }
    }
}
