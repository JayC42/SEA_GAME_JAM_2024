using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop", menuName = "Upgrades/Stats/Increased Customer Quantity")]
public class IncreasedCustomerUpgrade : StatUpgrade
{
    public override void ApplyUpgrade()
    {
        Debug.Log($"Daily earnings increased by %100");
    }
}
