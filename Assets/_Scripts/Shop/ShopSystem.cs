using UnityEngine;
using UnityEngine.UI; 
using TMPro; 
using System.Collections.Generic;

public class ShopSystem : MonoBehaviour
{
    public static ShopSystem Instance { get; private set; }

    [SerializeField] private List<ShopItem> availableItems;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private Button refundButton;
    private ShopItem selectedItem;
    private PlayerInventory playerInventory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerInventory = PlayerInventory.Instance;
        purchaseButton.onClick.AddListener(PurchaseSelectedItem);
    }

    public void SelectItem(ShopItem item)
    {
        selectedItem = item;
        Debug.Log("Selected item: " + selectedItem.itemName);
        UpdateItemDisplay();
    }

    private void UpdateItemDisplay()
    {
        if (selectedItem != null)
        {
            itemNameText.text = selectedItem.itemName;
            descriptionText.text = selectedItem.description;
            purchaseButton.interactable = !selectedItem.isBought;
        }
        else
        {
            itemNameText.text = "Select an item";
            descriptionText.text = "";
            purchaseButton.interactable = false;
        }
    }

    private void PurchaseSelectedItem()
    {
        if (selectedItem != null && !selectedItem.isBought && MoneyManager.Instance.TotalCoins >= selectedItem.price)
        {
            MoneyManager.Instance.RemoveCoins(selectedItem.price);
            selectedItem.isBought = true;
            selectedItem.ApplyEffect();
            UpdateItemDisplay();
        }
    }

    public List<ShopItem> GetAvailableItems()
    {
        return availableItems;
    }
}
