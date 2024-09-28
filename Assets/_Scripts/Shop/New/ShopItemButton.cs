using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour
{
    public ShopItem item;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnButtonClick()
    {
        if (item != null)
        {
            //Debug.Log("Selected item: " + item.itemName);
            ShopSystem.Instance.SelectItem(item);
        }
    }
}
