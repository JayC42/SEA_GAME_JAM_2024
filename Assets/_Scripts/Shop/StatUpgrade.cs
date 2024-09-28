// Abstract class for stat-boosting upgrades
using System.Collections.Generic;
using UnityEngine;

public abstract class StatUpgrade : ScriptableObject, IShopUpgrade
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public bool IsPurchased { get; set; } = false;
    public List<UpgradeLevel> Levels { get; set; } = new List<UpgradeLevel>();

    public abstract void ApplyUpgrade();

    // Helper class to store upgrade levels and their effects
    [System.Serializable]
    public class UpgradeLevel
    {
        public float StatModifier { get; set; }
    }
}