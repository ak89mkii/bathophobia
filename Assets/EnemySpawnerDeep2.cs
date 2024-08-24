using UnityEngine;

public class EnemySpawnerDeep2 : MonoBehaviour
{
    public GameObject objectToSpawn;      // The object to spawn
    public Transform player;              // Reference to the player character
    public float spawnDistance = 10999f;  // Distance at which the object will be spawned

    private bool hasSpawned = false; // Flag to ensure only one spawn
    private float startY;

    void Start()
    {
        startY = player.position.y;  // Record the starting Y position
    }

    void Update()
    {
        // Calculate the distance the player has fallen
        float distanceFallen = startY - player.position.y;
        float distanceInMeters = distanceFallen * 0.05f;

        // Check if the player has reached the specified distance
        if (distanceInMeters >= spawnDistance && !hasSpawned)
        {
            SpawnObject();
            hasSpawned = true; // Ensure no further spawning occurs
        }
    }

    void SpawnObject()
    {
        if (objectToSpawn == null || player == null) return;

        // Calculate the spawn position directly below the player
        Vector3 spawnPosition = new Vector3(player.position.x, player.position.y - 2f, player.position.z);

        // Define the rotation if needed (optional)
        Quaternion spawnRotation = Quaternion.identity; // No rotation

        // Instantiate the object with the desired rotation
        Instantiate(objectToSpawn, spawnPosition, spawnRotation);
    }
}
