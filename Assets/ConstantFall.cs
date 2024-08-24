using UnityEngine;
using UnityEngine.UI;

public class ConstantFall : MonoBehaviour
{
    public float fallSpeed = -5f;          // Desired fall speed
    public float fallDuration = 5f;        // Duration of the fall
    public Text distanceText;              // Reference to the UI Text component for displaying the distance
    public Text endGameText;               // Reference to the UI Text component for displaying the end game message
    public Camera mainCamera;              // Reference to the main camera

    // Define the transition colors and thresholds
    public Color lightBlue = new Color(0.5f, 0.5f, 1f); // Light blue color
    public Color blue = new Color(0f, 0f, 1f);         // Blue color
    public Color darkBlue = new Color(0f, 0f, 0.5f);    // Dark blue color
    public Color black = Color.black;                  // Black color

    public float lightBlueThreshold = 50f; // Distance for light blue transition
    public float blueThreshold = 100f;     // Distance for blue transition
    public float darkBlueThreshold = 150f; // Distance for dark blue transition
    public float blackThreshold = 200f;    // Distance for black transition
    public float endGameThreshold = 11000f; // Distance for ending the game

    private Rigidbody2D rb;
    private float fallTimeRemaining;
    private float startY;
    private bool gameEnded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fallTimeRemaining = fallDuration;  // Initialize the timer
        startY = transform.position.y;     // Record the starting Y position

        if (mainCamera == null)
        {
            mainCamera = Camera.main;  // Automatically find the camera with the "MainCamera" tag
        }

        // Ensure the end game message is hidden at the start
        if (endGameText != null)
        {
            endGameText.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (fallTimeRemaining > 0 && !gameEnded)
        {
            rb.velocity = new Vector2(rb.velocity.x, fallSpeed);
            fallTimeRemaining -= Time.fixedDeltaTime;

            // Calculate and display the distance fallen
            float distanceFallen = startY - transform.position.y;
            float distanceInMeters = distanceFallen * 0.05f;
            distanceText.text = "Meters: " + distanceInMeters.ToString("F0");

            // Change the background color based on the distance fallen
            ChangeBackgroundColor(distanceInMeters);

            // Check if the player has reached the end game threshold
            if (distanceInMeters >= endGameThreshold)
            {
                EndGame();
            }
        }
        else
        {
            // Stop the fall after the timer ends
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    void ChangeBackgroundColor(float distance)
    {
        if (mainCamera != null)
        {
            Color targetColor;

            if (distance >= blackThreshold)
            {
                // Transition to black color
                targetColor = black;
            }
            else if (distance >= darkBlueThreshold)
            {
                // Interpolate between dark blue and black
                float t = Mathf.InverseLerp(darkBlueThreshold, blackThreshold, distance);
                targetColor = Color.Lerp(darkBlue, black, t);
            }
            else if (distance >= blueThreshold)
            {
                // Interpolate between blue and dark blue
                float t = Mathf.InverseLerp(blueThreshold, darkBlueThreshold, distance);
                targetColor = Color.Lerp(blue, darkBlue, t);
            }
            else if (distance >= lightBlueThreshold)
            {
                // Interpolate between light blue and blue
                float t = Mathf.InverseLerp(lightBlueThreshold, blueThreshold, distance);
                targetColor = Color.Lerp(lightBlue, blue, t);
            }
            else
            {
                // If distance is less than the lightBlueThreshold, set the background to light blue
                targetColor = lightBlue;
            }

            mainCamera.backgroundColor = targetColor;
        }
    }

    void EndGame()
    {
        gameEnded = true;
        rb.velocity = Vector2.zero;

        if (endGameText != null)
        {
            endGameText.text = "Congratulations! You Reached the Deepest Part of the Ocean.";
            endGameText.gameObject.SetActive(true);
        }
    }
}
