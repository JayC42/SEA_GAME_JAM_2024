using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientClicker : MonoBehaviour
{
    private int clickCount = 0;
    public GameObject itemCPrefab;
    public int itemCAmount = 1;

    public List<GeneralHolder> itemCHolders;

    private void OnMouseDown()
    {
        if(clickCount < 3)
        {
            clickCount++;

            ProduceItems();
        }
    }

    private void ProduceItems()
    {
        SpawnItems(itemCPrefab, itemCAmount, itemCHolders);
    }

    private void SpawnItems(GameObject prefab, int amount, List<GeneralHolder> holders)
    {
        for (int i = 0; i < amount; i++)
        {
            GeneralHolder availableHolder = FindAvailableHolder(holders);
            if (availableHolder != null)
            {
                GameObject newItem = Instantiate(prefab, availableHolder.transform.position, Quaternion.identity);
                availableHolder.AddItems(newItem);
            }
            else
            {
                //Instantiate(itemBPrefab, transform.position + Random.insideUnitSphere, Quaternion.identity);
                Debug.LogWarning("No available holders for new item");
                break;
            }
        }
    }

    private GeneralHolder FindAvailableHolder(List<GeneralHolder> holders)
    {
        foreach (GeneralHolder holder in holders)
        {
            if (holder.CanAddItem())
            {
                return holder;
            }
        }
        return null;
    }



}
