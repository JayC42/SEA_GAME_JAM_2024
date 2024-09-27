using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Order
{
    public List<DishData> orderedDishes; // List of ordered dishes
    public List<int> dishQuantities; // the quantity of each dish ordered
    public List<List<string>> requiredIngredientsForEachDish; // Ingredients for each dish
    public DishData dish;
    public Order()
    {
        orderedDishes = new List<DishData>();
        dishQuantities = new List<int>();
    }
    // Add a dish and its quantity to the order
    public void AddDish(string dishName, int quantity)
    {
        if (quantity > 0)
        {
            DishData dish = DishesManager.Instance.GetDishByName(dishName);
            for (int i = 0; i < quantity; i++)
            {
                orderedDishes.Add(dish);
            }
            dishQuantities.Add(quantity);
        }
    }
    public Order(DishesManager dishesManager, PlayerInventory playerInventory)
    {
        int dishCount = DetermineDishCount(playerInventory); // Determine dish count based on purchased upgrades
        orderedDishes = new List<DishData>(dishCount);
        requiredIngredientsForEachDish = new List<List<string>>(dishCount);

        for (int i = 0; i < dishCount; i++)
        {
            DishData dish = dishesManager.GetRandomDish();
            orderedDishes.Add(dish);
            requiredIngredientsForEachDish.Add(GenerateOrderIngredients(dish));
        }
    }
    
    private int DetermineDishCount(PlayerInventory playerInventory)
    {
        // Get the maximum number of dishes based on purchased upgrades
        int maxDishCount = playerInventory.GetDishUpgradeCount();

        // Randomly decide the number of dishes the customer will order (between 1 and maxDishCount)
        return Random.Range(1, maxDishCount + 1);
    }

    private List<string> GenerateOrderIngredients(DishData dish)
    {
        List<string> ingredients = new List<string>(dish.mandatoryIngredients);

        foreach (string ingredient in dish.optionalIngredients)
        {
            if (Random.value > 0.5f)
            {
                ingredients.Add(ingredient);
            }
        }

        if (ingredients.Count == dish.mandatoryIngredients.Count && dish.optionalIngredients.Count > 0)
        {
            ingredients.Add(dish.optionalIngredients[Random.Range(0, dish.optionalIngredients.Count)]);
        }

        return ingredients;
    }

    public bool ValidateOrder(DishData servedDish)
    {
        foreach (var dish in orderedDishes)
        {
            if (servedDish == dish)
            {
                return true;
            }
        }
        return false;
    }
    // Method to get quantity of a specific dish
    public int GetDishQuantity(DishData dish)
    {
        // Example implementation; adjust according to your order structure
        return orderedDishes.Count(d => d == dish); // Count occurrences of the dish
    }
}
