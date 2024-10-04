using CharacterConsole;

public class MockOutput : IOutput
{
    public List<string> Output { get; } = new List<string>();

    public void WriteLine(string message)
    {
        Output.Add(message);
    }

    public void Write(string message)
    {
        Output.Add(message);
    }
}