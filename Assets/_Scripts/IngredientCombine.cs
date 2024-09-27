using UnityEngine;

public class IngredientCombine : MonoBehaviour
{
    [Header("Preset Positions for Ingredients")]
    public Transform tortillaSnapPosition;
    public Transform beansSnapPosition;
    public Transform meatSnapPosition;
    public Transform cheeseSnapPosition;

    private bool isTortillaOccupied = false;
    private bool isBeansOccupied = false;
    private bool isMeatOccupied = false;
    private bool isCheeseOccupied = false;

    [Header("Bounce Back Force")]
    public float bounceBackForce = 5f;

    private void OnTriggerEnter(Collider other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();

        // Check if the incoming object is an ingredient and its type
        if (ingredient != null && ingredient.isBeingDragged)
        {
            switch (ingredient.type.ToLower())
            {
                case "tortilla":
                    HandleSnap(other.gameObject, tortillaSnapPosition, ref isTortillaOccupied);
                    break;
                case "beans":
                    HandleSnap(other.gameObject, beansSnapPosition, ref isBeansOccupied);
                    break;
                case "meat":
                    HandleSnap(other.gameObject, meatSnapPosition, ref isMeatOccupied);
                    break;
                case "cheese":
                    HandleSnap(other.gameObject, cheeseSnapPosition, ref isCheeseOccupied);
                    break;
                default:
                    // Optional: Handle unexpected ingredient types
                    break;
            }
        }
    }

    private void HandleSnap(GameObject ingredientObj, Transform snapPosition, ref bool isOccupied)
    {
        Rigidbody ingredientRb = ingredientObj.GetComponent<Rigidbody>();

        // If the spot is free, snap the ingredient
        if (!isOccupied)
        {
            ingredientObj.transform.position = snapPosition.position;
            ingredientObj.transform.rotation = snapPosition.rotation;  // Align orientation
            ingredientRb.isKinematic = true;  // Disable physics after snapping
            isOccupied = true;
        }
        else
        {
            // If spot is occupied, apply a small force to push the ingredient back
            Vector3 bounceDirection = (ingredientObj.transform.position - snapPosition.position).normalized;
            ingredientRb.AddForce(bounceDirection * bounceBackForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Release the spot when the ingredient exits
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            switch (ingredient.type.ToLower())
            {
                case "tortilla":
                    isTortillaOccupied = false;
                    break;
                case "beans":
                    isBeansOccupied = false;
                    break;
                case "meat":
                    isMeatOccupied = false;
                    break;
                case "cheese":
                    isCheeseOccupied = false;
                    break;
                default:
                    // Optional: Handle unexpected ingredient types
                    break;
            }
        }
    }
}
