using System;
using System.IO;
using CharacterConsole.Models;

namespace CharacterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IInput input = new ConsoleInput();
            IOutput output = new ConsoleOutput();
            string csvFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", "input.csv");
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", "input.json");

            // Default to CSV file handler
            IFileHandler fileHandler = new CsvFileHandler(csvFilePath);

            var manager = new CharacterManager(input, output, fileHandler);
            manager.Run();
        }
    }

    public class ConsoleInput : IInput
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }

    public class ConsoleOutput : IOutput
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void Write(string message)
        {
            Console.Write(message);
        }
    }
}
