using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class EnemyAI : MonoBehaviour
{
    public NodeGenerator nodeGenerator; // Assign in Inspector
    public float moveSpeed = 2f;
    public Vector3Int targetPosition; // Set this to the goal position

    private List<Vector3Int> currentPath;
    private int currentPathIndex = 0;
    private Tilemap tilemap; // Reference to the Tilemap for position conversion

    public int damage = 1;
    public float attackRate = 1f;

    private bool isRecalculating = false;



    void Start()
    {
        nodeGenerator = NodeGenerator.Instance;

        if (nodeGenerator.tilemaps.Length > 0)
            tilemap = nodeGenerator.tilemaps[0];

        // Snap enemy to the nearest valid node
        Vector3Int startPos = tilemap.WorldToCell(transform.position);

        // Find the closest node to the enemy's position
        Vector3Int closestNodePos = startPos;
        float closestDistance = float.MaxValue;

        foreach (Vector3Int nodePos in nodeGenerator.nodes.Keys)
        {
            float distance = Vector3Int.Distance(startPos, nodePos);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNodePos = nodePos;
            }
        }

        // Move enemy to the closest node
        transform.position = tilemap.CellToWorld(closestNodePos) + new Vector3(0.5f, 0.5f, 0);
        //Debug.Log($"Snapped enemy to node at: {closestNodePos}");

        FindNewPath();
    }

    void Update()
    {
        if (currentPath == null || currentPath.Count == 0)
        {
            if (!isRecalculating)
            {
                isRecalculating = true;
                Debug.LogWarning("No path! Recalculating...");
                FindNewPath();
                isRecalculating = false;
            }
            return;
        }

        // Rest of your Update() code...
        Vector3Int nextNode = currentPath[currentPathIndex];
        Vector3 targetWorldPos = tilemap.GetCellCenterWorld(nextNode);

        Debug.DrawLine(transform.position, targetWorldPos, Color.red); // Draw path line

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetWorldPos,
            moveSpeed * Time.deltaTime
        );


        if (Vector3.Distance(transform.position, targetWorldPos) < 0.2f)
        {
            currentPathIndex++;
            if (currentPathIndex >= currentPath.Count)
            {
                Debug.Log("You lost!");
                Destroy(gameObject);
                SceneManager.LoadScene("Start Menu");
                return;
            }
        }
    }



    // Call this when the target changes or nodes are updated
    public void FindNewPath()
    {
        Vector3Int startPos = tilemap.WorldToCell(transform.position);

        // Ensure start and target are valid nodes
        if (!nodeGenerator.nodes.ContainsKey(startPos) || !nodeGenerator.nodes.ContainsKey(targetPosition))
        {
            Debug.LogWarning("Start or target position is not a valid node!");
            return;
        }

        currentPath = Pathfinding.FindPath(
            nodeGenerator.nodes,
            startPos,
            targetPosition
        );

        currentPathIndex = 0;

        // Debug: Draw the path in the Scene view
        //if (currentPath.Count > 0)
        //{
        //    Debug.Log($"Path found with {currentPath.Count} nodes. Total cost: {currentPath.Count}");
        //}
        //else
        //{
        //    Debug.LogWarning("No path found!");
        //}
    }

    // Visualize the path in the Scene view (optional)
    void OnDrawGizmos()
    {
        if (currentPath == null || currentPath.Count == 0)
            return;

        Gizmos.color = Color.green;
        foreach (Vector3Int pos in currentPath)
        {
            Vector3 worldPos = tilemap.CellToWorld(pos) + new Vector3(0.5f, 0.5f, 0);
            Gizmos.DrawWireCube(worldPos, Vector3.one * 0.8f);
        }
    }
}
