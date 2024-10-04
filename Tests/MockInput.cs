using CharacterConsole;

public class MockInput : IInput
{
    private readonly Queue<string> _inputs;

    public MockInput(IEnumerable<string> inputs = null)
    {
        _inputs = new Queue<string>(inputs ?? new string[0]);
    }

    public string ReadLine()
    {
        return _inputs.Count > 0 ? _inputs.Dequeue() : string.Empty;
    }
}