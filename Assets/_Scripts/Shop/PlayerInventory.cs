using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    public Dictionary<ShopItem, int> Upgrades = new Dictionary<ShopItem, int>();

    public void ApplyUpgrade(ShopItem upgrade)
    {
        if (!Upgrades.ContainsKey(upgrade))
        {
            Upgrades.Add(upgrade, upgrade.price);
            InitializeUpgradeEffect(upgrade);
            Debug.Log($"Purchased new item: {upgrade.itemName}");
        }
    }

    private void InitializeUpgradeEffect(ShopItem upgrade)
    {
        upgrade.ApplyEffect();
        // Additional logic can be added here if needed
    }
    public void RemoveUpgrade(ShopItem upgrade)
    {
        if (Upgrades.ContainsKey(upgrade))
        {
            Upgrades.Remove(upgrade);
            ReverseUpgradeEffect(upgrade);
        }
    }
    private void ReverseUpgradeEffect(ShopItem upgrade)
    {
        // This method should reverse the effect of the upgrade
        // You might need to implement a new method in ShopItem class for this
        upgrade.ReverseEffect();
        // Additional logic can be added here if needed
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrintPurchasedItems();
        }
    }
    public void PrintPurchasedItems()
    {
        Debug.Log($"Total purchased items: {Upgrades.Count}");
        foreach (var upgrade in Upgrades.Keys)
        {
            Debug.Log($"- {upgrade.itemName}");
        }
    }
}
