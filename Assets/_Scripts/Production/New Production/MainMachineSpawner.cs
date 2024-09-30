using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMachineSpawner : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image fillImage;
    public float fillDuration = 1.5f;
    public int itemAAmount = 0;
    public int itemBAmount = 0;

    public GameObject itemAPrefab;
    public GameObject itemBPrefab;

    public List<GeneralHolder> itemAHolders;
    public List<GeneralHolder> itemBHolders;

    private bool isHolding = false;
    private float currentFillAmount = 0f;

    private void Start()
    {
        // Debug.Log("MachineFiller script started");
        if (fillImage == null)
        {
            Debug.LogError("Fill Image is not assigned!");
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
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
        else
            ResetFill();
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
        SpawnItems(itemAPrefab, 1, itemAHolders, "A");
        SpawnItems(itemBPrefab, 1, itemBHolders, "B");
    }

    private void SpawnItems(GameObject prefab, int amount, List<GeneralHolder> holders, string itemType)
    {
        for (int i = 0; i < amount; i++)
        {
            GeneralHolder availableHolder = FindAvailableHolder(holders, itemType);
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

    private GeneralHolder FindAvailableHolder(List<GeneralHolder> holders, string itemType)
    {
        int count = 0;
        if (itemType == "A")
            count = itemAAmount;
        else
            count = itemBAmount;
        foreach (GeneralHolder holder in holders)
        {
            // if (holder.CanAddItemSun(count))
            // {
            if (holder.CanAddItem())
            {
                return holder;
            }
        }
        return null;
    }
}
