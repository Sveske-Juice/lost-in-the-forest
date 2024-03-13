using System;
using UnityEngine;

public class CreditManager : MonoBehaviour
{
    public static CreditManager Instance { get; private set; }
    public static Action<int> OnCreditChange;

    [SerializeField] private int startingCoins = 10;
    public int Coins { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Coins = startingCoins;
    }

    private void Start()
    {
        OnCreditChange?.Invoke(Coins);
    }

    public void AddCoin(int quantity = 1)
    {
        if (quantity == 0) return;

        Coins += quantity;
        OnCreditChange?.Invoke(Coins);
    }

    public bool CanAfford(float price) => Coins >= price;

    public bool Charge(int price)
    {
        // Can not afford
        if (!CanAfford(price))
            return false;

        Coins -= price;
        OnCreditChange?.Invoke(Coins);
        return true;
    }
}
