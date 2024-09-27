using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapController : MonoBehaviour
{
    [System.Serializable]
    public class SnapPoint
    {
        public Transform point;          // The Transform for the snap point
        public string requiredIngredient; // The type of ingredient allowed for this snap point (e.g., "tortilla", "meat")
        public bool isOccupied = false;  // Tracks if the snap point is already occupied
        public GameObject snappedIngredient; // Reference to the snapped ingredient
    }

    public List<SnapPoint> snapPoints;  // List of snap points with assigned ingredient types

    public float snapRange = 0.5f;

    public GameObject burritoPrefab; // Reference to the burrito prefab
    public Transform burritoSpawnPoint; // Where the burrito should appear after all ingredients are placed
    private List<Draggable> draggableObjects = new List<Draggable>();

    void OnEnable()
    {
        Draggable.OnDraggableCreated += RegisterNewDraggable;
    }

    void OnDisable()
    {
        Draggable.OnDraggableCreated -= RegisterNewDraggable;
    }

    private void RegisterNewDraggable(Draggable draggable)
    {
        if (!draggableObjects.Contains(draggable))
        {
            draggableObjects.Add(draggable);
            draggable.dragEndedCallback = OnDragEnded;
        }
    }

    void Start()
    {
        foreach (Draggable draggable in draggableObjects)
        {
            draggable.dragEndedCallback = OnDragEnded;
        }
    }

    private void OnDragEnded(Draggable draggable)
    {
        float closestDistance = -1;
        SnapPoint closestSnapPoint = null;

        Ingredient draggableIngredient = draggable.GetComponent<Ingredient>();
        if (draggableIngredient == null) return; // If there's no Ingredient component, exit

        // Find the closest valid snap point for this ingredient
        foreach (SnapPoint snapPoint in snapPoints)
        {
            if (snapPoint.isOccupied) continue; // Skip if the snap point is occupied

            // Check if the ingredient matches the required type for this snap point
            if (snapPoint.requiredIngredient == draggableIngredient.type)
            {
                float currentDistance = Vector2.Distance(draggable.transform.localPosition, snapPoint.point.localPosition);

                if (closestSnapPoint == null || currentDistance < closestDistance)
                {
                    closestSnapPoint = snapPoint;
                    closestDistance = currentDistance;
                }
            }
        }

        // Snap the draggable to the closest valid snap point if within snap range
        if (closestSnapPoint != null && closestDistance < snapRange)
        {
            draggable.transform.localPosition = closestSnapPoint.point.localPosition;
            closestSnapPoint.isOccupied = true; // Mark the snap point as occupied
            closestSnapPoint.snappedIngredient = draggable.gameObject; // Store the ingredient in the snap point

            // Disable dragging for this ingredient once it's placed
            draggable.DisableDragging(); // Ensure this is called to prevent further dragging

            // Optionally, you can disable the collider or set the GameObject inactive
            draggable.GetComponent<Collider2D>().enabled = false; // Disable the collider if using 2D colliders

            // Check if all ingredients are in place
            CheckAllIngredientsInPlace();
        }
    }


    // Method to check if all ingredients are in place
    private void CheckAllIngredientsInPlace()
    {
        bool allInPlace = true;

        // Check if all snap points are occupied
        foreach (SnapPoint snapPoint in snapPoints)
        {
            if (!snapPoint.isOccupied)
            {
                allInPlace = false;
                break;
            }
        }

        // If all snap points are occupied, create the burrito and destroy ingredients
        if (allInPlace)
        {
            InstantiateBurrito();
        }
    }

    // Method to instantiate the burrito and destroy all ingredients
    private void InstantiateBurrito()
    {
        // Instantiate the burrito at the defined spawn point
        Instantiate(burritoPrefab, burritoSpawnPoint.position, burritoSpawnPoint.rotation);

        // Destroy all snapped ingredients
        foreach (SnapPoint snapPoint in snapPoints)
        {
            if (snapPoint.snappedIngredient != null)
            {
                Destroy(snapPoint.snappedIngredient);
                snapPoint.snappedIngredient = null; // Clear the reference
                snapPoint.isOccupied = false; // Mark the snap point as no longer occupied
            }
        }
    }
}
