using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Increased Customer Upgrade", menuName = "Shop/Increased Customer Upgrade")]
public class IncreasedCustomerUpgrade : ShopItem
{
    public override void ApplyEffect()
    {
        // Increase the number of customers from 10 -> 16
        
    }

    public override void ReverseEffect()
    {
        // Revert the number of customers back to 10

    }
}
