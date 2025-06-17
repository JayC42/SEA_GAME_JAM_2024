using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Auto Wind Production Upgrade", menuName = "Shop/Auto Wind Production Upgrade")]
public class AutoWindProductionUpgrade : ShopItem
{
    public override void ApplyEffect()
    {
        // wind machine will automatically start to produce wind when storage falls below 2
        // , no need to start up machine by clicking
    }

    public override void ReverseEffect()
    {
        // wind machine will return to produce wind as it has done before originally

    }
}
