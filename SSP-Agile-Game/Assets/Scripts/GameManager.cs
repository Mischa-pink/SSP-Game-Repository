using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UnityEvent OnWaveStarted = new UnityEvent();

    public GameObject player;
    public CinemachineCamera playerCam;
    public float zoomIn = 6f;
    public float zoomOut = 15f;

    public int enemiesAlive;
    public int amountOfKills;

    public bool waveActive;
    public GameObject BuildingPanel;
    public GameObject WaveButton;

    public int currentWave = 1;

    public MoneyHolder moneyHolder;


    private void Awake()
    {
        Instance = this;
    }

    public void StartWave(int enemyCount)
    {
        enemiesAlive = enemyCount;
        waveActive = true;

        Debug.Log("Wave started with: " + enemiesAlive);

        OnWaveStarted.Invoke();
    }


    public void EnemyDied()
    {
        enemiesAlive--;

        if (waveActive && enemiesAlive <= 0)
        {

            waveActive = false;

            moneyHolder.AddMoney(500);

            player.transform.position = new Vector3(-9f, 0f, 0f);
    

            playerCam.Lens.OrthographicSize = zoomIn;

            WaveButton.SetActive(true);

            BuildingPanel.SetActive(true);

            player.SetActive(true);

            currentWave++;
        }
    }

    public void AddKill()
    {
        amountOfKills++;
        //Debug.Log("Kills: " + amountOfKills);
    }
}
