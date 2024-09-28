using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop", menuName = "Upgrades/Stats/Wind Production Quantity")]
public class WindProductionQuantityUpgrade : StatUpgrade
{
    public override void ApplyUpgrade()
    {
        Debug.Log($"Wind production quantity increased by x3");
    }
}


