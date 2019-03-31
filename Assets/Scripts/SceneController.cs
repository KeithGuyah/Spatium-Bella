using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private int sceneNumber = 0;
    private int sceneNumberMax = 5;

    public void LoadNextLevel()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        //sceneNumberMax = SceneManager.sceneCount;

        if(sceneNumber != sceneNumberMax)
        {
            sceneNumber++;
            SceneManager.LoadScene(sceneNumber);
        }
        else
        {
            Debug.Log("Invalid scene index. Returning to title screen.");
            LoadTitle();
        }
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("titleScreen");
    }
}
