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
    private int minDistanceBetweenEnemies = 1;


    private void Start()
    {


        Vector2 spawnPosition = (Vector2)transform.position + spawnOffset;
        SpawnAtPosition(spawnPosition, AmountOfEnemySpawns);
    }

    public void SpawnAtPosition(Vector2 position, int AmountOfEnemySpawns)
    {
        List<Vector2> spawnedPositions = new List<Vector2>();

        float MaxEnemyLocationsFloat = Vector2.Dot(spawnRange, spawnRange);
        Debug.Log($"MaxEnemyLocationsFloat: {MaxEnemyLocationsFloat}");

        // Convert float to int (rounded)
        int MaxEnemyLocations = Mathf.RoundToInt(MaxEnemyLocationsFloat);
        Debug.Log($"Rounded to int: {MaxEnemyLocations}");

        if (AmountOfEnemySpawns >= MaxEnemyLocations)
        {
            AmountOfEnemySpawns = MaxEnemyLocations;
        }

        for (int i = 0; i < AmountOfEnemySpawns; ++i)
        {
            Vector2 randomPosition;
            bool positionIsValid;

            // Try to find a valid random position
            do
            {
                randomPosition = new Vector2(
                    Random.Range(transform.position.x - spawnRange.x, transform.position.x + spawnRange.x),
                    Random.Range(transform.position.y - spawnRange.y, transform.position.y + spawnRange.y)
                );

                // Check if the position is already taken
                positionIsValid = true;
                foreach (Vector2 spawnedPos in spawnedPositions)
                {
                    if (Vector2.Distance(randomPosition, spawnedPos) < minDistanceBetweenEnemies)
                    {
                        positionIsValid = false;
                        break;
                    }
                }
            }
            while (!positionIsValid);

            // Spawn the enemy
            Instantiate(Enemy, randomPosition, Quaternion.identity);
            spawnedPositions.Add(randomPosition);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector2 center = transform.position;

        Gizmos.DrawLine(
            new Vector2(center.x - spawnRange.x, center.y - spawnRange.y),
            new Vector2(center.x + spawnRange.x, center.y - spawnRange.y)
        );
        Gizmos.DrawLine(
            new Vector2(center.x + spawnRange.x, center.y - spawnRange.y),
            new Vector2(center.x + spawnRange.x, center.y + spawnRange.y)
        );
        Gizmos.DrawLine(
            new Vector2(center.x + spawnRange.x, center.y + spawnRange.y),
            new Vector2(center.x - spawnRange.x, center.y + spawnRange.y)
        );
        Gizmos.DrawLine(
            new Vector2(center.x - spawnRange.x, center.y + spawnRange.y),
            new Vector2(center.x - spawnRange.x, center.y - spawnRange.y)
        );
    }

    //public void SpawnAtPosition(Vector2 position, int AmountOfEnemySpawns)
    //{
    //    List<Vector2> spawnedPositions = new List<Vector2>();

    //    float MaxEnemyLocationsFloat = Vector2.Dot(spawnRange, spawnRange);
    //    Debug.Log($"{MaxEnemyLocationsFloat}");

    //    if (float.TryParse(MaxEnemyLocationsFloat, out MaxEnemyLocationsF))
    //    {
    //        int intValue = Mathf.RoundToInt(floatValue); // Rounds to nearest int
    //        Debug.Log($"Rounded to int: {intValue}"); // Output: 4
    //    }

    //    if (AmountOfEnemySpawns >= MaxEnemyLocations)
    //    {
    //        AmountOfEnemySpawns = MaxEnemyLocations;
    //    }

    //    for (int i = 0; i < AmountOfEnemySpawns; ++i)
    //    {
    //        Vector2 randomPosition;
    //        bool positionIsValid;

    //        // Try to find a valid random position
    //        do
    //        {
    //            randomPosition = new Vector2(
    //                Random.Range(transform.position.x - spawnRange.x, transform.position.x + spawnRange.x),
    //                Random.Range(transform.position.y - spawnRange.y, transform.position.y + spawnRange.y)
    //            );

    //            // Check if the position is already taken
    //            positionIsValid = true;
    //            foreach (Vector2 spawnedPos in spawnedPositions)
    //            {
    //                if (Vector2.Distance(randomPosition, spawnedPos) < minDistanceBetweenEnemies)
    //                {
    //                    positionIsValid = false;
    //                    break;
    //                }
    //            }
    //        }
    //        while (!positionIsValid);

    //        // Spawn the enemy
    //        Instantiate(Enemy, randomPosition, Quaternion.identity);
    //        spawnedPositions.Add(randomPosition);
    //    }
    //}
}
