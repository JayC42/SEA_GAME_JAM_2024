using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralHolder : MonoBehaviour
{
    public int maxItems = 10;
    public float itemSpacing = 0.5f;
    public Vector3 itemOffset = new Vector3(0, 0.5f, 0);

    public  List<GameObject> heldItems;

    public bool CanAddItem()
    {
        // Debug.Log(heldItems.Count);
        // if (heldItems[0].name == "Ingredient (C)(Clone)")
        Debug.Log("wee no held: " + heldItems.Count + "max items: " + maxItems);
        return heldItems.Count < maxItems;
    }

    public bool CanAddItemSun(int count)
    {
        return count < maxItems;
    }

    public void AddItems(GameObject item)
    {
        if (CanAddItem() && item != null)
        {
            item.transform.SetParent(transform);
            heldItems.Add(item);
            PositionItems(item);
        }
        else
        {
            Debug.LogWarning("Cannot add item to holder: " + (item == null ? "Item is null" : "Holder is full"));
        }
    }

    private void PositionItems(GameObject item)
    {
        int tmp = 0;
        for (int i = heldItems.Count - 1; i >= 0; i--) // Iterate backward
        {
            if (heldItems[i] == null)
            {
                tmp = i;
                heldItems.RemoveAt(i);
                Debug.Log("wee REMOVED cur count: " + heldItems.Count);
            }
        }

        Vector3 position = transform.position + itemOffset;
        position.x += ((tmp - (heldItems.Count - 1) / 2f) * itemSpacing);
        item.transform.position = position;
        //     break;
        // }
    }

    public void ClearThing()
    {
        for (int i = heldItems.Count - 1; i >= 0; i--) // Iterate backward
        {
            if (heldItems[i] == null)
            {
                heldItems.RemoveAt(i);
                Debug.Log("wee REMOVED cur count: " + heldItems.Count);
            }
        }
    }

    public void RemoveItem()
    {
        if (heldItems.Count > 0)
        {
            GameObject itemToRemove = heldItems[0]; // Remove the first item
            heldItems.RemoveAt(0);
            Destroy(itemToRemove); // Destroy the GameObject
        }
        else
        {
            Debug.LogWarning("No items to remove!");
        }
    }
}
