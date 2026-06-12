using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class NodeGenerator : MonoBehaviour
{
    public static NodeGenerator Instance { get; private set; }
    // Assign your Tilemaps in the Inspector (e.g., Ground and Objects)
    public Tilemap[] tilemaps;

    // Base values for each regular tile type
    public Dictionary<string, float> tileBaseValues = new Dictionary<string, float>
    {
        { "tileset_3_19", 1f },
        { "tileset_3_17", 1f },
        { "wall_0", 3f },
    };

    // Base values for building tiles
    public Dictionary<string, float> buildingBaseValues = new Dictionary<string, float>
    {
        { "basic_house_1_0", 999f },
        { "basic_house_1_1", 999f },
        { "basic_house_1_2", 999f },
        { "basic_house_1_3", 999f }
    };

    // Stores all generated nodes (position -> node)
    public Dictionary<Vector3Int, TileNode> nodes = new Dictionary<Vector3Int, TileNode>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        GenerateNodes(); // Generate nodes when the game starts
        //DebugPrintNodes(); // Optional: Print node data to the Console
        ColorTilesByValue(); // Optional: Color tiles to visualize values
    }

    public void DebugPrintTileNames()
    {
        Debug.Log("=== TILE NAMES DEBUG ===");
        foreach (Tilemap tilemap in tilemaps)
        {
            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {
                if (tilemap.HasTile(pos))
                {
                    TileBase tile = tilemap.GetTile(pos);
                    Debug.Log($"Tile at {pos}: {tile.name}");
                }
            }
        }
        Debug.Log("========================");
    }

    public void DebugPrintNodes()
    {
        Debug.Log("=== NODES DEBUG ===");
        foreach (var kvp in nodes)
        {
            Vector3Int pos = kvp.Key;
            TileNode node = kvp.Value;
            Debug.Log(
                $"Node at {pos}: " +
                $"Tile = {node.tile.name}, " +
                $"Value = {node.value}"
            );
        }
        Debug.Log("===================");
    }

    public void ColorTilesByValue()
    {
        foreach (var tilemap in tilemaps)
        {
            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {
                if (nodes.ContainsKey(pos))
                {
                    float value = nodes[pos].value;
                    float darkness = Mathf.Clamp01(value / 20f);
                    Color color = Color.Lerp(Color.white, Color.black, darkness);

                    // Enable color tinting for this tile
                    tilemap.SetTileFlags(pos, TileFlags.None);
                    tilemap.SetColor(pos, color);
                }
            }
        }
    }

    // Call this method from other scripts to generate/update nodes
    public void GenerateNodes()
    {
        nodes.Clear(); // Clear existing nodes

        // Loop through all Tilemaps
        foreach (Tilemap tilemap in tilemaps)
        {
            // Loop through all positions in the Tilemap
            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {
                if (tilemap.HasTile(pos))
                {
                    TileBase tile = tilemap.GetTile(pos);
                    float baseValue = GetBaseValue(tile.name);
                    nodes[pos] = new TileNode(pos, tile, baseValue);
                }
            }
        }

        // Update node values based on neighbors
        UpdateNodeValues();

        // Apply building radius effects
        ApplyBuildingRadiusEffects();
    }

    // Update all node values based on their neighbors
    private void UpdateNodeValues()
    {
        foreach (var kvp in nodes)
        {
            Vector3Int pos = kvp.Key;
            TileNode node = kvp.Value;

            // Reset to base value
            node.value = GetBaseValue(node.tile.name);

            // Check all 4 neighbors (up, down, left, right)
            foreach (Vector3Int dir in new Vector3Int[] {
                new Vector3Int(1, 0, 0),
                new Vector3Int(-1, 0, 0),
                new Vector3Int(0, 1, 0),
                new Vector3Int(0, -1, 0)
            })
            {
                Vector3Int neighborPos = pos + dir;
                if (nodes.ContainsKey(neighborPos) && nodes[neighborPos].tile.name == "wall_0")
                {
                    node.value += 1f; // Bonus for adjacent wall_0
                }
            }
        }
    }

    // Apply radius effect for buildings
    private void ApplyBuildingRadiusEffects()
    {
        foreach (var kvp in nodes)
        {
            Vector3Int buildingPos = kvp.Key;
            TileNode buildingNode = kvp.Value;

            // Check if this tile is a building
            if (buildingBaseValues.ContainsKey(buildingNode.tile.name))
            {
                // Check all tiles in a 7x7 area (radius 3) around the building
                for (int dx = -3; dx <= 3; dx++)
                {
                    for (int dy = -3; dy <= 3; dy++)
                    {
                        Vector3Int affectedPos = buildingPos + new Vector3Int(dx, dy, 0);
                        if (nodes.ContainsKey(affectedPos))
                        {
                            nodes[affectedPos].value += 1.5f; // Increase value
                        }
                    }
                }
            }
        }
    }

    // Get the base value for a tile type
    private float GetBaseValue(string tileName)
    {
        if (tileBaseValues.ContainsKey(tileName))
            return tileBaseValues[tileName];
        if (buildingBaseValues.ContainsKey(tileName))
            return buildingBaseValues[tileName];
        return 0f; // Default value for unknown tiles
    }
}