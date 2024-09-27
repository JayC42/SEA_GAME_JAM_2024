using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton instance
    public int currentLevel = 1;
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

    void Start()
    {
        //customerPool = GetComponent<CustomerPool>(); // Assumes CustomerPool is attached to the same GameObject
        //StartGame();
    }

    void StartGame()
    {
        InitializeCustomerPool();
        InvokeRepeating("SpawnCustomer", 2f, customerPool.GetSpawnRate(currentLevel));
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

    void SpawnCustomer()
    {
        customerPool.SpawnCustomer();
    }
}