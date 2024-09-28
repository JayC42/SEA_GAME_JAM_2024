using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop", menuName = "Upgrades/Stats/Auto Air Production Up")]
public class AutoAirProductionUpgrade : StatUpgrade
{
    public override void ApplyUpgrade()
    {
        Debug.Log($"Auto AIr production mode activated");
    }
}
