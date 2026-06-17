using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartMenuController : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("MainGameScene"); //currently to demo scene, will change later
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
