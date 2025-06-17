using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCanSpawnerChecker : MonoBehaviour
{
    public GameObject windObject;  // Assign this in the inspector, the Wind GameObject to activate.

    private void Start()
    {
        //get wind can spawner to lock
        windObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void CheckGlassBottle()
    {
        // Check if the child has the GeneralHolder component
        GeneralHolder holder = GetComponent<GeneralHolder>();
        if (holder != null)
        {
            // Check if there are items in the GeneralHolder (sand objects)
            if (holder.heldItems.Count > 0)
            {
           
                // Activate the Wind object
                if (windObject != null)
                {
                    //get wind can spawner to unlock
                    windObject.GetComponent<BoxCollider2D>().enabled = true;
                }
                else
                {
                    Debug.LogWarning("Wind object is not assigned.");
                }
            }
            else
            {
                 

                Debug.LogWarning("No sand objects available to produce wind.");
            }
        }
    }
    
    public void RemoveUsedSandGlass()
    {
        // Check if the child has the GeneralHolder component
        GeneralHolder holder = GetComponent<GeneralHolder>();

        // Remove one sand object from the holder
        holder.RemoveItem();

        if (holder.heldItems.Count == 0)
        {
            //get wind can spawner to lock
            windObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
