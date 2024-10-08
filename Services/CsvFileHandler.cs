using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using CharacterConsole.Models;

namespace CharacterConsole
{
    public class CsvFileHandler : IFileHandler
    {
        private readonly string _filePath;

        public CsvFileHandler(string filePath)
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

        public void WriteCharacters(List<Character> characters)
        {
            using var writer = new StreamWriter(_filePath);
            using var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture));
            csv.WriteRecords(characters);
        }
    }
}
