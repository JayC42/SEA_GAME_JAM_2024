using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBar_Script : MonoBehaviour
{
    public Image fillImage; //UI image type: Filled
    public float fillDuration = 3f;
    private float currentFillAmount = 0f;

    private bool isWaiting = false; //boolean for customer waiting

    private void Start()
    {
        if (!isWaiting)
        {
            StartWaiting(); // Start the waiting
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isWaiting)
        {
            // Fill the loading bar over time
            currentFillAmount += Time.deltaTime / fillDuration;

            if (fillImage != null)
            {
                fillImage.fillAmount = currentFillAmount;
            }

            // Check if the bar is fully filled
            if (currentFillAmount >= 1f)
            {
                //actions here //eg: customer angry run away
                ResetFill(); //reset fill bar //optional
            }
        }
        
    }

    private void StartWaiting()
    {
        isWaiting = true;
    }

    //optional
    private void ResetFill()
    {
        currentFillAmount = 0f;
        if (fillImage != null)
        {
            fillImage.fillAmount = 0f;
        }

        isWaiting = false;

        //Debug.Log("Fill reset");
    }

}
