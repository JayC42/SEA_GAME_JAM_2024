using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop", menuName = "Upgrades/Stats/ Store Decoration Upgrade")]
public class ShopDecorationUpgrade : StatUpgrade
{
    public override void ApplyUpgrade()
    {
        Debug.Log($"Shop decoration activated");
    }
}
