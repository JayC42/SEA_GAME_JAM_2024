using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindCanSpawner : MonoBehaviour
{
    private int clickCount = 0;
    public int itemCAmount = 3;
    public GameObject itemPrefab; // Prefab for the draggable object

    public List<ProperItemHolder> itemHolders;

    public Image[] clickImages; // Array to hold the 3 images (1 for each click)
    
    public WindCanSpawnerChecker windCanSpawnerChecker;

    private void Start()
    {
        ResetImages();
    }

    private void OnMouseDown()
    {
        clickCount++;
        UpdateImages(); // Update images based on the current click count

        if (clickCount == 3)
        {
            ProduceItems();
            clickCount = 0; //reset for next batch
            ResetImages(); // Reset the images to be hidden again
        }
    }

    private void ProduceItems()
    {
        SpawnItems(itemPrefab, itemCAmount, itemHolders);

    }

    private void SpawnItems(GameObject prefab, int amount, List<ProperItemHolder> holders)
    {
        for (int i = 0; i < amount; i++)
        {

            ProperItemHolder availableHolder = FindAvailableHolder(holders);
            if (availableHolder != null)
            {
                availableHolder.gameManager.windQuantity += 1;
                GameObject newItem = Instantiate(prefab, availableHolder.transform.position, Quaternion.identity);
                availableHolder.AddItem(newItem);

                //remove sand glass after used to create a wind can
                windCanSpawnerChecker.RemoveUsedSandGlass();
            }
            else
            {
                //Instantiate(itemBPrefab, transform.position + Random.insideUnitSphere, Quaternion.identity);
                Debug.LogWarning("No available holders for new item(SubMachine)");
                break;
            }
        }
    }

    private void UpdateImages()
    {
        // Adjust the alpha (transparency) of the image based on the click count
        for (int i = 0; i < clickImages.Length; i++)
        {
            if (i < clickCount)
            {
                SetImageVisibility(clickImages[i], true); // Show the image
            }
            else
            {
                SetImageVisibility(clickImages[i], false); // Hide the image
            }
        }
    }

    private void ResetImages()
    {
        // Hide all images when resetting the click count
        foreach (Image img in clickImages)
        {
            SetImageVisibility(img, false);
        }
    }

    private void SetImageVisibility(Image image, bool isVisible)
    {
        Color tempColor = image.color;
        tempColor.a = isVisible ? 1f : 0f; // Set alpha to 1 (visible) or 0 (invisible)
        image.color = tempColor;
    }

    private ProperItemHolder FindAvailableHolder(List<ProperItemHolder> holders)
    {
        foreach (ProperItemHolder holder in holders)
        {
            if (holder.gameManager.windQuantity < holder.max_items)
            {
                return holder;
            }
        }
        return null;
    }
}
