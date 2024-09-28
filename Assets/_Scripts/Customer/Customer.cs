using UnityEngine;
using TMPro; 
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public Order currentOrder;
    public PlayerInventory playerInventory;
    public GameObject orderImage; // UI image showing the customer's order
    public GameObject smileyImage; // Smiley feedback image
    public GameObject sadImage; // Sad feedback image

    // Multiple dish image slots for displaying up to 3 dishes
    public GameObject dishImage1;
    public GameObject dishImage2;
    public GameObject dishImage3;
    public TextMeshProUGUI burritoText; // Text display for burritos
    public TextMeshProUGUI pizzaText;    // Text display for pizzas
    public TextMeshProUGUI doughnutText; // Text display for doughnuts

    public Transform exitPoint; // The exit point where the customer will leave
    public GameObject coinPrefab;
    public bool isOrderServed = false;
    public bool stopFilling = false;
    // for patience bar
    public Image fillImage; //UI image type: Filled
    public Image fillImageBg; //UI image type: Filled
    public float fillDuration = 5f;
    private float currentFillAmount = 0f;
    protected bool leaving = false;
    private bool timer_start = false;
    public int seatNumber;
    // public float pauseTime = 5f;

    protected void Awake()
    {
        exitPoint = GameObject.FindGameObjectWithTag("ExitPoint").transform;
        DishesManager dishesManager = FindObjectOfType<DishesManager>();
        playerInventory = FindObjectOfType<PlayerInventory>(); // Get player inventory

        // HERE IS NULL!! (NOT THE ISSUE)
        currentOrder = new Order(dishesManager, playerInventory); // Generate order based on inventory 
        currentOrder.orderPrice = CalculateTotalOrderPrice();
        DisplayOrder();
    }
    protected void Start()
    {
    }

    protected void Update()
    {
        if (!isOrderServed)
        {
            if (timer_start)
                {
                currentFillAmount += Time.deltaTime / fillDuration;

                if (fillImage != null)
                {
                    fillImage.fillAmount = currentFillAmount;
                }
                // Check if the bar is fully filled
                if (currentFillAmount >= 1f && stopFilling == false)
                {
                    //actions here //eg: customer angry run away
                    stopFilling = true;
                    HideOrder();
                    MoveTowardsExit();
                    isOrderServed = true;
                    FindObjectOfType<CustomerPool>().CustomerLeftSeat(seatNumber);
                }
            }
        }
        else
        {
            MoveTowardsExit();
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        Dish servedDish = other.GetComponent<Dish>();

        if (servedDish != null)
        {
            JudgeOrder(servedDish);
        }
    }
    public virtual void JudgeOrder(Dish servedDish)
    {
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
            bool isOrderComplete = true;
            if (currentOrder.orderedDishes.Count > 0)
                isOrderComplete = false;

            if (isOrderComplete)
            {
                stopFilling = true;
                isOrderServed = true;
                HideOrder();
                SpawnCoin(currentOrder.orderPrice);
                print("Money = " + currentOrder.orderPrice);
                MoveTowardsExit();
                FindObjectOfType<CustomerPool>().CustomerLeftSeat(seatNumber);
            }
        }
        else
        {
            Debug.Log($"Rejecting Order");
            RejectOrder(servedDish.gameObject);
        }
    }
    protected void HideOrder()
    {
        orderImage.SetActive(false);
        dishImage1.SetActive(false);
        dishImage2.SetActive(false);
        dishImage3.SetActive(false);
        burritoText.enabled = false;
        pizzaText.enabled = false;
        doughnutText.enabled = false;
        fillImage.enabled = false;
        fillImageBg.enabled = false;
    }
    protected float CalculateTotalOrderPrice()
    {
        float totalPrice = 0f;
        for (int i = 0; i < currentOrder.orderedDishes.Count; i++)
        {
            totalPrice += currentOrder.orderedDishes[i].price;
        }
        return totalPrice;
    }
    protected void UpdateOrderDisplay(DishData servedDish)
    {
            int index = currentOrder.orderedDishes.IndexOf(servedDish);
            if (index != -1)
            {
                if (currentOrder.dishQuantities[index] == 0)
                {
                    currentOrder.orderedDishes.RemoveAt(index);
                    currentOrder.dishQuantities.RemoveAt(index);
                }
            }
   
            // Update the text directly
            Dictionary<string, int> dishCounts = new Dictionary<string, int>();
            foreach (DishData dish in currentOrder.orderedDishes)
            {
                if (!dishCounts.ContainsKey(dish.dishName))
                    dishCounts[dish.dishName] = 0;
                dishCounts[dish.dishName]++;
            }

            if (dishCounts.ContainsKey("burrito"))
            {
                burritoText.text = "x " + dishCounts["burrito"];
            }
            else
            {
                burritoText.text = "";
            }

            if (dishCounts.ContainsKey("pizza"))
            {
                pizzaText.text = "x " + dishCounts["pizza"];
            }
            else
            {
                pizzaText.text = "";
            }

            if (dishCounts.ContainsKey("doughnut"))
            {
                doughnutText.text = "x " + dishCounts["doughnut"];
            }
            else
            {
                doughnutText.text = "";
            }
     
    }

    protected void SpawnCoin(float value)
    {
        GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);

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
    protected void DisplayOrder()
    {
        HideDishImages();
        burritoText.text = ""; 
        pizzaText.text = ""; 
        doughnutText.text = ""; 

        Dictionary<string, int> dishCounts = new Dictionary<string, int>();

        foreach (DishData dish in currentOrder.orderedDishes)
        {
            if (!dishCounts.ContainsKey(dish.dishName))
                dishCounts[dish.dishName] = 0;
            dishCounts[dish.dishName]++;
        }

        if (dishCounts.ContainsKey("burrito"))
        {
            dishImage1.SetActive(true);
            dishImage1.GetComponent<SpriteRenderer>().sprite = DishesManager.Instance.GetDishByName("burrito").dishImage;
            burritoText.text = "x " + dishCounts["burrito"];
        }

        if (dishCounts.ContainsKey("pizza"))
        {
            dishImage2.SetActive(true);
            dishImage2.GetComponent<SpriteRenderer>().sprite = DishesManager.Instance.GetDishByName("pizza").dishImage;
            pizzaText.text = "x " + dishCounts["pizza"];
        }

        if (dishCounts.ContainsKey("doughnut"))
        {
            dishImage3.SetActive(true);
            dishImage3.GetComponent<SpriteRenderer>().sprite = DishesManager.Instance.GetDishByName("doughnut").dishImage;
            doughnutText.text = "x " + dishCounts["doughnut"];
        }
    }


    protected void HideDishImages()
    {
        // Hide all dish image slots initially
        dishImage1.SetActive(false);
        dishImage2.SetActive(false);
        dishImage3.SetActive(false);
    }

    public virtual void RejectOrder(GameObject servedDish)
    {
        Dish dish = servedDish.GetComponent<Dish>();

        if (dish != null)
        {
            dish.ReturnToOriginalPosition();
        }
        else
        {
            Debug.LogError("Served dish does not have a Dish component.");
        }
    }

    public void EnterRestaurant(Transform spot)
    {
        transform.position = spot.position;
        orderImage.SetActive(true);
    }

    public void MoveToSeat(Transform seat)
    {
        // Start the coroutine to move towards the seat gradually
        StartCoroutine(MoveToSeatCoroutine(seat));
    }

    protected IEnumerator MoveToSeatCoroutine(Transform seat)
    {
        float speed = 2f; // Adjust the speed as needed

        // Continue moving until the customer is close enough to the seat
        while (Vector3.Distance(transform.position, seat.position) > 0.1f)
        {
            // Lerp the position towards the seat
            transform.position = Vector3.Lerp(transform.position, seat.position, speed * Time.deltaTime);
            // Yield to wait for the next frame
            yield return null;
        }

        // Ensure the customer reaches the exact seat position
        transform.position = seat.position;
        timer_start = true;
    }


    protected void MoveTowardsExit()
    {
        leaving = true;
        transform.position = Vector3.MoveTowards(transform.position, exitPoint.position, Time.deltaTime * 2f);

        if (Vector3.Distance(transform.position, exitPoint.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
