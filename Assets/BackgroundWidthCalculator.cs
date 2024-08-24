using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundWidthCalculator : MonoBehaviour
{
    void Start()
    {
        // Get the width of the background using the Renderer component
        if (TryGetComponent(out Renderer renderer))
        {
            float backgroundWidth = renderer.bounds.size.x;
            Debug.Log("Background width: " + backgroundWidth + " units");
        }
        else
        {
            Debug.LogError("No Renderer component found on the background object.");
        }
    }
}
