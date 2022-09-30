namespace Sparky;

public class BankAccount
{
    private int balance;
    private readonly ILogBook _logBook;

    public BankAccount(ILogBook logBook)
    {
        balance = 0;
        _logBook = logBook;
    }

    public bool Deposit(int amount)
    {
        _logBook.Message("Deposit Invoked");

        balance += amount;
        return true;
    }

    public bool Withdraw(int amount)
    {
        if(balance >= amount)
        {
            balance -= amount;
            return true;
        }

        return false;
    }

    public int GetBalance() 
        => balance;
}
