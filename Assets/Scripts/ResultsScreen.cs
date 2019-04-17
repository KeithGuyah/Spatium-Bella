using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ResultsScreen : MonoBehaviour
{
    private PlayerScore _scoreObj;
    public Text _level1Text;
    public Text _level1TextBG;
    public Text _level2Text;
    public Text _level2TextBG;
    public Text _level3Text;
    public Text _level3TextBG;
    public Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
       try
       {
            _scoreObj = GameObject.Find("Score Object").GetComponent<PlayerScore>();

            _level1Text.text = "- " + _scoreObj.GetLevelScore(1) + " PTS";
            _level2Text.text = "- " + _scoreObj.GetLevelScore(2) + " PTS";
            _level3Text.text = "- " + _scoreObj.GetLevelScore(3) + " PTS";

            _level1TextBG.text = "- " + _scoreObj.GetLevelScore(1) + " PTS";
            _level2TextBG.text = "- " + _scoreObj.GetLevelScore(2) + " PTS";
            _level3TextBG.text = "- " + _scoreObj.GetLevelScore(3) + " PTS";
       }
       catch(NullReferenceException e)
       {
            Debug.Log(e.Message);
            Debug.Log("Could not find Score Object.");
       }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.anyKeyDown)
        {
            FadeOut();
        }
    }

    void FadeOut()
    {
        _animator.SetTrigger("fadeout");
    }
}
