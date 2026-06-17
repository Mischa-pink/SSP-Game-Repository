using UnityEngine;
using UnityEngine.UI;

public class SpawnOnButtonClick : MonoBehaviour
{
    public EnemySpawningScript enemySpawner;
    public Button spawnButton; // Assign in Inspector

    private void Start()
    {
        if (spawnButton != null)
        {
            spawnButton.onClick.AddListener(SpawnEnemies);
        }
    }

    private void SpawnEnemies()
    {
        if (enemySpawner != null)
        {
            Vector2 spawnPosition = transform.position;
            int amountToSpawn = 4; // Spawn 4 enemies
            enemySpawner.SpawnAtPosition(spawnPosition, amountToSpawn);
        }
    }
}