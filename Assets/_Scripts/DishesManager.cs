using System.Collections.Generic;
using UnityEngine;

// Manages a list of available food dishes
public class DishesManager : MonoBehaviour
{
    public static DishesManager Instance { get; private set; } // Singleton instance
    public List<DishData> availableDishes; // List of dishes from ScriptableObjects

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

    // Get a random dish from the list
    public DishData GetRandomDish()
    {
        if (availableDishes.Count == 0)
        {
            Debug.LogError("No dishes available in DishesManager!");
            return null; // or handle this case as needed
        }
        return availableDishes[Random.Range(0, availableDishes.Count)];
    }

    // Get a dish by its name
    public DishData GetDishByName(string dishName)
    {
        // Find and return the dish data by name
        return availableDishes.Find(dish => dish.dishName == dishName);
    }
}
