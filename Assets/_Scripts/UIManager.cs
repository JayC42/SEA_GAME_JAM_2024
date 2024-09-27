using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // Singleton instance
    public TextMeshProUGUI moneyTxt; 
    public int money = 0;
    public float reputation = 10;   
    public float gameTime = 300f; // Initial time per level
    private CustomerPool customerPool;
    private int totalCoins; // Total coins collected

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
    
    }
    public void AddCoins(int amount)
    {
        totalCoins += amount; // Update the total coins
        UpdateCoinDisplayUI(); // Update the UI display
    }

    private void UpdateCoinDisplayUI()
    {
        moneyTxt.text = "Coins: " + totalCoins; // Update the UI with the new amount
    }
}