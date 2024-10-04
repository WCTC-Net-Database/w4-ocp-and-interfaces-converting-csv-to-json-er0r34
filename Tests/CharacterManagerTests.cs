using System;
using CharacterConsole;
using CharacterConsole.Models;
using Moq;
using Xunit;

namespace CharacterConsole.Tests
{
    public class CharacterManagerTests
    {
        [Fact]
        public void Test_DisplayAllCharacters()
        {
            // Arrange
            var mockInput = new Mock<IInput>();
            var mockOutput = new Mock<IOutput>();
            IFileHandler fileHandler = new CsvFileHandler("input.csv"); // or new JsonFileHandler("input.json")

            var characterManager = new CharacterManager(mockInput.Object, mockOutput.Object, fileHandler);

            // Act
            characterManager.DisplayAllCharacters();

            // Assert
            mockOutput.Verify(o => o.WriteLine(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Test_FindCharacter()
        {
            // Arrange
            var mockInput = new Mock<IInput>();
            var mockOutput = new Mock<IOutput>();
            IFileHandler fileHandler = new CsvFileHandler("input.csv"); // or new JsonFileHandler("input.json")

            var characterManager = new CharacterManager(mockInput.Object, mockOutput.Object, fileHandler);

            // Act
            characterManager.FindCharacter();

            // Assert
            mockOutput.Verify(o => o.WriteLine(It.IsAny<string>()), Times.AtLeastOnce);
        }

        // Add more tests as needed
    }
}
