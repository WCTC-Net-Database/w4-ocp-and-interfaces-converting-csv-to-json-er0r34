using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CharacterConsole.Models;

namespace CharacterConsole
{
    public class CharacterReader
    {
        private readonly string _filePath;

        public CharacterReader(string filePath)
        {
            _filePath = filePath;
        }

        public List<Character> ReadCharacters()
        {
            if (!File.Exists(_filePath))
            {
                Console.WriteLine($"File not found: {_filePath}");
                return new List<Character>();
            }

            using var reader = new StreamReader(_filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture));
            return new List<Character>(csv.GetRecords<Character>());
        }

        public Character FindCharacterByName(string name)
        {
            var characters = ReadCharacters();
            return characters.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
