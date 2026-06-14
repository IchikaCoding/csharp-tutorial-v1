class BankAccount
{
    // 残高は外から直接変えられないように private
    private int balance = 0;

    // 残高の読み取りは OK
    // これはプロパティらしいよ
    // get setはなくてもプロパティになれるの？
    public int Balance => balance;

    // 入金はこのメソッドを通じてのみ
    public void Deposit(int amount)
    {
        if (amount > 0) balance += amount;
    }
}