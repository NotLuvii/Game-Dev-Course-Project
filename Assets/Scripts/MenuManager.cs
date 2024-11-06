using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
        //EditorApplication.isPlaying = false;
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(8);
    }
}
