using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerPool : MonoBehaviour
{
    [Header("Customer Settings")]
    public GameObject customerPrefab;
    public GameObject ACustomerPrefab;
    public Transform entrance; // The entrance where customers will spawn
    public List<Transform> seats = new List<Transform>(); // The seats in the game

    [Range(5, 50)]
    [Tooltip("Maximum number of customers in the pool.")]
    public int maxPoolSize = 20; // Set the default value based on your need

    [Header("Spawn Rate Settings (Seconds)")]
    [Range(1f, 15f)]
    [Tooltip("Spawn rate for easy levels (1-10).")]
    public float easySpawnRate = 10f;

    [Range(1f, 15f)]
    [Tooltip("Spawn rate for medium levels (11-20).")]
    public float mediumSpawnRate = 7f;

    [Range(1f, 15f)]
    [Tooltip("Spawn rate for hard levels (21+).")]
    public float hardSpawnRate = 5f;

    private List<GameObject> pool = new List<GameObject>();
    public GameOrderManager gameOrderManager;
    public GameManager gameManager;
    private int currentSeatIndex = 0; // The index of the current seat
    public List<bool> isSeatOccupied = new List<bool>(); // Whether each seat is occupied

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        // Check if the customer max quantity upgrade is purchased
        if (PlayerInventory.Instance.IsCustomerMaxQuantityUpgradePurchased())
        {
            maxPoolSize = 30; // Adjust this value based on your game's requirements
        }

        //InitializePool(maxPoolSize);
        // InvokeRepeating("SpawnCustomer", 0f, 3f); // Spawn a customer every 3 seconds
        // Ensure seats list is populated correctly
        if (seats.Count == 0)
        {
            Debug.LogError("Seats list is empty! Make sure all seats are assigned.");
        }

        for (int i = 0; i < seats.Count; i++)
        {
            isSeatOccupied.Add(false); // Initialize seat occupancy
        }
        //gameManager.StartGame();
    }

    public void InitializePool(int totalCustomers)
    {
        // totalCustomers = Mathf.Clamp(totalCustomers, 0, maxPoolSize); // Ensure it doesn't exceed max size
        totalCustomers = maxPoolSize; // Ensure it doesn't exceed max size
        for (int i = 0; i < totalCustomers; i++)
        {
            // if (Random.Range(1, 6) != 1)
            // {
            GameObject customer = Instantiate(customerPrefab);
            customer.SetActive(false);
            pool.Add(customer);
            // }
            // else
            // {
            // Debug.Log("WEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
            // GameObject customer = Instantiate(ACustomerPrefab);
            // customer.SetActive(false);
            // pool.Add(customer);
            // }
        }

        // Generate orders for the customers
        gameOrderManager.InitializeAllCustomers(totalCustomers);
    }

    public void SpawnCustomer()
    {
        // Check if there are available seats
        // if (currentSeatIndex < seats.Count && !isSeatOccupied[currentSeatIndex])
        // {
        foreach (GameObject customer in pool)
        {
            if (customer && !customer.activeInHierarchy)
            {
                // customer.SetActive(true);
                // customer.GetComponent<Customer>().EnterRestaurant(entrance); // Enter the restaurant
                // Find the first unoccupied seat and move the customer there
                for (int i = 0; i < seats.Count; i++)
                {
                    if (!isSeatOccupied[i]) // If the seat is not occupied
                    {
                        customer.SetActive(true);
                        customer.GetComponent<Customer>().EnterRestaurant(entrance);
                        customer.GetComponent<Customer>().gameManager = gameManager;
                        customer.GetComponent<Customer>().MoveToSeat(seats[i]); // Move to the available seat
                        isSeatOccupied[i] = true;
                        customer.GetComponent<Customer>().seatNumber = i; // Mark this seat as occupied
                        currentSeatIndex = i; // Set the current seat index to the seat the customer moved to
                        return; // Exit the loop once the customer has been seated
                    }
                }
                Debug.Log("seats full");
                return;
            }
            // }
        }
    }
    public void CustomerLeftSeat(int seat)
    {
        // int seatIndex = seats.IndexOf(seat);
        // Debug.Log("AWUIFGBHJR: " + seatIndex);

        // Ensure seatIndex is valid
        if (seat >= 0 && seat < seats.Count)
        {
            // Mark the seat as unoccupied
            isSeatOccupied[seat] = false;

            // Adjust the current seat index if necessary
            if (currentSeatIndex > seat)
            {
                currentSeatIndex--;
            }
        }
        else
        {
            Debug.LogWarning("Seat not found in the list, cannot unoccupy.");
        }
    }


    public float GetSpawnRate(int level)
    {
        if (level <= 10)
            return easySpawnRate;
        else if (level <= 20)
            return mediumSpawnRate;
        else
            return hardSpawnRate; // Adjust rates as needed for higher levels
    }

    public void DestroyAllCustomerInstances()
    {
        // Find all GameObjects tagged with "Customer"
        GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");

        // Loop through all the found objects and destroy each one
        foreach (GameObject customer in customers)
        {
            Destroy(customer);
        }

        // Debug.Log(customers.Length + " customer(s) destroyed.");
    }
}