using UnityEngine;
using Cinemachine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;         // The enemy prefab to spawn
    public Transform character;            // Reference to the falling character
    public float spawnDistanceBelow = 2f;  // Distance below the character to spawn the enemy
    public float spawnInterval = 2f;       // Time interval between spawns
    public Camera mainCamera;              // Reference to the main camera
    public float stopSpawningAtDistance = 500f;  // Distance at which to stop spawning

    private float nextSpawnTime;
    private float startY;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;  // Automatically find the camera with the "MainCamera" tag
        }

        nextSpawnTime = Time.time + spawnInterval;
        startY = character.position.y;  // Record the starting Y position
    }

    void Update()
    {
        // Calculate the distance the character has fallen
        float distanceFallen = startY - character.position.y;
        float distanceInMeters = distanceFallen * 0.05f;

        // Stop spawning enemies after the player reaches the stopSpawningAtDistance
        if (distanceInMeters >= stopSpawningAtDistance)
        {
            return;
        }

        // Check if it's time to spawn the next enemy
        if (Time.time > nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        if (mainCamera == null) return;

        // Get the camera's boundaries in world space
        float minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).x;
        float maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane)).x;

        // Calculate a random horizontal position within the camera's visible area
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, character.position.y - spawnDistanceBelow, 0f);

        // Define the rotation to rotate the enemy 90 degrees counterclockwise
        Quaternion spawnRotation = Quaternion.Euler(0f, 0f, 90f);

        // Instantiate the enemy object with the desired rotation
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);

        // Attach the destroy script to the enemy
        enemy.AddComponent<DestroyWhenOutOfView>();
    }
}

// This script should be attached to the enemy object.
public class DestroyWhenOutOfView : MonoBehaviour
{
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Destroy the enemy object if it's no longer visible by any camera
        if (!objectRenderer.isVisible)
        {
            Destroy(gameObject);
        }
    }
}
