using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Auto Air Production Upgrade", menuName = "Shop/Auto Air Production Upgrade")]
public class AutoAirProductionUpgrade : ShopItem
{
    public override void ApplyEffect()
    {
        // air machine will automatically produce air bags without needing any clicks
        
    }

    public override void ReverseEffect()
    {
        // air machine will return to producing air bags beginning with a click each time

    }
}
