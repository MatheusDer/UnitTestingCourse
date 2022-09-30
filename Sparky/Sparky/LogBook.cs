namespace Sparky;

public interface ILogBook
{
    void Message(string message);
}

public class LogBook : ILogBook
{
    public void Message(string message)
    {
        Console.WriteLine(message);
    }
}

public class FakkerLogBook : ILogBook
{
    public void Message(string message)
    { }
}
