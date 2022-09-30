namespace Sparky;

public interface ILogBook
{
    int LogSeverity { get; set; }
    string LogType { get; set; }

    void Message(string message);
    string MessageWithReturn(string message);
    bool LogToDb(string message);
    bool LogBalanceAfterWithdrawl(int balanceAfterWithdraw);
    bool LogWithOutputResult(string message, out string str);
    bool LogWithRefObj(ref Customer customer);
}

public class LogBook : ILogBook
{
    public int LogSeverity { get; set; }
    public string LogType { get; set; }

    public bool LogBalanceAfterWithdrawl(int balanceAfterWithdraw)
    {
        if (balanceAfterWithdraw >= 0)
        {
            Console.WriteLine("Success");
            return true;
        }

        Console.WriteLine("Failed");
        return false;
    }

    public bool LogToDb(string message)
    {
        Console.WriteLine(message);
        return true;
    }

    public bool LogWithOutputResult(string message, out string str)
    {
        str = "Hello " + message;
        return true;
    }

    public bool LogWithRefObj(ref Customer customer) 
        => true;

    public void Message(string message)
    {
        Console.WriteLine(message);
    }

    public string MessageWithReturn(string message)
    {
        Console.WriteLine(message);
        return message.ToLower();
    }
}
