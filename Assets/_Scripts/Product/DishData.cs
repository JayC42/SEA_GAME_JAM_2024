using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewDish", menuName = "Restaurant/Dish")]
public class DishData : ScriptableObject
{
    public string dishName;                 // Name of the dish
    public float price;                     // Price of the dish
    public Sprite dishImage;                // Image to represent the dish
    public List<string> mandatoryIngredients; // List of mandatory ingredients
    public List<string> optionalIngredients;  // List of optional ingredients
}
