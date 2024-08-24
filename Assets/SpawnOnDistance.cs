using UnityEngine;

public class SpawnOnDistance : MonoBehaviour
{
    public GameObject objectToSpawn;       // The object to spawn (not a prefab)
    public Transform player;               // Reference to the player object
    public float targetDistance = 11000f;  // Distance at which the object will be spawned

    private bool hasSpawned = false; // Flag to ensure only one spawn

    void Update()
    {
        // Calculate the distance the player has fallen
        float distanceFallen = (player.position.y - transform.position.y) * 0.05f;

        // Check if the player has reached the target distance and if the object hasn't been spawned yet
        if (distanceFallen >= targetDistance && !hasSpawned)
        {
            SpawnObject();
            hasSpawned = true; // Ensure no further spawning occurs
        }
    }

    void SpawnObject()
    {
        if (objectToSpawn != null && player != null)
        {
            // Calculate the spawn position directly below the player
            Vector3 spawnPosition = new Vector3(player.position.x, player.position.y - 2f, player.position.z);

            // Log the spawn position for debugging purposes
            Debug.Log("Spawning object at position: " + spawnPosition);

            // Instantiate the object at the calculated position
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("ObjectToSpawn or Player is not assigned.");
        }
    }
}
