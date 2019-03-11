using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    public enum state {running, paused, endLevel, atContinueScreen, gameOver};
    public state gameState;
    private bool _playedIntro = false;
    private Animator _uiAnimator;
    private Text _continueCounter;
    private float _continueTimer = 10;

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
            _continueCounter.text = Mathf.RoundToInt(_continueTimer).ToString();
            _continueTimer -= Time.deltaTime;
                if(Input.anyKeyDown)
                {
                    GameObject.Find("Player").GetComponent<LivesHandler>().RefreshLives();
                    _uiAnimator.SetBool("showContinue", false);
                     SetRunning();
                }
                if(_continueTimer <= 0)
                {
                    _continueTimer = 0;
                    SetGameOver();
                }
            break;
            case state.gameOver:
            
            break;
        }
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
