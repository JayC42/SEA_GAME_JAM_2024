using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop", menuName = "Upgrades/Gameplay/Purifier Air Machine")]
public class PurifierAirMachineUnlock : GameplayUpgrade
{
    public override void ApplyUpgrade()
    {
        Debug.Log("Purifier Air Machine unlocked!");
    }
}
