using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScore : MonoBehaviour
{
   public int _score;
   public int _level1score = -1;
   public int _level2score = -1;
   public int _level3score = -1;
   public bool _allowLevelScoreSet = false;
   public bool _levelSetLocked = false;
   private int _deathPenalty = 1000;
   private Text _scoreTextUI;
   private GameStateManager _gameStateManager;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
   void Start()
   {
       GameObject.DontDestroyOnLoad(this.gameObject);
        try
        {
            _scoreTextUI = GameObject.Find("ScoreCounter").GetComponent<Text>();
            _gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();
        }
        catch(NullReferenceException e)
        {
            Debug.Log(e.Message);
            Debug.Log("Could not find Score UI or Game State Manager.");
        }
   }

   void OnSceneLoaded(Scene s, LoadSceneMode l)
   {
        _levelSetLocked = false;
        _allowLevelScoreSet = false;

        //
       try
       {
            _scoreTextUI = GameObject.Find("ScoreCounter").GetComponent<Text>();
       }
       catch(NullReferenceException e)
       {
            Debug.Log(e.Message);
            Debug.Log("Could not find Score UI.");
       }

       //
       try
       {
            _gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();
       }
       catch(NullReferenceException e)
       {
            Debug.Log(e.Message);
            Debug.Log("Could not find Game State Manager.");
       }

       if(_scoreTextUI != null)
       {
           _scoreTextUI.text = _score.ToString();
       }
   }

   void Update()
   {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(this.gameObject);
        }

        if(_gameStateManager != null)
        {
            if(_gameStateManager.StateIsEndLevel() == true)
            {
               _allowLevelScoreSet = true;
            }

            if(_levelSetLocked == false && _allowLevelScoreSet == true)
            {
                if(_level1score == -1)
                {
                    _level1score = _score;
                    Debug.Log("Level 1 complete. Updating score");
                    _levelSetLocked = true;
                    _allowLevelScoreSet = false;
                }
                else if(_level2score == -1)
                {
                    _level2score = _score;
                    Debug.Log("Level 2 complete. Updating score");
                    _levelSetLocked = true;
                    _allowLevelScoreSet = false;
                }
                else if(_level3score == -1)
                {
                    _level3score = _score;
                    Debug.Log("Level 3 complete. Updating score");
                    _levelSetLocked = true;
                    _allowLevelScoreSet = false;
                }
            }
        }
   }
    public void IncreaseScore(int scoreValue)
    {
        _score += scoreValue;
        _scoreTextUI.text = _score.ToString();
    }
    public void DecreaseScore()
    {
        _score -= _deathPenalty;

        if (_score<=0)
        {
            _score = 0;
        }
        _scoreTextUI.text = _score.ToString();
    }

    public float GetLevelScore(int level)
    {
        switch(level)
        {
            case 1:
                return _level1score;
            case 2:
                return _level2score;
            case 3:
                return _level3score;
            default:
                return -1;
        }
    }
}
