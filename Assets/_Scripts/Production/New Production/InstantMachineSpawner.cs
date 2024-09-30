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

    public GameManager gameManager;
    private float timeSinceLastCall = 0f;

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
                if (gameManager.airDouble == true)
                {
                    ProduceItems(itemPrefab, itemAmount, itemHolders);
                    ProduceItems(itemPrefab, itemAmount, itemHolders);
                }
                ResetFill(); // Reset the fill bar
            }
        }
        else if (gameManager.airAuto == true)
        {
            timeSinceLastCall += Time.deltaTime; // Increment the timer by the time passed since last frame

            if (timeSinceLastCall >= 2f)
            {
                StartProduction(); // Call the function
                timeSinceLastCall = 0f; // Reset the timer
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
            Debug.Log("air quantity: " + holder.gameManager.airQuantity);
            if (holder.gameManager.airQuantity < holder.max_items)
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