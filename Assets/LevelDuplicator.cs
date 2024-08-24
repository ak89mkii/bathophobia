using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDuplicator : MonoBehaviour
{
    public GameObject levelSectionPrefab;  // The prefab for the level section
    public Transform lastSection;          // The transform of the last instantiated section
    public float sectionHeight = 10f;      // The height of each level section (adjust this to your section's actual height)
    public GameObject backgroundPrefab;    // The prefab for the background object
    public int maxBackgrounds = 5;         // The maximum number of background objects

    private Queue<GameObject> backgroundQueue = new Queue<GameObject>(); // Queue to manage background objects
    private bool canTrigger = true;        // Flag to prevent multiple triggers

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canTrigger && collision.CompareTag("Player"))
        {
            canTrigger = false; // Prevent further triggers until the cooldown period is over

            Debug.Log("Player entered the trigger.");

            // Calculate the position for the new section (below the last section)
            Vector3 newSectionPosition = lastSection.position + new Vector3(0, -sectionHeight, 0);

            // Instantiate the new section at the calculated position
            Transform newSection = Instantiate(levelSectionPrefab, newSectionPosition, Quaternion.identity).transform;

            // Update the lastSection reference to the newly created section
            lastSection = newSection;

            Debug.Log("New section created at position: " + newSectionPosition);

            // Instantiate a new background and manage background queue
            GameObject newBackground = Instantiate(backgroundPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            // Position the new background appropriately
            newBackground.transform.position = new Vector3(0, -sectionHeight * (backgroundQueue.Count + 1), 0);

            // Add the new background to the queue
            backgroundQueue.Enqueue(newBackground);

            // If the number of backgrounds exceeds maxBackgrounds, remove the oldest one
            if (backgroundQueue.Count > maxBackgrounds)
            {
                GameObject oldBackground = backgroundQueue.Dequeue();
                Destroy(oldBackground);
                Debug.Log("Old background destroyed.");
            }

            Debug.Log("New background instantiated.");

            // Reset the trigger flag after a short delay
            StartCoroutine(ResetTrigger());
        }
    }

    private IEnumerator ResetTrigger()
    {
        yield return new WaitForSeconds(1f); // Adjust the cooldown period as needed
        canTrigger = true;
    }
}
