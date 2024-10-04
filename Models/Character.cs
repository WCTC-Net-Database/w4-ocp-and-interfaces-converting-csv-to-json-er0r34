using CsvHelper.Configuration.Attributes;

namespace CharacterConsole.Models
{
    public class Character
    {
        [Name("Name")]
        public string Name { get; set; }

        [Name("Class")]
        public string CharacterClass { get; set; }

        [Name("Level")]
        public int Level { get; set; }

        [Name("HP")]
        public int HitPoints { get; set; }

        [Name("Equipment")]
        public string[] Equipment { get; set; }
    }
}
