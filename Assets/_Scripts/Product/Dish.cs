using UnityEngine;

public class Dish : MonoBehaviour
{
    public DishData dishData; // Reference to the ScriptableObject for this dish
    public Vector3 originalPosition;
    private bool isReturning = false;
    private float lerpSpeed = 5f;

    private void Start()
    {
        // Set original position at the start
        originalPosition = transform.position;
    }

    public void SetOriginalPosition(Vector3 position)
    {
        originalPosition = position;
    }

    public void ReturnToOriginalPosition()
    {
        isReturning = true;
    }

    private void Update()
    {
        if (isReturning)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * lerpSpeed);
            if (Vector3.Distance(transform.position, originalPosition) < 0.01f)
            {
                transform.position = originalPosition;
                isReturning = false;
            }
        }
    }
}
