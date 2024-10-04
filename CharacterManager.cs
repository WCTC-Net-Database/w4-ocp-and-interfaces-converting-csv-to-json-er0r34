using System;
using System.Collections.Generic;
using System.Linq;
using CharacterConsole.Models;

namespace CharacterConsole
{
    public class CharacterManager
    {
        private readonly IInput _input;
        private readonly IOutput _output;
        private IFileHandler _fileHandler;
        private List<Character> _characters;

        public CharacterManager(IInput input, IOutput output, IFileHandler fileHandler)
        {
            _input = input;
            _output = output;
            _fileHandler = fileHandler;
            _characters = _fileHandler.ReadCharacters();
        }

        public void Run()
        {
            while (true)
            {
                _output.WriteLine("Menu:");
                _output.WriteLine("1. Display Characters");
                _output.WriteLine("2. Find Character");
                _output.WriteLine("3. Add Character");
                _output.WriteLine("4. Level Up Character");
                _output.WriteLine("5. Change File Format (CSV/JSON)");
                _output.WriteLine("6. Exit");
                _output.Write("Enter your choice: ");
                string choice = _input.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAllCharacters();
                        break;
                    case "2":
                        FindCharacter();
                        break;
                    case "3":
                        AddCharacter();
                        _fileHandler.WriteCharacters(_characters);
                        break;
                    case "4":
                        LevelUpCharacter();
                        _fileHandler.WriteCharacters(_characters);
                        break;
                    case "5":
                        ChangeFileFormat();
                        break;
                    case "6":
                        return;
                    default:
                        _output.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public void DisplayAllCharacters()
        {
            foreach (var character in _characters)
            {
                _output.WriteLine($"{character.Name}");
                _output.WriteLine($"Level {character.Level} {character.CharacterClass}     {character.HitPoints}/{character.HitPoints} Hit Points");
                _output.WriteLine("Equipment:");
                if (character.Equipment != null && character.Equipment.Length > 0)
                {
                    foreach (var item in character.Equipment)
                    {
                        _output.WriteLine($"{new string(' ', 10)}{item}");
                    }
                }
                else
                {
                    _output.WriteLine($"{new string(' ', 10)}None");
                }
                _output.WriteLine("");
            }
        }

        public void FindCharacter()
        {
            _output.Write("Enter the name of the character to find: ");
            string name = _input.ReadLine();
            var character = _characters.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (character != null)
            {
                string equipment = character.Equipment != null ? string.Join(", ", character.Equipment) : "None";
                _output.WriteLine($"Name: {character.Name}, Class: {character.CharacterClass}, Level: {character.Level}, HP: {character.HitPoints}, Equipment: {equipment}");
            }
            else
            {
                _output.WriteLine("Character not found.");
            }
        }

        public void AddCharacter()
        {
            _output.Write("Enter first name: ");
            string firstName = _input.ReadLine();

            _output.Write("Enter last name (leave blank if none): ");
            string lastName = _input.ReadLine();

            string formattedName = string.IsNullOrEmpty(lastName) ? firstName : $"{lastName}, {firstName}";

            _output.Write("Enter class: ");
            string characterClass = _input.ReadLine();
            _output.Write("Enter level: ");
            int level = int.Parse(_input.ReadLine());
            _output.Write("Enter hit points: ");
            int hitPoints = int.Parse(_input.ReadLine());

            List<string> equipmentList = new List<string>();
            string input;

            _output.WriteLine("Enter equipment one-by-one. Type 'done' when you are finished:");

            while (true)
            {
                _output.Write("Enter equipment: ");
                input = _input.ReadLine();

                if (input.ToLower() == "done")
                {
                    break;
                }

                equipmentList.Add(input);
            }

            string[] equipment = equipmentList.ToArray();

            _characters.Add(new Character
            {
                Name = formattedName,
                CharacterClass = characterClass,
                Level = level,
                HitPoints = hitPoints,
                Equipment = equipment
            });
        }

        public void LevelUpCharacter()
        {
            _output.Write("Enter the name of the character to level up: ");
            string nameToLevelUp = _input.ReadLine();

            foreach (var character in _characters)
            {
                if (character.Name.Equals(nameToLevelUp, StringComparison.OrdinalIgnoreCase))
                {
                    character.Level++;
                    _output.WriteLine($"Character {character.Name} leveled up to level {character.Level}!");
                    return;
                }
            }

            _output.WriteLine("Character not found.");
        }

        public void ChangeFileFormat()
        {
            _output.WriteLine("Choose file format:");
            _output.WriteLine("1. CSV");
            _output.WriteLine("2. JSON");
            _output.Write("Enter your choice: ");
            string choice = _input.ReadLine();

            switch (choice)
            {
                case "1":
                    _fileHandler = new CsvFileHandler("files/input.csv");
                    break;
                case "2":
                    _fileHandler = new JsonFileHandler("files/input.json");
                    break;
                default:
                    _output.WriteLine("Invalid choice. Please try again.");
                    return;
            }

            _fileHandler.WriteCharacters(_characters); // Write characters to the new file format
            _characters = _fileHandler.ReadCharacters(); // Read characters from the new file format
            _output.WriteLine("File format changed successfully.");
        }
    }
}
