using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop/Shop Item")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public string description;
    public int price;
    public bool isBought;
    public virtual void ApplyEffect() 
    {
        // Implement the effect of buying this item
        Debug.Log($"{itemName} effect applied!");
    }
}
