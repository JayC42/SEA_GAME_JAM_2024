using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }
    public int TotalCoins
    {
        get { return totalCoins; }
        private set
        {
            totalCoins = value;
            UIManager.Instance.UpdateMoneyDisplay();
        }
    }
    private int totalCoins;
    public float priceMultiplier = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //totalCoins = 3; // Starting money
        UIManager.Instance.UpdateMoneyDisplay();
    }

    public void AddCoins(int amount)
    {
        TotalCoins += amount;
    }

    public void RemoveCoins(int amount)
    {
        TotalCoins = Mathf.Max(0, totalCoins - amount);
    }

    public int GetCoins()
    {
        return TotalCoins;
    }

    public void UpdatePriceMultiplier(bool isDecorationActive)
    {
        priceMultiplier = isDecorationActive ? 1.5f : 1f;
        Debug.Log("Price multiplier is now " + priceMultiplier);
    }
}
