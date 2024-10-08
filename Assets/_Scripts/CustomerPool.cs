using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerPool : MonoBehaviour
{
    [Header("Customer Settings")]
    public GameObject customerPrefab;
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
    private GameOrderManager gameOrderManager;
    private int currentSeatIndex = 0; // The index of the current seat
    public List<bool> isSeatOccupied = new List<bool>(); // Whether each seat is occupied

    private void Start()
    {
        gameOrderManager = FindObjectOfType<GameOrderManager>();

        // Check if the customer max quantity upgrade is purchased
        if (PlayerInventory.Instance.IsCustomerMaxQuantityUpgradePurchased())
        {
            maxPoolSize = 30; // Adjust this value based on your game's requirements
        }

        InitializePool(maxPoolSize);
        InvokeRepeating("SpawnCustomer", 0f, 3f); // Spawn a customer every 3 seconds

        // Ensure seats list is populated correctly
        if (seats.Count == 0)
        {
            Debug.LogError("Seats list is empty! Make sure all seats are assigned.");
        }

        for (int i = 0; i < seats.Count; i++)
        {
            isSeatOccupied.Add(false); // Initialize seat occupancy
        }

    }

    public void InitializePool(int totalCustomers)
    {
        totalCustomers = Mathf.Clamp(totalCustomers, 0, maxPoolSize); // Ensure it doesn't exceed max size
        for (int i = 0; i < totalCustomers; i++)
        {
            GameObject customer = Instantiate(customerPrefab);
            customer.SetActive(false);
            pool.Add(customer);
        }

        // Generate orders for the customers
        gameOrderManager.InitializeOrdersForTenCustomers();
    }

    public void SpawnCustomer()
    {
        // Check if there are available seats
        if (currentSeatIndex < seats.Count && !isSeatOccupied[currentSeatIndex])
        {
            foreach (GameObject customer in pool)
            {
                if (!customer.activeInHierarchy)
                {
                    customer.SetActive(true);
                    customer.GetComponent<Customer>().EnterRestaurant(entrance); // Enter the restaurant
                    // Find the first unoccupied seat and move the customer there
                    for (int i = 0; i < seats.Count; i++)
                    {
                        if (!isSeatOccupied[i]) // If the seat is not occupied
                        {
                            customer.GetComponent<Customer>().MoveToSeat(seats[i]); // Move to the available seat
                            isSeatOccupied[i] = true; // Mark this seat as occupied
                            currentSeatIndex = i; // Set the current seat index to the seat the customer moved to
                            break; // Exit the loop once the customer has been seated
                        }
                        
                    }
                }
            }
        }
    }
    public void CustomerLeftSeat(Transform seat)
    {
        int seatIndex = seats.IndexOf(seat);

        // Ensure seatIndex is valid
        if (seatIndex >= 0 && seatIndex < seats.Count)
        {
            // Mark the seat as unoccupied
            isSeatOccupied[seatIndex] = false;

            // Adjust the current seat index if necessary
            if (currentSeatIndex > seatIndex)
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
}

