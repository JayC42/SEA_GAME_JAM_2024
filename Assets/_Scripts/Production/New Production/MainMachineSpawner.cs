using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMachineSpawner : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image fillImage;
    public float fillDuration = 3f;
    public int itemAAmount = 3;
    public int itemBAmount = 3;

    public GameObject itemAPrefab;
    public GameObject itemBPrefab;

    public List<GeneralHolder> itemAHolders;
    public List<GeneralHolder> itemBHolders;

    private bool isHolding = false;
    private float currentFillAmount = 0f;

    private void Start()
    {
        Debug.Log("MachineFiller script started");
        if (fillImage == null)
        {
            Debug.LogError("Fill Image is not assigned!");
        }
    }

    private void Update()
    {
        if (isHolding)
        {
            Debug.Log("Holding: " + currentFillAmount);
            currentFillAmount += Time.deltaTime / fillDuration;
            if (fillImage != null)
            {
                fillImage.fillAmount = currentFillAmount;
            }

            if (currentFillAmount >= 1f)
            {
                ProduceItems();
                ResetFill();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer down detected");
        isHolding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Pointer up detected");
        isHolding = false;
        ResetFill();
    }

    private void ResetFill()
    {
        currentFillAmount = 0f;
        if (fillImage != null)
        {
            fillImage.fillAmount = 0f;
        }
        //Debug.Log("Fill reset");
    }

    private void ProduceItems()
    {
        //Debug.Log("Producing items");
        SpawnItems(itemAPrefab, itemAAmount, itemAHolders, "A");
        SpawnItems(itemBPrefab, itemBAmount, itemBHolders, "B");
    }

    private void SpawnItems(GameObject prefab, int amount, List<GeneralHolder> holders, string itemType)
    {
        for (int i = 0; i < amount; i++)
        {
            GeneralHolder availableHolder = FindAvailableHolder(holders);
            if (availableHolder != null)
            {
                GameObject newItem = Instantiate(prefab, availableHolder.transform.position, Quaternion.identity);
                availableHolder.AddItems(newItem);
                Debug.Log($"Spawned item {itemType} in holder {holders.IndexOf(availableHolder)}");
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
