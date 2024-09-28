using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Auto Solar Production Upgrade", menuName = "Shop/Auto Solar Production Upgrade")]
public class AutoSolarProductionUpgrade : ShopItem
{
    public override void ApplyEffect()
    {
        // sun machine will automatically start to produce wind when storage falls below 3, no need to start up machine by clicking
        
    }

    public override void ReverseEffect()
    {
        // sun  machine will return to produce wind as it has done before originally

    }
}
