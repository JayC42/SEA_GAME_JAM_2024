using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop", menuName = "Upgrades/Gameplay/WindMachine")]
public class WindMachineUnlock : GameplayUpgrade
{
    public override void ApplyUpgrade()
    {
        Debug.Log("Wind Machine unlocked!");
    }
}
