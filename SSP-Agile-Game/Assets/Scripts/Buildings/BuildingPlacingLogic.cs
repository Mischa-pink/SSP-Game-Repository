using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class BuildingPlacingLogic : MonoBehaviour
{
    [SerializeField] PlayerInput input;
    [SerializeField] Tilemap tilemap;
    private InputAction click;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        click = input.actions["Click"];
    }

    // Update is called once per frame
    void Update()
    {

        // test for click
        if (click.WasPressedThisFrame())
        {
            gameObject.SetActive(false);
        }

        if (gameObject.activeSelf)
        {
            // Get Mouse pos
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 mouseCellPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
            Vector3Int worldCell = tilemap.WorldToCell(mouseCellPos);

            Debug.Log(worldCell);
            gameObject.transform.position = worldCell;
        }
    }
}
