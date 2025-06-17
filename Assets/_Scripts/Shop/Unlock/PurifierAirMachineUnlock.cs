using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Purifier Air Machine Unlock", menuName = "Shop/Purifier Air Machine Unlock")]
public class PurifierAirMachineUnlock : ShopItem
{
    public override void ApplyEffect()
    {
        // Unhide the said GameObject 
        //PlayerInventory.Instance.maxDishCount++;
        Debug.Log($"Dish count increased to {PlayerInventory.Instance.maxDishCount}");
    }

    public override void ReverseEffect()
    {
        // Hide the said GameObject 
        //PlayerInventory.Instance.maxDishCount--;
        Debug.Log($"Dish count decreased to {PlayerInventory.Instance.maxDishCount}");
    }
}
