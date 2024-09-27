using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public int money;
    public float price_multiplier = 1;
    public bool auto_maker_item = false;
    public bool increase_quantity_item = false;

    private bool _decorationItem = false;
    public bool decoration_item
    {
        get { return _decorationItem; }
        set
        {
            if (_decorationItem != value) // Check if the value is actually changing
            {
                _decorationItem = value;
                UpdateDecorationPrice(); // Call the function when the value changes
            }
        }
    }
    void Start()
    {
        money = 3;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (decoration_item == true)

    // }
    void UpdateDecorationPrice()
    {
        if (_decorationItem == false)
            price_multiplier = 1f;
        else
            price_multiplier = 1.5f;
        Debug.Log("Price multiplier is now" + price_multiplier);
    }
}
