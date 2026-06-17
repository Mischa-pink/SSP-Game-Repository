using Unity.Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public GameObject player;
    public Camera mainCamera;
    public CinemachineCamera playerCam;


    public float zoomIn = -15f;
    public float zoomOut = 15f;

    public void ZoomOutButton()
    {
        player.SetActive(false);
        player.transform.position = new Vector3(16f, -5f, 0f);

        
        playerCam.Lens.OrthographicSize = zoomOut;
    }

    public void ZoomInButton()
    {
        player.SetActive(true);
        player.transform.position = new Vector3(-13f, 0f, 0f);


        playerCam.Lens.OrthographicSize = zoomIn;
    }
}