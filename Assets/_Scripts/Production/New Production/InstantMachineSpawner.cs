using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantMachineSpawner : MonoBehaviour
{
    public Image fillImage;
    public float fillDuration = 3f;
    private float currentFillAmount = 0f;
    private bool isProducing = false; // To prevent multiple clicks

    public GameObject itemPrefab;
    public List<ProperItemHolder> itemHolders;
    public int itemAmount = 1;


    private void OnMouseDown()
    {
        if (!isProducing)
        {
            StartProduction(); // Start the production process on the first click
        }
    }

    private void Update()
    {
        if (isProducing)
        {
            // Fill the loading bar over time
            currentFillAmount += Time.deltaTime / fillDuration;

            if (fillImage != null)
            {
                fillImage.fillAmount = currentFillAmount;
            }

            // Check if the bar is fully filled
            if (currentFillAmount >= 1f)
            {
                ProduceItems(itemPrefab, itemAmount, itemHolders); // Create the item
                ResetFill(); // Reset the fill bar
            }
        }
    }

    private void StartProduction()
    {
        isProducing = true; // Prevent further clicks during production
    }

    private void ProduceItems(GameObject prefab, int amount, List<ProperItemHolder> holders)
    {
        for (int i = 0; i < amount; i++)
        {
            ProperItemHolder availableHolder = FindAvailableHolder(holders);
            if (availableHolder != null)
            {
                GameObject newItem = Instantiate(prefab, availableHolder.transform.position, Quaternion.identity);
                availableHolder.AddItem(newItem);
            }
            else
            {
                Debug.LogWarning("No available holders for new item (InstantMachine)");
                break;
            }
        }
    }

    private ProperItemHolder FindAvailableHolder(List<ProperItemHolder> holders)
    {
        foreach (ProperItemHolder holder in holders)
        {
            if (holder.CanAddItem())
            {
                return holder;
            }
        }
        return null;
    }

    private void ResetFill()
    {
        currentFillAmount = 0f;
        if (fillImage != null)
        {
            fillImage.fillAmount = 0f;
        }

        isProducing = false; // Allow the player to click again for another production cycle
        
        //Debug.Log("Fill reset");
    }


}
