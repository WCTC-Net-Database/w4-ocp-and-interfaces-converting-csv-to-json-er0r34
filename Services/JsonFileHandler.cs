using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using CharacterConsole.Models;
using CharacterConsole.Services;

namespace CharacterConsole
{
    public class JsonFileHandler : IFileHandler
    {
        private readonly string _filePath;

        public JsonFileHandler(string filePath)
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

            var json = File.ReadAllText(_filePath);
            var options = new JsonSerializerOptions();
            options.Converters.Add(new CharacterConverter());
            return JsonSerializer.Deserialize<List<Character>>(json, options) ?? new List<Character>();
        }

        public void WriteCharacters(List<Character> characters)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            options.Converters.Add(new CharacterConverter());
            var json = JsonSerializer.Serialize(characters, options);
            File.WriteAllText(_filePath, json);
        }
    }
}
