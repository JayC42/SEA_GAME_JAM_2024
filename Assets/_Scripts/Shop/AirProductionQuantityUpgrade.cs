using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop", menuName = "Upgrades/Stats/Air Production Quantity")]
public class AirProductionQuantityUpgrade : StatUpgrade
{
    public override void ApplyUpgrade()
    {
        Debug.Log($"Air production quantity increased by x3");
    }
}
