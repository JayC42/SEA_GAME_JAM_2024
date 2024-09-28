using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop", menuName = "Upgrades/Stats/Auto Wind Production Up")]
public class AutoWindProductionUpgrade : StatUpgrade
{
    public override void ApplyUpgrade()
    {
        Debug.Log($"Auto Wind production mode activated");
    }
}
