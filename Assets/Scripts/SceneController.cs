using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private int sceneNumber = 0;
    private int sceneNumberMax = 5;
    void Start()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        sceneNumberMax = SceneManager.sceneCountInBuildSettings - 1;

        //Debug.Log("Current scene index: " + sceneNumber);
        //Debug.Log("Final scene index: " + sceneNumberMax);
    }
    public void LoadNextLevel()
    {
        if(sceneNumber == sceneNumberMax)
        {
            Debug.Log("Reached last scene. Returning to title screen.");
            LoadTitle();
        }
        else
        {
            sceneNumber++;
            SceneManager.LoadScene(sceneNumber);
        }
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("titleScreen");
    }
}
