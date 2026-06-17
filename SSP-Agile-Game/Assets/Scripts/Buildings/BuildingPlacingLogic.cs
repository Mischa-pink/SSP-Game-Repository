using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class BuildingPlacingLogic : MonoBehaviour
{
    [SerializeField] PlayerInput input;
    [SerializeField] Tilemap tileMap;
    [SerializeField] MoneyHolder moneyHolder;
    [SerializeField] GameObject shootPosPrefab;
    [SerializeField] GameObject shootHolder;

    private InputAction click;

    private Vector3Int worldCell;

    public int buildingSizeX;
    public int buildingSizeY;

    [SerializeField] int curserOffsetX;
    [SerializeField] int curserOffsetY;

    public int buildingPrice;

    public TileBase[] tiles;

    public List<Vector3Int> placedTiles = new List<Vector3Int>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        click = input.actions["Click"];
    }

    // Update is called once per frame
    void Update()
    {
        // Get Mouse pos
        GetMouseGridPos();

        // test for click
        if (click.WasPressedThisFrame())
        {
            PlaceBuildingOnGrid(GetMouseGridPos());
            GameObject shootPos = Instantiate(shootPosPrefab, transform.position, Quaternion.identity);
            shootPos.transform.SetParent(shootHolder.transform);

            gameObject.SetActive(false);
        }

        // Places the transparent image of the building to its positon 
        if (gameObject.activeSelf)
        {
            gameObject.transform.position = worldCell;
        }
    }

    // Get the position of the curser on the grid
    Vector3Int GetMouseGridPos()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 mouseCellPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
        worldCell = tileMap.WorldToCell(mouseCellPos);
        return worldCell;
    }

    // Places the tiles based on the position (and offset) off the 
    void PlaceBuildingOnGrid(Vector3Int placePos)
    {
        // Tests if there is already a building and if the money is bigger then the building price
        if (IsEmptyTiles(placePos) && moneyHolder.moneyAmount >= buildingPrice)
        {
            int totalTiles = 0;
            placePos = new Vector3Int(placePos.x + curserOffsetX, placePos.y + curserOffsetY);
            for (int y = 1; y <= buildingSizeY; y++)
            {
                for (int x = 1; x <= buildingSizeX; x++)
                {
                    // Places tiles
                    Vector3Int placeAt = new Vector3Int(placePos.x + x, placePos.y - y, 0);
                    tileMap.SetTile(placeAt, tiles[totalTiles]);
                    placedTiles.Add(placeAt);
                    totalTiles++;
                }
            }

            // Decrease Money 
            moneyHolder.decreaseMoney(buildingPrice);
        }

    }

    bool IsEmptyTiles(Vector3Int checkPos)
    {
        checkPos = new Vector3Int(checkPos.x + curserOffsetX, checkPos.y + curserOffsetY);
        for (int y = 1; y <= buildingSizeY; y++)
        {
            for (int x = 1; x <= buildingSizeX; x++)
            {
                Vector3Int lookAt = new Vector3Int(checkPos.x + x, checkPos.y - y, 0);

                if (placedTiles.Contains(lookAt)) {return false;}
            }
        }
        return true;
    }
}
