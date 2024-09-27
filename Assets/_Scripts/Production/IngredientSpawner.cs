using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    public GameObject draggablePrefab; // Prefab for the draggable object

    private void OnMouseDown()
    {
        if (draggablePrefab != null)
        {
            // Instantiate a new draggable object at the current ingredient's position
            GameObject newDraggable = Instantiate(draggablePrefab, transform.position, Quaternion.identity);

            // Get the Draggable component from the newly instantiated object
            Draggable draggableComponent = newDraggable.GetComponent<Draggable>();

            // Start dragging the object immediately
            draggableComponent.StartDragging();
        }
    }
}
