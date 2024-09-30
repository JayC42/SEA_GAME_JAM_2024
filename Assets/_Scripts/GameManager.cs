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
    public float coinMultiplier = 1;
    public GameObject pauseScreen;

    public bool isPaused = false;
    // this is gonna look ugly
    public bool sunlightAuto; 
    public bool windAuto; 
    public bool airAuto;
    public bool sunlightDouble; 
    public bool windDouble; 
    public bool airDouble;
    private MainMenu mainMenu;
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
        // if (game_running)
        // {
        UIManager.Instance.UpdateMoneyDisplay();
        //     UIManager.Instance.timerObject.SetActive(true);
        // }
        // else
        // {
        //     UIManager.Instance.timerObject.SetActive(false);
        // }
        
        // if (Input.GetKeyDown(KeyCode.Escape) && game_running)
        // {
        //     isPaused = true; 
        // }
        // TogglePauseScreen(); 

    }
    private void TogglePauseScreen()
    {
        if (isPaused)
        {
            mainMenu.ResumeGameScene();
        }
        else
        {
            mainMenu.PauseGameScene();
        }
        pauseScreen.SetActive(!pauseScreen.activeSelf);
    }
    private IEnumerator InitializeCustomerRoutine()
    {
        while (game_running) // Loop only while the game is running
        {
            customerPool.SpawnCustomer(); // Call the method
            Debug.Log("Spawn Customer called");
            yield return new WaitForSeconds(6f); // Wait for 'interval' seconds
        }
    }

    public void StopGame()
    {
        game_running = false;
        StopCoroutine(InitializeCustomerRoutine()); // Stops the coroutine
        currentDay += 1;
        UIManager.Instance.UpdateDayDisplay();
        Debug.Log("CURRENT DAY IS: " + currentDay);
        // Clear all ingredients in the game
        // GameObject[] ingredients = GameObject.FindGameObjectsWithTag("ingredients");
        // foreach (GameObject i in ingredients)
        // {
        //     Destroy(i);
        // }
        mainMenu.ShowMainMenu();
    }
    
    void Start()
    {
        UIManager.Instance.UpdateDayDisplay();
        mainMenu = FindObjectOfType<MainMenu>();
        //customerPool = GetComponent<CustomerPool>(); // Assumes CustomerPool is attached to the same GameObject
        // customerPool.InitializePool(customerPool.maxPoolSize);
        //Bind to button
        //StartGame();
        // StartCoroutine(InitializeCustomerRoutine());
    }

    public void DestroyAll()
{
    List<GameObject> items = new List<GameObject>();

    // Add objects with each tag to the List
    items.AddRange(GameObject.FindGameObjectsWithTag("Ingredients"));
    items.AddRange(GameObject.FindGameObjectsWithTag("Pizza"));
    items.AddRange(GameObject.FindGameObjectsWithTag("Burrito"));
    items.AddRange(GameObject.FindGameObjectsWithTag("Doughnut"));

    // If you need an array instead of a List, you can convert the List to an array
    GameObject[] itemsArray = items.ToArray();

    foreach (GameObject item in items)
    {
        Destroy(item); // Destroy each item
    }

    GameManager.Instance.airQuantity = 0;
    GameManager.Instance.windQuantity = 0;
    GameManager.Instance.sunlightQuantity = 0;
}
    public void StartGame()
    {
        customerPool.DestroyAllCustomerInstances();
        customerPool.InitializePool(customerPool.maxPoolSize);
        game_running = true;
        timer.timerIsRunning = true;
        timer.resetTimer();
        UIManager.Instance.ShowTimer(); 
        StartCoroutine(InitializeCustomerRoutine());
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