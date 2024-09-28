using UnityEngine;

public class CustomerSpecialA : Customer
{
    // private bool hasDroppedCoin = false; // To ensure the coin is dropped only once
    [Range(0,10)][SerializeField] int coinMultiplier = 2; 
    public float orderTime = 5f;  
    private float stallTime = 5f;
    private bool isOrderComplete = false;
    private bool customerClicked = false;
    // private void Update()
    // {
    //     if (!isOrderServed)
    //     {
    //         // The customer waits as usual until the order is served
    //     }
    //     else
    //     {
    //         MoveTowardsExit();
    //     }
    // }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        Dish servedDish = other.GetComponent<Dish>();

        if (servedDish != null && isOrderComplete == false)
        {
            JudgeOrder(servedDish);
        }
    }
    // Override JudgeOrder to modify what happens when the order is served
    public override void JudgeOrder(Dish servedDish)
    {
        // if (servedDish == null || servedDish.dishData == null)
        // {
        //     Debug.LogError("Served dish or its dish data is null.");
        //     return;
        // }

        // int index = currentOrder.orderedDishes.IndexOf(servedDish.dishData);
        // if (index != -1 && currentOrder.orderedDishes[index] && !leaving)
        // {
        //     // Correct dish served
        //     currentOrder.orderedDishes.Remove(currentOrder.orderedDishes[index]);
        //     Destroy(servedDish.gameObject);
        //     // Debug.Log($"Dish served: {servedDish.dishData.dishName}, Remaining: {currentOrder.dishQuantities[index]}");
        //     UpdateOrderDisplay(servedDish.dishData);

        //     // print("Revenue = " + CalculateTotalOrderPrice());

        //     // Check if all dishes have been served
        //     bool isOrderComplete = true;
        //     if (currentOrder.orderedDishes.Count > 0)
        //         isOrderComplete = false;

        //     if (isOrderComplete)
        //     {
        //         Debug.Log("Service complete");
        //         isOrderServed = true;
        //         HideOrder();
        //         SpawnCoin(currentOrder.orderPrice);
        //         print("Money = " + currentOrder.orderPrice);
        //         MoveTowardsExit();
        //         FindObjectOfType<CustomerPool>().CustomerLeftSeat(seatNumber);
        //     }
        // }
        // else
        // {
        //     Debug.Log($"Rejecting Order");
        //     RejectOrder(servedDish.gameObject);
        // }
        if (servedDish == null || servedDish.dishData == null)
        {
            Debug.LogError("Served dish or its dish data is null.");
            return;
        }

        int index = currentOrder.orderedDishes.IndexOf(servedDish.dishData);
        if (index != -1 && currentOrder.orderedDishes[index] && !leaving)
        {
            // Correct dish served
            currentOrder.orderedDishes.Remove(currentOrder.orderedDishes[index]);
            Destroy(servedDish.gameObject);
            // Debug.Log($"Dish served: {servedDish.dishData.dishName}, Remaining: {currentOrder.dishQuantities[index]}");
            UpdateOrderDisplay(servedDish.dishData);

            // print("Revenue = " + CalculateTotalOrderPrice());

            // Check if all dishes have been served
            isOrderComplete = true;
            if (currentOrder.orderedDishes.Count > 0)
                isOrderComplete = false;

            if (isOrderComplete)
            {
                stopFilling = true;
                Debug.Log("TESTING");
                HideItems();
                sadImage.SetActive(true);
                if (stallTime == 0f)
                {
                    stallTime = Time.time;
                }
                while (Time.time - stallTime > orderTime)
                {
                    Debug.Log("loop");
                    if (customerClicked == true)
                        return ;
                }
                isOrderServed = true;
                sadImage.SetActive(false);
                smileyImage.SetActive(true);
                HideItems();
                Debug.Log("Successfully ran away");
                MoveTowardsExit();
                FindObjectOfType<CustomerPool>().CustomerLeftSeat(seatNumber);
                return ;
                // else if (Input.GetMouseButtonDown(0))
                // {
                //     isOrderServed = true;
                //     HideItems();
                //     SpawnCoin(currentOrder.orderPrice * coinMultiplier);
                //     print("Money = " + currentOrder.orderPrice);
                //     MoveTowardsExit();
                //     FindObjectOfType<CustomerPool>().CustomerLeftSeat(seatNumber);
                //     return ;
                // }
            }
        }
        else
        {
            // Debug.Log($"Rejecting Order | index: {index} | dish quan: {currentOrder.dishQuantities[index]}");
            RejectOrder(servedDish.gameObject);
        }
    }

    private void HideItems()
    {
        dishImage1.SetActive(false);
        dishImage2.SetActive(false);
        dishImage3.SetActive(false);
        burritoText.enabled = false;
        pizzaText.enabled = false;
        doughnutText.enabled = false;
        fillImage.enabled = false;
        fillImageBg.enabled = false;
    }
    private void OnMouseDown()
    {
        // This is called when the customer is clicked
        // if (isOrderServed && !hasDroppedCoin)
        // {
        //     // The customer has reached the exit, and the player clicks on them
        //     SpawnCoin(currentOrder.dish.price * coinMultiplier); // Drop double the money
        //     hasDroppedCoin = true;
        // }
        Debug.Log("clicked");
        if (isOrderComplete == true && isOrderServed == false)
        {
            customerClicked = true;
            isOrderServed = true;
            HideItems();
            SpawnCoin(currentOrder.orderPrice * coinMultiplier);
            print("Money = " + currentOrder.orderPrice);
            MoveTowardsExit();
            FindObjectOfType<CustomerPool>().CustomerLeftSeat(seatNumber);
        }
    }

    // private void MoveTowardsExit()
    // {
    //     // Logic for moving the customer towards the exit
    //     transform.position = Vector3.MoveTowards(transform.position, exitPoint.position, Time.deltaTime * 2f);

    //     if (Vector3.Distance(transform.position, exitPoint.position) < 0.1f)
    //     {
    //         // Destroy the customer object when they reach the exit
    //         Destroy(gameObject);
    //     }
    // }

    // private void SpawnCoin(float value)
    // {
    //     // Instantiate the coin prefab at the customer's position
    //     GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);

    //     // Set the coin's value (double the original price)
    //     Coin coinScript = coin.GetComponent<Coin>();
    //     if (coinScript != null)
    //     {
    //         coinScript.value = value;
    //     }
    //     else
    //     {
    //         Debug.LogError("Coin prefab does not have a Coin component.");
    //     }
    // }
}
