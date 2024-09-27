using UnityEngine;
using System.Collections.Generic;

public class GameOrderManager : MonoBehaviour
{
    private int remainingBurritos = 15;
    private int remainingPizzas = 15;
    private int remainingDoughnuts = 20;

    private List<Order> allOrders = new List<Order>();
    
    // Initialize 10 customer orders
    public void InitializeOrdersForTenCustomers()
    {
        for (int i = 0; i < 10; i++)
        {
            Order newOrder = GenerateCustomerOrder();
            allOrders.Add(newOrder);
        }
    }

    private Order GenerateCustomerOrder()
    {
        Order order = new Order();

        // Check if the upgrade has been purchased
        bool isUpgradePurchased = PlayerInventory.Instance.IsCustomerMaxQuantityUpgradePurchased();

        // Set maximum orders based on whether the upgrade is purchased
        int maxBurritos = isUpgradePurchased ? 25 : 15; // Max burritos for 16 or 10 customers
        int maxPizzas = isUpgradePurchased ? 25 : 15;   // Max pizzas for 16 or 10 customers
        int maxDoughnuts = isUpgradePurchased ? 30 : 20; // Max doughnuts for 16 or 10 customers

        // Initialize remaining quantities
        int remainingBurritos = maxBurritos; 
        int remainingPizzas = maxPizzas;  
        int remainingDoughnuts = maxDoughnuts; 

        // Step 1: Add at least 1 burrito to each order
        int burritosOrdered = Mathf.Min(remainingBurritos, Random.Range(1, 4)); // (1 - 3 burritos)
        order.AddDish("burrito", burritosOrdered);
        remainingBurritos -= burritosOrdered;

        // Step 2: Optionally add pizzas (0 - 2)
        if (remainingPizzas > 0)
        {
            int pizzasOrdered = Mathf.Min(remainingPizzas, Random.Range(0, 3)); // (0 - 2 pizzas)
            order.AddDish("pizza", pizzasOrdered);
            remainingPizzas -= pizzasOrdered;
        }

        // Step 3: Optionally add doughnuts (0 - 3)
        if (remainingDoughnuts > 0)
        {
            int doughnutsOrdered = Mathf.Min(remainingDoughnuts, Random.Range(0, 4)); // (0 - 3 doughnuts)
            order.AddDish("doughnut", doughnutsOrdered);
            remainingDoughnuts -= doughnutsOrdered;
        }

        return order;
    }


}
