using UnityEngine;

public class MoneyHolder : MonoBehaviour
{
    // This is the main variable for the players money
    public int moneyAmount;

    void Start()
    {
        moneyAmount = 1500;
    }

    public void AddMoney(int addAmount)
    {
        moneyAmount += addAmount;
    }

    public void decreaseMoney(int loseAmount)
    {
        moneyAmount -= loseAmount;
    }
}
