using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescriptionText : MonoBehaviour
{
    public TextMeshProUGUI description_text;
    public MoneyLogic money_logic;
    public string selected_item = "";
    int item_price = 0;
    // Start is called before the first frame update
    void Start()
    {
        description_text.text = "Please pick an item for its description";
    }

    // Update is called once per frame
    // void Update()
    // {
    // }

    public void UpdateString(string item_description)
    {
        description_text.text = item_description;
    }

    public void UpdateSelectedItem(string item_name)
    {
        selected_item = item_name;
    }

    public void UpdateItemPrice(int price)
    {
        item_price = price;
    }
    public void BuyItem()
    {
        if (selected_item == "" || money_logic.money < item_price || IsItemBought() == true)
        {
            Debug.Log("Failed purchase");
            return ;
        }

        SetBuyRefund();
        money_logic.money -= item_price;
        Debug.Log("bought" + selected_item);
        Debug.Log("current money:" + money_logic.money);
    }

    public void RefundItem()
    {
        if (selected_item == "" || IsItemBought() == false)
        {
            Debug.Log("Failed refund");
            return ;
        }

        SetBuyRefund();
        money_logic.money += item_price;
        Debug.Log("refunded" + selected_item);
        Debug.Log("current money:" + money_logic.money);
    }

    bool IsItemBought()
    {
        if (selected_item == "AutoMake")
            return money_logic.auto_maker_item;
        else if (selected_item == "IncreaseQuantity")
            return money_logic.increase_quantity_item;
        else if (selected_item == "Decoration")
            return money_logic.decoration_item;
        else 
            return false;
    }

    void SetBuyRefund()
    {
        if (selected_item == "AutoMake")
            money_logic.auto_maker_item = !money_logic.auto_maker_item;
        else if (selected_item == "IncreaseQuantity")
            money_logic.increase_quantity_item = !money_logic.increase_quantity_item;
        else if (selected_item == "Decoration")
            money_logic.decoration_item = !money_logic.decoration_item;
    }
}
