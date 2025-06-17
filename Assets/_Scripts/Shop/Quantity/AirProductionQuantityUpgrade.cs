using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Air Production Quantity", menuName = "Shop/Air Production Quantity")]
public class AirProductionQuantityUpgrade : ShopItem
{
     public override void ApplyEffect()
    {
        // Increase the production quantity to x3 amount
        
    }

    public override void ReverseEffect()
    {
        // decrease the production quantity to original amount

    }
}
