using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class EnemySpawningScript : MonoBehaviour
{
    public GameObject Enemy;
    public Vector2 spawnRange = new Vector2(5f, 5f);
    public Vector2 spawnOffset = new Vector2(2f, 1f);
    public int AmountOfEnemySpawns = 3;
    public float minDistanceBetweenEnemies = 1f; // Made public for easier tuning

    private void Start()
    {
        Vector2 spawnPosition = (Vector2)transform.position + spawnOffset;
        //SpawnAtPosition(spawnPosition, AmountOfEnemySpawns);
    }

    public void SpawnAtPosition(Vector2 center, int amountOfEnemySpawns)
    {
        List<Vector2> spawnedPositions = new List<Vector2>();

        // Calculate the approximate maximum number of enemies that can fit in the spawn range
        // This is a rough estimate based on the area and minimum distance
        float spawnArea = spawnRange.x * spawnRange.y;
        float minAreaPerEnemy = minDistanceBetweenEnemies * minDistanceBetweenEnemies;
        int maxEnemyLocations = Mathf.FloorToInt(spawnArea / minAreaPerEnemy);

        // Clamp the number of enemies to spawn
        amountOfEnemySpawns = Mathf.Min(amountOfEnemySpawns, maxEnemyLocations);

        for (int i = 0; i < amountOfEnemySpawns; ++i)
        {
            Vector2 randomPosition;
            bool positionIsValid;
            int attempts = 0;
            const int maxAttempts = 100; // Prevent infinite loops

            do
            {
                // Generate a random position within the spawn range around the center
                randomPosition = new Vector2(
                    Random.Range(center.x - spawnRange.x, center.x + spawnRange.x),
                    Random.Range(center.y - spawnRange.y, center.y + spawnRange.y)
                );

                // Check if the position is valid
                positionIsValid = true;
                foreach (Vector2 spawnedPos in spawnedPositions)
                {
                    if (Vector2.Distance(randomPosition, spawnedPos) < minDistanceBetweenEnemies)
                    {
                        positionIsValid = false;
                        break;
                    }
                }

                attempts++;
            }
            while (!positionIsValid && attempts < maxAttempts);

            // If a valid position was found, spawn the enemy
            if (positionIsValid)
            {
                Instantiate(Enemy, randomPosition, Quaternion.identity);
                spawnedPositions.Add(randomPosition);
            }
            else
            {
                Debug.LogWarning($"Failed to find a valid position for enemy {i} after {maxAttempts} attempts.");
                break; // Stop spawning if no valid position is found
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector2 center = (Vector2)transform.position + spawnOffset;

        // Draw a rectangle representing the spawn range
        Gizmos.DrawWireCube(center, spawnRange);
    }
}