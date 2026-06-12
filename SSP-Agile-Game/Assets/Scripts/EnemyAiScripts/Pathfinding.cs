using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;

public static class Pathfinding
{
    // Find the safest path from start to target using A*
    public static List<Vector3Int> FindPath(
        Dictionary<Vector3Int, TileNode> nodes,
        Vector3Int startPos,
        Vector3Int targetPos)
    {
        // Reset A* properties for all nodes
        foreach (var node in nodes.Values)
        {
            node.gCost = float.MaxValue;
            node.hCost = 0;
            node.parent = null;
        }

        // Priority queue (sorted by fCost)
        var openSet = new PriorityQueue<TileNode>(Comparer<TileNode>.Create((a, b) => a.fCost.CompareTo(b.fCost)));
        var closedSet = new HashSet<Vector3Int>();

        TileNode startNode = nodes[startPos];
        TileNode targetNode = nodes[targetPos];

        startNode.gCost = 0;
        startNode.hCost = CalculateDistance(startPos, targetPos);
        openSet.Enqueue(startNode);

        while (openSet.Count > 0)
        {
            TileNode current = openSet.Dequeue();

            // Reached the target
            if (current.position == targetPos)
            {
                return ReconstructPath(current);
            }

            closedSet.Add(current.position);

            // Check all 4 neighbors (up, down, left, right)
            foreach (Vector3Int dir in GetNeighborDirections())
            {
                Vector3Int neighborPos = current.position + dir;

                // Skip if out of bounds or already processed
                if (!nodes.ContainsKey(neighborPos) || closedSet.Contains(neighborPos))
                    continue;

                TileNode neighbor = nodes[neighborPos];
                float tentativeGCost = current.gCost + CalculateMovementCost(current, neighbor);

                // Found a better path to this neighbor
                if (tentativeGCost < neighbor.gCost)
                {
                    neighbor.parent = current;
                    neighbor.gCost = tentativeGCost;
                    neighbor.hCost = CalculateDistance(neighborPos, targetPos);

                    if (!openSet.Contains(neighbor))
                        openSet.Enqueue(neighbor);
                }
            }
        }

        // No path found
        return new List<Vector3Int>();
    }

    // Calculate movement cost (higher node value = more dangerous = higher cost)
    private static float CalculateMovementCost(TileNode from, TileNode to)
    {
        // Base cost of 1 for moving to a neighbor
        float cost = 1f;

        // Add node value as a penalty (higher value = more dangerous)
        cost += to.value * 0.5f; // Adjust multiplier to control avoidance strength

        return cost;
    }

    // Manhattan distance (good for grid-based movement)
    private static float CalculateDistance(Vector3Int a, Vector3Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    // Reconstruct the path from target to start
    private static List<Vector3Int> ReconstructPath(TileNode target)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        path.Add(target.position);

        TileNode current = target;
        while (current.parent != null)
        {
            current = current.parent;
            path.Add(current.position);
        }

        path.Reverse(); // Start -> Target
        return path;
    }

    // Possible movement directions (4-way: up, down, left, right)
    private static Vector3Int[] GetNeighborDirections()
    {
        return new Vector3Int[]
        {
            new Vector3Int(1, 0, 0),   // Right
            new Vector3Int(-1, 0, 0),  // Left
            new Vector3Int(0, 1, 0),   // Up
            new Vector3Int(0, -1, 0)   // Down
        };
    }
}

// Simple priority queue for A*
public class PriorityQueue<T>
{
    private List<T> data;
    private readonly IComparer<T> comparer;

    public PriorityQueue(IComparer<T> comparer)
    {
        this.data = new List<T>();
        this.comparer = comparer;
    }

    public int Count => data.Count;

    public void Enqueue(T item)
    {
        data.Add(item);
        int child = data.Count - 1;
        while (child > 0)
        {
            int parent = (child - 1) / 2;
            if (comparer.Compare(data[child], data[parent]) >= 0)
                break;
            T tmp = data[child];
            data[child] = data[parent];
            data[parent] = tmp;
            child = parent;
        }
    }

    public T Dequeue()
    {
        int last = data.Count - 1;
        T frontItem = data[0];
        data[0] = data[last];
        data.RemoveAt(last);

        last--;
        int parent = 0;
        while (true)
        {
            int left = parent * 2 + 1;
            if (left > last) break;
            int right = left + 1;
            if (right <= last && comparer.Compare(data[right], data[left]) < 0)
                left = right;
            if (comparer.Compare(data[parent], data[left]) <= 0)
                break;
            T tmp = data[parent];
            data[parent] = data[left];
            data[left] = tmp;
            parent = left;
        }

        return frontItem;
    }

    public bool Contains(T item)
    {
        return data.Contains(item);
    }
}