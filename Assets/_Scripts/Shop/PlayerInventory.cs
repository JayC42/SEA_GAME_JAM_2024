using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; } // Singleton instance
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
    //public int dishUpgradeCount = 1; 
    public int maxDishCount = 3; // Start with 1 dish available by default
    private bool purchaseCustomerMaxQuantityUpgrade = false;   // Track if the upgrade has been purchased

    // This method increments the number of dish upgrades the player has purchased
    public void PurchaseDishUpgrade()
    {
        // For example, a player could purchase up to 3 upgrades
        maxDishCount = Mathf.Clamp(maxDishCount + 1, 1, 3);
        Debug.Log("Dish upgrade purchased. Current available dishes: " + maxDishCount);
    }

    // Optionally, you could refund a dish upgrade
    public void RefundDishUpgrade()
    {
        maxDishCount = Mathf.Clamp(maxDishCount - 1, 1, 3);
        Debug.Log("Dish upgrade refunded. Current available dishes: " + maxDishCount);
    }

    // Retrieve the current dish upgrade count
    public int GetDishUpgradeCount()
    {
        return maxDishCount;
    }
    public void PurchaseCustomerMaxQuantityUpgrade()
    {
        purchaseCustomerMaxQuantityUpgrade = true;
        // Optionally, add any effects of the upgrade here (e.g., update UI)
    }

    // Method to check if the upgrade is purchased
    public bool IsCustomerMaxQuantityUpgradePurchased()
    {
        return purchaseCustomerMaxQuantityUpgrade;
    }
}
