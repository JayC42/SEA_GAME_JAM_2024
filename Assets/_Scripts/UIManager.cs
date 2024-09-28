using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // Singleton instance
    public GameManager gameManager;
    public TextMeshProUGUI timeTxt;
    public TextMeshProUGUI moneyTxt;
    public int money = 0;
    public float reputation = 10;   
    public float gameTime = 30f; // Initial time per level
    public float timeRemaining = 30f;
    public bool timerIsRunning = false; // Initial time per level
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
    private void Update()
    {
        if (timerIsRunning)
        {
            UpdateTimer();
        }
    }
    void UpdateTimer()
    {
        // Check if there is still time remaining
        if (timeRemaining > 0)
        {
            // Reduce the remaining time by the time passed since the last frame
            timeRemaining -= Time.deltaTime;

            // Clamp time to zero (in case it goes negative)
            timeRemaining = Mathf.Max(timeRemaining, 0);
            timeTxt.text = "Time Remaining: " + Mathf.RoundToInt(timeRemaining).ToString();

            // Debug.Log("Time remaining: " + timeRemaining);
        }
        else
        {
            // Stop the timer when it reaches zero
            timerIsRunning = false;
            gameManager.game_running = false;
            Debug.Log("show stats");
        }
    }

    public void resetTimer()
    {
        timeRemaining = gameTime;
        UpdateMoneyDisplay();
    }
    public void UpdateMoneyDisplay()
    {
        moneyTxt.text = "Coins: " + MoneyManager.Instance.GetCoins();
    }
}