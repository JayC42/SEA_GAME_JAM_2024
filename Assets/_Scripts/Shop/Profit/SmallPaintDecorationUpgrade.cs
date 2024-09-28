using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Small Paint Decoration Upgrade", menuName = "Shop/Small Paint Decoration Upgrade")]
public class SmallPaintDecorationUpgrade : ShopItem
{
    public override void ApplyEffect()
    {
        // Daily income summary: income +50%. (income x 1.5)
        
    }

    public override void ReverseEffect()
    {
        // Daily Income will return to as it has done before originally

    }
}
