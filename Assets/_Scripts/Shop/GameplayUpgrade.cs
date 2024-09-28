// Abstract class for gameplay-altering upgrades
using UnityEngine;

public abstract class GameplayUpgrade : ScriptableObject, IShopUpgrade
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public bool IsPurchased { get; set; } = false;

    public abstract void ApplyUpgrade();
}