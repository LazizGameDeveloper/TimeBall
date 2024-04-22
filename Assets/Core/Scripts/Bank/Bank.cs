using System;

public static class Bank
{
    public static int Coins { get; private set; }

    public static Action<object, int, int> OnCoinsValueChangedEvent;

    public static void AddCoins(object sender, int coins)
    {
        if (coins < 1)
            throw new ArgumentException("Number of coins should be positive");
        
        var oldValue = Coins;
        Coins += coins;
        OnCoinsValueChangedEvent?.Invoke(sender, oldValue, Coins);
    }

    public static void SpendCoins(object sender, int coins)
    {
        if (coins < 1)
            throw new ArgumentException("Number of coins should be positive");

        if (IsEnoughCoins(coins))
            return;

        var oldValue = Coins;
        Coins -= coins;
        OnCoinsValueChangedEvent?.Invoke(sender, oldValue, Coins);
    }

    public static bool IsEnoughCoins(int number)
    {
        return Coins >= number;
    }
}
