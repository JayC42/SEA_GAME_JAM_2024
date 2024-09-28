using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop/Shop Item")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public string description;
    public int price;
    public bool isBought;
     public int Cost => price;
    public bool IsPurchased { get => isBought; set => isBought = value; }
    // Apply the correct effect based on the item name
    public virtual void ApplyEffect()
    {
        //Debug.Log($"{itemName} effect applied!");
        // Implement the effect of buying this item
        switch (itemName)
        {
            case "Wind Energy Machine":
                //PlayerInventory.Instance.maxDishCount += 1;
                Debug.Log($"{itemName}: Increased max dish count. New count: {PlayerInventory.Instance.maxDishCount}");
                break;
                
            case "Pure Air Energy Canister":
                //PlayerInventory.Instance.PurchaseCustomerMaxQuantityUpgrade();
                Debug.Log($"{itemName} + 1");
                break;
            case "V2 Solar Energy Machine Module":
                Debug.Log($"{itemName} + 1");
                break;
            case "V2 Wind Energy Machine Module":
                Debug.Log($"{itemName}");
                break;
            case "V2 Air Purification Machine Module":
                Debug.Log($"{itemName}");
                break;
            case "Auto Solar Collection Chip":
                Debug.Log($"{itemName}");
                break;
            case "Auto Wind Collection Chip":
                Debug.Log($"{itemName}");
                break;
            case "Auto Air Collection Chip":
                Debug.Log($"{itemName}");
                break;
            case "Small Paint Bucket":
                Debug.Log($"{itemName}");
                break;
            case "Large Paint Bucket":
                Debug.Log($"{itemName}");
                break;
            case "Loud Speaker":
                Debug.Log($"{itemName}");
                break;
            // Add more cases for other items
            default:
                Debug.Log($"{itemName}: No specific effect applied.");
                break;
        }
    }
    public virtual void ReverseEffect()
    {
        //Debug.Log($"{itemName} effect applied!");
        // Implement the effect of buying this item
        switch (itemName)
        {
            case "Wind Energy Machine":
                //PlayerInventory.Instance.maxDishCount += 1;
                Debug.Log($"{itemName} - 1");
                break;
                
            case "Pure Air Energy Canister":
                //PlayerInventory.Instance.PurchaseCustomerMaxQuantityUpgrade();
                Debug.Log($"{itemName} - 1");
                break;
            case "V2 Solar Energy Machine Module":
                Debug.Log($"{itemName}");
                break;
            case "V2 Wind Energy Machine Module":
                Debug.Log($"{itemName}");
                break;
            case "V2 Air Purification Machine Module":
                Debug.Log($"{itemName}");
                break;
            case "Auto Solar Collection Chip":
                Debug.Log($"{itemName}");
                break;
            case "Auto Wind Collection Chip":
                Debug.Log($"{itemName}");
                break;
            case "Auto Air Collection Chip":
                Debug.Log($"{itemName}");
                break;
            case "Small Paint Bucket":
                Debug.Log($"{itemName}");
                break;
            case "Large Paint Bucket":
                Debug.Log($"{itemName}");
                break;
            case "Loud Speaker":
                Debug.Log($"{itemName}");
                break;
            // Add more cases for other items
            default:
                Debug.Log($"{itemName}: No specific effect applied.");
                break;
        }
    }
}
