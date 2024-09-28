using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wind Machine Unlock", menuName = "Shop/Wind Machine Unlock")]
public class WindMachineUnlock : ShopItem
{
    public override void ApplyEffect()
    {
        //PlayerInventory.Instance.maxDishCount++;
        Debug.Log($"Dish count increased to {PlayerInventory.Instance.maxDishCount}");
    }

    public override void ReverseEffect()
    {
        //PlayerInventory.Instance.maxDishCount--;
        Debug.Log($"Dish count decreased to {PlayerInventory.Instance.maxDishCount}");
    }
}
