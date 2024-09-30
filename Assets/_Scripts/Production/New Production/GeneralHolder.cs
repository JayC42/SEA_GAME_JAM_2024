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
        return heldItems.Count < maxItems;
    }

    public void AddItems(GameObject item)
    {
        if (CanAddItem() && item != null)
        {
            item.transform.SetParent(transform);
            heldItems.Add(item);
            PositionItems();
        }
        else
        {
            Debug.LogWarning("Cannot add item to holder: " + (item == null ? "Item is null" : "Holder is full"));
        }
    }

    private void PositionItems()
    {
        for (int i = heldItems.Count - 1; i >= 0; i--)
        {
            if (heldItems[i] == null)
            {
                heldItems.RemoveAt(i);
                continue;
            }

            Vector3 position = transform.position + itemOffset;
            position.x += ((i - (heldItems.Count - 1) / 2f) * itemSpacing);
            heldItems[i].transform.position = position;
            break;
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
