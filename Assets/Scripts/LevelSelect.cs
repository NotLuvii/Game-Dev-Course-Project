using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void SelectLevel(int level)
    {
        if(level != 0)
        {
            SceneManager.LoadSceneAsync(1);
            SceneManager.LoadSceneAsync(level);
        }
        else
        {
            SceneManager.LoadScene(level);
        }
    }
}
