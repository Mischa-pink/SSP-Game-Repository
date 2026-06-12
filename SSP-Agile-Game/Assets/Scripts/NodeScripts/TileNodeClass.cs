using UnityEngine;
using UnityEngine.Tilemaps;

public class TileNode
{
    public Vector3Int position;
    public TileBase tile;
    public float value; // From NodeGenerator (lower = safer)

    // A* properties
    public float gCost; // Distance from start
    public float hCost; // Heuristic distance to goal
    public float fCost => gCost + hCost;
    public TileNode parent; // For path reconstruction

    public TileNode(Vector3Int pos, TileBase tileBase, float baseValue)
    {
        position = pos;
        tile = tileBase;
        value = baseValue;
    }
}