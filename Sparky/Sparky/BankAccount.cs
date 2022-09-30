namespace Sparky;

public class BankAccount
{
    public int Balance { private get; set; }

    private readonly ILogBook _logBook;

    public BankAccount(ILogBook logBook)
    {
        Balance = 0;
        _logBook = logBook;
    }

    public bool Deposit(int amount)
    {
        _logBook.Message("Deposit Invoked");

        Balance += amount;
        return true;
    }

    public bool Withdraw(int amount)
    {
        if(Balance >= amount)
        {
            Balance -= amount;
            return true;
        }

        return false;
    }

    public int GetBalance() 
        => Balance;
}
