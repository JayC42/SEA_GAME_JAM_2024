using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop/Shop Item")]
public class ShopItem : ScriptableObject
{
    public GameManager gameManager;
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
                gameManager.sunlightDouble = true;
                Debug.Log($"{itemName} + 1");
                break;
            case "V2 Wind Energy Machine Module":
                gameManager.windDouble = true;
                Debug.Log($"{itemName}");
                break;
            case "V2 Air Purification Machine Module":
                gameManager.airDouble = true;
                Debug.Log($"{itemName}");
                break;
            case "Auto Solar Collection Chip":
                gameManager.sunlightAuto = true;
                Debug.Log($"{itemName}");
                break;
            case "Auto Wind Collection Chip":
                gameManager.windAuto = true;
                Debug.Log($"{itemName}");
                break;
            case "Auto Air Collection Chip":
                gameManager.airAuto = true;
                Debug.Log($"{itemName}");
                break;
            case "Small Paint Bucket":
                gameManager.coinMultiplier += 0.5f;
                Debug.Log($"{itemName}");
                break;
            case "Large Paint Bucket":
                gameManager.coinMultiplier += 1f;
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
                gameManager.sunlightAuto = false;
                break;
            case "Auto Wind Collection Chip":
                gameManager.windAuto = false;
                Debug.Log($"{itemName}");
                break;
            case "Auto Air Collection Chip":
                gameManager.airAuto = false;
                Debug.Log($"{itemName}");
                break;
            case "Small Paint Bucket":
                gameManager.coinMultiplier -= 0.5f;
                Debug.Log($"{itemName}");
                break;
            case "Large Paint Bucket":
                gameManager.coinMultiplier -= 1f;
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