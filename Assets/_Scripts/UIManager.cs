using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // Singleton instance
    public GameManager gameManager;
    public TextMeshProUGUI dayTxt;
    public TextMeshProUGUI timeTxt;
    public TextMeshProUGUI moneyTxt;
    public GameObject timerObject;
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
            gameManager.StopGame();
            Debug.Log("Time's up!");
        }
    }

    public void resetTimer()
    {
        timeRemaining = gameTime;
        UpdateMoneyDisplay();
    }
    public void UpdateMoneyDisplay()
    {
        if (MoneyManager.Instance.TotalCoins <= 0)
        {
            moneyTxt.text = "0";
        }
        else
        {
            moneyTxt.text = MoneyManager.Instance.GetCoins().ToString();
        }
    }
}