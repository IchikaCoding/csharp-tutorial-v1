public class Wallet
{
    // これはよく使う書き方らしい。プロパティにしてみた
    public int Money {get; private set;}
    public void AddMoney(int amount)
    {
        if(amount <= 0)
        {
            return;
        }
        else
        {
            Money += amount;
        }
    }
    public void SpendMoney(int amount)
    {
        if(Money < amount || amount <= 0)
        {
            return;
        }
        else
        {
            Money -= amount;
        }
        
    }
}