using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProperItemHolder : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public int max_items = 3;
    public GameManager gameManager;
    private Vector3[] itemPositions = new Vector3[]
    {
        new Vector3(-0.2f, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0.2f, 0, 0)
    };

    public bool CanAddItem(GameObject newItem)
    {
        if (newItem.name == "Doughnut(Clone)")
        {
            // Debug.Log("name: " + newItem.name + " | quantity: " + gameManager.airQuantity + " | max items: " + max_items);
            return gameManager.airQuantity < max_items;
        }
        else if (newItem.name == "Burrito(Clone)")
        {
            // Debug.Log("name: " + newItem.name + " | quantity: " + gameManager.windQuantity + " | max items: " + max_items);
            return gameManager.windQuantity < max_items;
        }
        else if (newItem.name == "Pizza(Clone)")
        {
            // Debug.Log("name: " + newItem.name + " | quantity: " + gameManager.sunlightQuantity + " | max items: " + max_items);
            // Debug.Log("can add: " + (gameManager.sunlightQuantity < max_items));
            return gameManager.sunlightQuantity < max_items;
        }
        else
            return false;
    }

    public int GetRemainingSpace()
    {
        return max_items - items.Count;
    }

    public void AddItem(GameObject newItem)
    {
        if (CanAddItem(newItem) == true)
        {
            if (newItem.name == "Doughnut(Clone)")
            {
                gameManager.airQuantity += 1;
                newItem.transform.SetParent(transform);
                newItem.transform.localPosition = itemPositions[gameManager.airQuantity - 1];
                items.Add(newItem);
            }
            else if (newItem.name == "Burrito(Clone)")
            {
                gameManager.windQuantity += 1;
                newItem.transform.SetParent(transform);
                newItem.transform.localPosition = itemPositions[gameManager.windQuantity - 1];
                items.Add(newItem);
            }
            else if (newItem.name == "Pizza(Clone)")
            {
                gameManager.sunlightQuantity += 1;
                newItem.transform.SetParent(transform);
                newItem.transform.localPosition = itemPositions[gameManager.sunlightQuantity - 1];
                items.Add(newItem);
            }
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
