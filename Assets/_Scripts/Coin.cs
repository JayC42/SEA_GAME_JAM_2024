using UnityEngine;

public class Coin : MonoBehaviour
{
    public float value; // Value of the coin

    private void OnMouseDrag() // Detect when the mouse hovers over the coin
    {
        // Logic to collect the coin
        CollectCoin();
    }

    private void CollectCoin()
    {
        // Update the money in the UIManager
        UIManager.Instance.money += (int)value;
        UIManager.Instance.moneyTxt.text = "Money: $" + UIManager.Instance.money;


        // Destroy the coin after it has been collected
        Destroy(gameObject);
    }
}



