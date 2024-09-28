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
        refundButton.onClick.AddListener(RefundSelectedItem);
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
            bool canAfford = MoneyManager.Instance.TotalCoins >= selectedItem.price;

            purchaseButton.gameObject.SetActive(!selectedItem.isBought);
            refundButton.gameObject.SetActive(selectedItem.isBought);

            purchaseButton.interactable = !selectedItem.isBought && canAfford;
            if (!canAfford)
            {
                purchaseButton.GetComponent<Image>().color = Color.gray;
            }
            else
            {
                purchaseButton.GetComponent<Image>().color = Color.white;
            }
        }
        else
        {
            itemNameText.text = "Select an item";
            descriptionText.text = "";
            purchaseButton.gameObject.SetActive(false);
            refundButton.gameObject.SetActive(false);
        }
    }

    private void PurchaseSelectedItem()
    {
        if (selectedItem != null && !selectedItem.isBought && MoneyManager.Instance.TotalCoins >= selectedItem.price)
        {
            MoneyManager.Instance.RemoveCoins(selectedItem.price);
            selectedItem.isBought = true;
            selectedItem.ApplyEffect();
            PlayerInventory.Instance.ApplyUpgrade(selectedItem);
            UpdateItemDisplay();
        }
    }
    private void RefundSelectedItem()
    {
        if (selectedItem != null && selectedItem.isBought)
        {
            MoneyManager.Instance.AddCoins(selectedItem.price);
            selectedItem.isBought = false;
            PlayerInventory.Instance.RemoveUpgrade(selectedItem);
            UpdateItemDisplay();
        }
    }

    public List<ShopItem> GetAvailableItems()
    {
        return availableItems;
    }
}
