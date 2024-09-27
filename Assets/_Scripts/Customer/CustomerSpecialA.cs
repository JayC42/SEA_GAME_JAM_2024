using UnityEngine;

public class CustomerSpecialA : Customer
{
    private bool hasDroppedCoin = false; // To ensure the coin is dropped only once
    [Range(0,10)][SerializeField] int coinMultiplier = 2; 
    private void Update()
    {
        if (!isOrderServed)
        {
            // The customer waits as usual until the order is served
        }
        else
        {
            MoveTowardsExit();
        }
    }

    // Override JudgeOrder to modify what happens when the order is served
    public override void JudgeOrder(Dish servedDish)
    {
        if (currentOrder.dish == servedDish.dishData)
        {
            HideDishImages();
            smileyImage.SetActive(true);
            sadImage.SetActive(false);
            isOrderServed = true;
        
            // Move the customer towards the exit, no immediate coin spawn
            // No coin is spawned here; the coin will be spawned on click after reaching the exit
            Destroy(servedDish.gameObject);
        }
        else
        {
            // If the order is wrong, reject the order as usual
            RejectOrder(servedDish.gameObject);
        }
    }

    private void OnMouseDown()
    {
        // This is called when the customer is clicked
        if (isOrderServed && !hasDroppedCoin)
        {
            // The customer has reached the exit, and the player clicks on them
            SpawnCoin(currentOrder.dish.price * coinMultiplier); // Drop double the money
            hasDroppedCoin = true;
        }
    }

    private void MoveTowardsExit()
    {
        // Logic for moving the customer towards the exit
        transform.position = Vector3.MoveTowards(transform.position, exitPoint.position, Time.deltaTime * 2f);

        if (Vector3.Distance(transform.position, exitPoint.position) < 0.1f)
        {
            // Destroy the customer object when they reach the exit
            Destroy(gameObject);
        }
    }

    private void SpawnCoin(float value)
    {
        // Instantiate the coin prefab at the customer's position
        GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);

        // Set the coin's value (double the original price)
        Coin coinScript = coin.GetComponent<Coin>();
        if (coinScript != null)
        {
            coinScript.value = value;
        }
        else
        {
            Debug.LogError("Coin prefab does not have a Coin component.");
        }
    }
}
