using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton instance
    public int currentDay = 1;
    public int currentLevel = 1;
    public float gameTime = 300f; // Initial time per level
    public CustomerPool customerPool;
    public int interval = 5; 
    public bool game_running = false;
    public GameOrderManager gameOrderManager;
    public UIManager timer;

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
        // While game is running, show timer
        if (game_running)
        {
            UIManager.Instance.timerObject.SetActive(true);
        }
        else
        {
            UIManager.Instance.timerObject.SetActive(false);
        }
    }
    private IEnumerator InitializeCustomerRoutine()
    {
        while (game_running) // Loop only while the game is running
        {
            customerPool.SpawnCustomer(); // Call the method
            Debug.Log("Spawn Customer called");
            yield return new WaitForSeconds(interval); // Wait for 'interval' seconds
        }
    }

    void StopInitializeCustomer()
    {
        StopCoroutine(InitializeCustomerRoutine()); // Stops the coroutine
        Debug.Log("routine stopped");
        currentDay += 1;
    }
    void Start()
    {
        //customerPool = GetComponent<CustomerPool>(); // Assumes CustomerPool is attached to the same GameObject
        customerPool.InitializePool(customerPool.maxPoolSize);
        StartGame();
        StartCoroutine(InitializeCustomerRoutine());
    }

    public void StartGame()
    {
        game_running = true;
        timer.timerIsRunning = true;
        timer.resetTimer();
        InitializeCustomerRoutine();
    }

    void InitializeCustomerPool()
    {
        int minCustomers, maxCustomers;

        if (currentLevel <= 10)
        {
            minCustomers = 5;
            maxCustomers = 15;
        }
        else if (currentLevel <= 20)
        {
            minCustomers = 15;
            maxCustomers = 30;
        }
        else
        {
            minCustomers = 30;
            maxCustomers = 50; // Adjust this range as needed
        }

        int totalCustomers = Random.Range(minCustomers, maxCustomers + 1);
        customerPool.InitializePool(totalCustomers);
    }

    public int sunlightQuantity;
    public int windQuantity;
    public int airQuantity;
    // void SpawnCustomer()
    // {
    //     customerPool.SpawnCustomer();
    // }
}