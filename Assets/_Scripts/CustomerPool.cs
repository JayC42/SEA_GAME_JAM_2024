using UnityEngine;
using System.Collections.Generic;

public class CustomerPool : MonoBehaviour
{
    [Header("Customer Settings")]
    public GameObject customerPrefab;

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

    public void InitializePool(int totalCustomers)
    {
        totalCustomers = Mathf.Clamp(totalCustomers, 0, maxPoolSize); // Ensure it doesn't exceed max size
        for (int i = 0; i < totalCustomers; i++)
        {
            GameObject customer = Instantiate(customerPrefab);
            customer.SetActive(false);
            pool.Add(customer);
        }
    }

    public void SpawnCustomer()
    {
        foreach (GameObject customer in pool)
        {
            if (!customer.activeInHierarchy)
            {
                customer.SetActive(true);
                customer.transform.position = GetRandomSpawnPosition(); // Define your spawn position logic here
                break;
            }
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

    private Vector3 GetRandomSpawnPosition()
    {
        // Return a random position based on your game's layout
        return new Vector3(Random.Range(-5f, 5f), 0, 0); // Example position range
    }
}
