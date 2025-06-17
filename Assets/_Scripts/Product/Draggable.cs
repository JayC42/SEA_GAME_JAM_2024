using System;
using UnityEngine;
public class Draggable : MonoBehaviour
{
    public static event Action<Draggable> OnDraggableCreated; // Event to notify when a Draggable is created

    public delegate void DragEndedDelegate(Draggable draggableObject);
    public DragEndedDelegate dragEndedCallback;

    private bool isDragged = false;
    private Vector3 mouseDragStartPosition;
    private Vector3 spriteDragStartPosition;
    private float zOffset;

    private void Start()
    {
        OnDraggableCreated?.Invoke(this); // Notify SnapController that this Draggable is created
        zOffset = Camera.main.WorldToScreenPoint(transform.position).z;
    }

    private void OnMouseDown()
    {
        StartDragging();
    }

    private void OnMouseDrag()
    {
        if (isDragged)
        {
            Vector3 currentMousePosition = GetMouseWorldPosition();
            transform.position = spriteDragStartPosition + (currentMousePosition - mouseDragStartPosition);
        }
    }

    private void OnMouseUp()
    {
        StopDragging();
        dragEndedCallback?.Invoke(this); // Call the drag ended callback if dragging occurred
    }

    public void StartDragging()
    {
        isDragged = true;
        mouseDragStartPosition = GetMouseWorldPosition();
        spriteDragStartPosition = transform.position;
    }

    public void StopDragging()
    {
        isDragged = false;
    }

    // Disable dragging after the ingredient is snapped to a snap point
    public void DisableDragging()
    {
        isDragged = false; // Prevent further dragging
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = zOffset; // Use the stored Z offset for the screen to world conversion
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }
}