using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum state {running, paused, endLevel, atContinueScreen, gameOver};
    public state gameState;
    private bool _playedIntro = false;
    private Animator _uiAnimator;

    // Start is called before the first frame update
    void Start()
    {
        gameState = state.running;
        _uiAnimator = GameObject.Find("Ui Canvas").GetComponent<Animator>();
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
            case state.paused:

            break;
        }
    }
}
