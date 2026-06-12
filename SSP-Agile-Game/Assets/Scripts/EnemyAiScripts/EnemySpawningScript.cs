using UnityEngine;

public class EnemySpawningScript : MonoBehaviour
{
    public GameObject Enemy; 
    public Vector2 spawnRange = new Vector2(5f, 5f); 
    public Vector2 spawnOffset = new Vector2(2f, 1f);
    public int AmountOfEnemySpawns = 3;


    private void Start()
    {


        Vector2 spawnPosition = (Vector2)transform.position + spawnOffset;
        SpawnAtPosition(spawnPosition, AmountOfEnemySpawns);
    }

    public void SpawnAtPosition(Vector2 position, int AmountOfEnemySpawns)
    {
        for (int i = 0; i < AmountOfEnemySpawns; ++i)
        {
            if (Mathf.Abs(position.x - transform.position.x) <= spawnRange.x &&
                Mathf.Abs(position.y - transform.position.y) <= spawnRange.y)
            {
                Instantiate(Enemy, position, Quaternion.identity);
            }

            else
            {
                Debug.Log("Selected spot is outside the spawn range!");
            }
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
}
