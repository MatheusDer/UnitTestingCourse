namespace Sparky;

public interface ILogBook
{
    void Message(string message);
    bool LogToDb(string message);
    bool LogBalanceAfterWithdrawl(int balanceAfterWithdraw);
}

public class LogBook : ILogBook
{
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

    public void Message(string message)
    {
        Console.WriteLine(message);
    }
}
