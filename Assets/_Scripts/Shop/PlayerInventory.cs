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

    #region Shop upgrades
    // Track upgrades and their costs
    public Dictionary<IShopUpgrade, int> Upgrades = new Dictionary<IShopUpgrade, int>();

    // Apply an upgrade to the player's inventory
    public void ApplyUpgrade(IShopUpgrade upgrade)
    {
        if (!Upgrades.ContainsKey(upgrade))
        {
            Upgrades.Add(upgrade, upgrade.Cost);
        }
        else
        {
            Upgrades[upgrade] = upgrade.Cost;
        }

        // Check if the upgrade is purchased
        if (IsUpgradePurchased(upgrade))
        {
            InitializeUpgradeEffect(upgrade);
        }
    }

    // Check if an upgrade is purchased
    public bool IsUpgradePurchased(IShopUpgrade upgrade)
    {
        return Upgrades.ContainsKey(upgrade);
    }
    // Initialize the upgrade effect
    private void InitializeUpgradeEffect(IShopUpgrade upgrade)
    {
        // Update player's stats or game state accordingly
        if (upgrade is PurifierAirMachineUnlock)
        {
            // Unlock machine functionality
        }
        else if (upgrade is WindMachineUnlock)
        {
            // Unlock machine functionality
        }
        if (upgrade is AirProductionQuantityUpgrade)
        {
            // Upgrade AirProductionQuantity functionality
        }
        else if (upgrade is WindProductionQuantityUpgrade)
        {
            // Upgrade WindProductionQuantity functionality
        }
        if (upgrade is AutoAirProductionUpgrade)
        {
            // Upgrade AirProductionQuantity functionality
        }
        else if (upgrade is AutoWindProductionUpgrade)
        {
            // Upgrade WindProductionQuantity functionality
        }
        else if (upgrade is ShopDecorationUpgrade)
        {
            // Upgrade WindProductionQuantity functionality
        }
        else if (upgrade is IncreasedCustomerUpgrade)
        {
            // Upgrade WindProductionQuantity functionality
        }
    }
    // Refund an upgrade
    public void RefundUpgrade(IShopUpgrade upgrade)
    {
        if (Upgrades.ContainsKey(upgrade))
        {
            int refundAmount = Upgrades[upgrade];
            Upgrades.Remove(upgrade);

            // Update player's money
            UIManager.Instance.AddCoins(refundAmount);

            // Update player's stats or game state accordingly
            // Update player's stats or game state accordingly
            if (upgrade is PurifierAirMachineUnlock)
            {
                // Refund machine functionality
            }
            else if (upgrade is WindMachineUnlock)
            {
                // Refund machine functionality
            }
            if (upgrade is AirProductionQuantityUpgrade)
            {
                // Refund AirProductionQuantity functionality
            }
            else if (upgrade is WindProductionQuantityUpgrade)
            {
                // Refund WindProductionQuantity functionality
            }
            if (upgrade is AutoAirProductionUpgrade)
            {
                // Refund AirProductionQuantity functionality
            }
            else if (upgrade is AutoWindProductionUpgrade)
            {
                // Refund WindProductionQuantity functionality
            }
            else if (upgrade is ShopDecorationUpgrade)
            {
                // Refund WindProductionQuantity functionality
            }
            else if (upgrade is IncreasedCustomerUpgrade)
            {
                // Refund WindProductionQuantity functionality
            }
        }
    }
    #endregion
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
