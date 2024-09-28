using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProperItemHolder : MonoBehaviour
{
    private List<GameObject> items = new List<GameObject>();
    public int max_items = 3;
    public GameManager gameManager;
    private Vector3[] itemPositions = new Vector3[]
    {
        new Vector3(-0.2f, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0.2f, 0, 0)
    };

    public bool CanAddItem()
    {
        return items.Count < max_items;
    }

    public int GetRemainingSpace()
    {
        return max_items - items.Count;
    }

    public void AddItem(GameObject newItem)
    {
        if (CanAddItem())
        {
            if (newItem.name == "Doughnut(Clone)")
                gameManager.airQuantity += 1;
            else if (newItem.name == "Burrito(Clone)")
                gameManager.windQuantity += 1;
            else if (newItem.name == "Pizza(Clone)")
                gameManager.sunlightQuantity += 1;
            newItem.transform.SetParent(transform);
            newItem.transform.localPosition = itemPositions[items.Count];
            items.Add(newItem);
        }
        else
        {
            Debug.LogWarning("Cannot add item. Holder is full.");
        }
    }

    public void AddItems(List<GameObject> newItems)
    {
        int availableSpace = GetRemainingSpace();
        int itemsToAdd = Mathf.Min(availableSpace, newItems.Count);
        for (int i = 0; i < itemsToAdd; i++)
        {
            AddItem(newItems[i]);
        }
        if (itemsToAdd < newItems.Count)
        {
            Debug.LogWarning($"Only {itemsToAdd} out of {newItems.Count} items could be added to this holder.");
        }
    }
}
