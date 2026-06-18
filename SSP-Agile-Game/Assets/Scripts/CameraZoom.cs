using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class CameraZoom : MonoBehaviour
{
    public GameObject player;
    public Camera mainCamera;
    public CinemachineCamera playerCam;

    public EnemySpawningScript enemySpawner;
    public Button spawnButton; // Assign in Inspector

    public GameObject BuildingPanel;
    public GameObject WaveButton;
    public GameObject PlayerButton;

    private void Start()
    {
        if (spawnButton != null)
        {
            spawnButton.onClick.AddListener(SpawnEnemies);
        }
        PlayerButton.SetActive(false);
    }

    private void SpawnEnemies()
    {
        if (enemySpawner != null)
        {
            Vector2 spawnPosition = transform.position;

            int amountToSpawn = 4 + (GameManager.Instance.currentWave - 1) * 3;

            GameManager.Instance.StartWave(amountToSpawn);
            enemySpawner.SpawnAtPosition(spawnPosition, amountToSpawn);
        }
    }


    public float zoomIn = 6f;
    public float zoomOut = 15f;

    public void ZoomOutButton()
    {
        WaveButton.SetActive(false);
        BuildingPanel.SetActive(false);
        player.SetActive(false);
        player.transform.position = new Vector3(14f, -4f, 0f);


        playerCam.Lens.OrthographicSize = zoomOut;
    }
}