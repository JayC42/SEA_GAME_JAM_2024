using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop/Shop Item")]
public class ShopItem : ScriptableObject
{
    // public GameManager gameManager;
    public string itemName;
    public string description;
    public int price;
    public bool isBought = false;
     public int Cost => price;
    public bool IsPurchased { get => isBought; set => isBought = value; }
    // Apply the correct effect based on the item name
    private void Awake()
    {
        isBought = false;
    }

    private void OnEnable()
    {
        isBought = false; // Automatically reset this value when the asset is loaded
    }
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
                GameManager.Instance.sunlightDouble = true;
                Debug.Log($"{itemName} + 1");
                break;
            case "V2 Wind Energy Machine Module":
                GameManager.Instance.windDouble = true;
                Debug.Log($"{itemName}");
                break;
            case "V2 Air Purification Machine Module":
                GameManager.Instance.airDouble = true;
                Debug.Log($"{itemName}");
                break;
            case "Auto Solar Collection Chip":
                GameManager.Instance.sunlightAuto = true;
                Debug.Log($"{itemName}");
                break;
            case "Auto Wind Collection Chip":
                GameManager.Instance.windAuto = true;
                Debug.Log($"{itemName}");
                break;
            case "Auto Air Collection Chip":
                GameManager.Instance.airAuto = true;
                Debug.Log($"{itemName}");
                break;
            case "Small Paint Bucket":
                GameManager.Instance.coinMultiplier += 0.5f;
                Debug.Log($"{itemName}");
                break;
            case "Large Paint Bucket":
                GameManager.Instance.coinMultiplier += 1f;
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
                GameManager.Instance.sunlightDouble = false;
                break;
            case "V2 Wind Energy Machine Module":
                GameManager.Instance.windDouble = false;
                break;
            case "V2 Air Purification Machine Module":
                GameManager.Instance.airDouble = false;
                break;
            case "Auto Solar Collection Chip":
                GameManager.Instance.sunlightAuto = false;
                break;
            case "Auto Wind Collection Chip":
                GameManager.Instance.windAuto = false;
                Debug.Log($"{itemName}");
                break;
            case "Auto Air Collection Chip":
                GameManager.Instance.airAuto = false;
                Debug.Log($"{itemName}");
                break;
            case "Small Paint Bucket":
                GameManager.Instance.coinMultiplier -= 0.5f;
                Debug.Log($"{itemName}");
                break;
            case "Large Paint Bucket":
                GameManager.Instance.coinMultiplier -= 1f;
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