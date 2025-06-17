using System.Collections.Generic;
using UnityEngine;

public class Burrito : MonoBehaviour
{
    public List<Ingredient> ingredients = new List<Ingredient>();

    public void AddIngredient(Ingredient ingredient)
    {
        if (IsValidIngredient(ingredient))
        {
            ingredients.Add(ingredient);
            // Update UI to show ingredient on tortilla
        }
    }

    public void Wrap()
    {
        // Logic to wrap the burrito
    }

    private bool IsValidIngredient(Ingredient ingredient)
    {
        // Check if the ingredient is valid for the burrito
        return true; // Placeholder
    }

    public void A()
    {
        Debug.Log("Success");
    }
}

