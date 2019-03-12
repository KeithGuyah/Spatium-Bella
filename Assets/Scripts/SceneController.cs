using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    int sceneNumber = 0;

    public void LoadNextLevel()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        
        if(sceneNumber != 3)
        {
            sceneNumber++;
        }
        else
        {
            Debug.Log("Invalid scene index. Returning to title screen.");
            LoadTitle();
        }

            SceneManager.LoadScene("level" + sceneNumber);
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("titleScreen");
    }
}
