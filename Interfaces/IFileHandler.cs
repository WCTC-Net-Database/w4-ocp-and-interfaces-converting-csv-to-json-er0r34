using System.Collections.Generic;
using CharacterConsole.Models;

namespace CharacterConsole
{
    public interface IFileHandler
    {
        List<Character> ReadCharacters();
        void WriteCharacters(List<Character> characters);
    }
}
