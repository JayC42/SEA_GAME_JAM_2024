using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewIngredientSpawner : MonoBehaviour
{
    public GameObject itemPrefab; // Prefab for the draggable object
    public int itemCAmount = 3;

    public List<GeneralHolder> itemCHolders;

    public WindCanSpawnerChecker windCanSpawnerChecker;

    private void OnMouseDown()
    {
        ProduceItems();
    }

    private void ProduceItems()
    {
        SpawnItems(itemPrefab, itemCAmount, itemCHolders);
        windCanSpawnerChecker.CheckGlassBottle();
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
                Debug.LogWarning("No available holders for new item(NewIngredient)");
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
