using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject container;

    void Update()
    {
        // If the escapekey is pressed, pause the game
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            container.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeButton()
    {
        // If the resume button is pressed, unpause the game
        container.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenuButton()
    {
        // If the main menu button is pressed, go to the main menu
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start Menu-Joshua"); //currently to demo scene, will change later
        Time.timeScale = 1;


    }
}
