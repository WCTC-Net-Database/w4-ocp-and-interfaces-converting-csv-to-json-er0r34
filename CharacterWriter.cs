using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CharacterConsole.Models;

namespace CharacterConsole
{
    public class CharacterWriter
    {
        private readonly string _filePath;

        public CharacterWriter(string filePath)
        {
            _filePath = filePath;
        }

        public void WriteCharacters(List<Character> characters)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine
            };

            using (var writer = new StreamWriter(_filePath))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.Context.RegisterClassMap<CharacterMap>();
                csv.WriteRecords(characters);
                writer.Flush(); // Ensure all data is flushed to the file
            }
        }
    }
}
