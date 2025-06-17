using System.Collections.Generic;
using UnityEngine;

public interface IShopUpgrade
{
    string Name { get; }
    string Description { get; }
    int Cost { get; }
    bool IsPurchased { get; set; }

    void ApplyUpgrade();
}