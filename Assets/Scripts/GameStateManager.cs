﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameStateManager : MonoBehaviour
{
    public enum state {running, paused, endLevel, atContinueScreen, gameOver};
    public state gameState;
    private bool _playedIntro = false;
    private Animator _uiAnimator;
    private Text _continueCounter;
    private float _timer = 10;
    public StageAudio _stageAudio;

    // Start is called before the first frame update
    void Start()
    {
        gameState = state.running;
        _uiAnimator = GameObject.Find("Ui Canvas").GetComponent<Animator>();
        _continueCounter = GameObject.Find("Continue Timer").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(gameState)
        {
            case state.running:
                // Play level intro at the start of the level
                if(!_playedIntro)
                {
                    _uiAnimator.SetBool("startIntro", true);
                    _playedIntro = true;
                }
            break;
            case state.atContinueScreen:
                if(_stageAudio._fadeVolumeOverTime == false)
                {
                    _stageAudio.FadeVolumeOnTimer(10);
                }

                _continueCounter.text = Mathf.RoundToInt(_timer).ToString();
                _timer -= Time.deltaTime;

                if(Input.anyKeyDown)
                {
                    _timer = 10;
                    _stageAudio.UndoFadeVolumeOnTimer();
                    GameObject.Find("Player").GetComponent<LivesHandler>().RefreshLives();
                    _uiAnimator.SetBool("showContinue", false);
                    SetRunning();
                }
                if(_timer <= 0)
                {
                    _timer = 0;
                    SetGameOver();
                }
            break;
            case state.endLevel:
                if(_stageAudio.ReturnVolumeLevel() > 0)
                {
                    _stageAudio.FadeVolumeTo(0, true);
                }
            break;
            case state.gameOver:
                _stageAudio._fadeVolumeOverTime = false;
            break;
        }
    }

    public bool StateIsEndLevel()
    {
        if(gameState == state.endLevel)
        {
            return true;
        }
        else
        {
            return false;
        } 
    }

    public bool StateIsRunning()
    {
        if(gameState == state.running)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetEndLevel()
    {
        _uiAnimator.SetBool("startOutro", true);
        gameState = state.endLevel;  
    }

    public void SetGameOver()
    {
        _uiAnimator.SetBool("gameOver", true);
        gameState = state.gameOver;
    }

    public void SetContinue()
    {
        _uiAnimator.SetBool("showContinue", true);
        gameState = state.atContinueScreen;
    }
    
    public void SetRunning()
    {
        gameState = state.running;
    }
}
