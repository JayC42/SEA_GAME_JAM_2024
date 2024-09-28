using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // Singleton instance
    public TextMeshProUGUI moneyTxt; 
    public float gameTime = 300f; // Initial time per level
    private CustomerPool customerPool;

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
        UpdateMoneyDisplay();
    }
    public void UpdateMoneyDisplay()
    {
        moneyTxt.text = "Coins: " + MoneyManager.Instance.GetCoins();
    }
}