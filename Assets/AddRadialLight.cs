using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddRadialLight : MonoBehaviour
{
    public float lightIntensity = 1f;  // Set the light intensity
    public float lightRadius = 3f;     // Set the light radius
    public Color lightColor = Color.white;  // Set the light color

    void Start()
    {
        // Add a Light2D component to the object
        UnityEngine.Rendering.Universal.Light2D light2D = gameObject.AddComponent<UnityEngine.Rendering.Universal.Light2D>();

        // Set the light type to Point for a radial light source
        light2D.lightType = UnityEngine.Rendering.Universal.Light2D.LightType.Point;

        // Set the light properties
        light2D.intensity = lightIntensity;
        light2D.pointLightOuterRadius = lightRadius;
        light2D.color = lightColor;

        // Optional: Adjust falloff intensity
        light2D.falloffIntensity = 1f;
    }
}
