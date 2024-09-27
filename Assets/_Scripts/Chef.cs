using UnityEngine;

public class Chef : MonoBehaviour
{
    private Dish currentBurrito;

    public void SelectIngredient(Ingredient ingredient)
    {
        currentBurrito.GetComponent<Burrito>().AddIngredient(ingredient);
    }

    public void WrapBurrito()
    {
        currentBurrito.GetComponent<Burrito>().Wrap();
    }

    public void ServeCustomer(Customer customer)
    {
        if (currentBurrito != null)
        {
            customer.JudgeOrder(currentBurrito);
            currentBurrito = null; // Clear the burrito after serving
        }
    }
}
