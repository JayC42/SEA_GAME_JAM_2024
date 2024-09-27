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

    private void Start()
    {
        OnDraggableCreated?.Invoke(this); // Notify SnapController that this Draggable is created
    }

    private void OnMouseDown()
    {
        StartDragging();
    }

    private void OnMouseDrag()
    {
        if (isDragged)
        {
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.localPosition = spriteDragStartPosition + (currentMousePosition - mouseDragStartPosition);
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
        mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spriteDragStartPosition = transform.localPosition;
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
}
