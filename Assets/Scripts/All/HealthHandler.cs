using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    public bool _isPlayer = false;
    public bool _isShield = false;
    public bool _isFinalBoss = false;
    public int _maxHP = 1;
    public int _currentHP = 1;
    public int _scoreValue = 0;
    private Animator _entityAnimator;
    private Collider2D _entityCollider2D;
    private LivesHandler _playerLivesHandler;
    public Text _healthBarText;
    public Image _healthSlider;
    public EntityAudio _entityAudio;

    // Start is called before the first frame update
    void Start()
    {
        _entityAnimator = GetComponent<Animator>();
        _entityCollider2D = GetComponent<Collider2D>();
        _playerLivesHandler = GetComponent<LivesHandler>();
        _currentHP = _maxHP;
        _entityAnimator.SetInteger("currentHP", _currentHP);
        setHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (_currentHP <= damage)
        {
            _currentHP = 0;
            score();
        }
        else
        {
            _currentHP -= damage;
        }

        if(_isPlayer && _playerLivesHandler.IsPlayerInvincible())
        {
            _currentHP = _maxHP;
        }

        setHealthUI();

        if(_isFinalBoss && _currentHP == 0)
        {
            //Debug.Log("FINAL BOSS HEALTH IS 0");
        }
        else
        {
            _entityAnimator.SetInteger("currentHP", _currentHP);
        }

        if(!_entityAnimator.gameObject.CompareTag("Player"))
        {
            _entityAnimator.SetTrigger("playDamageAnimation");
        }
    }
    public void setHealthUI()
    {
        if (_isPlayer)
        {
            _healthBarText.text = _currentHP + "/" + _maxHP;
            _healthSlider.fillAmount = (float)_currentHP / (float)_maxHP;
        }
    }
    public void score()
    {
        if (_isPlayer == false)
        {
            try
            {
                GameObject.Find("Score Object").GetComponent<PlayerScore>().IncreaseScore(_scoreValue);
            }
            catch(NullReferenceException e)
            {
                Debug.Log(e.Message);
                Debug.Log("Could not find score object. Cannot increase score.");
            }
        }
        if (_isPlayer)
        {
            try
            {
                GameObject.Find("Score Object").GetComponent<PlayerScore>().DecreaseScore();
            }
            catch(NullReferenceException e)
            {
                Debug.Log(e.Message);
                Debug.Log("Could not find score object. Cannot decrease score.");
            }
        }
    }
    public void SetHealth(int value)
    {
        _currentHP = value;
        _entityAnimator.SetInteger("currentHP", _currentHP);

        setHealthUI();
    }

    public void EntityDestroyStart() // Activates when the 'currentHP' float in the entities animator component is <= 0. 
    {
        if(_isPlayer)
        {
            _playerLivesHandler.PlayerDeathStart();
        }
        else //Enemy
        {
            //Stop enemy movement while the destroy animation is playing.
            try
            {
                GetComponent<EnemyMovementHandler>().StopMovement();
            }
            catch(NullReferenceException e)
            {
                Debug.Log(e.Message);
            }

            _entityAudio.PlayDestroyAudio();

            // Allows player projectiles to pass through enemy objects playing the 'destroy' animation.
            _entityCollider2D.enabled = false;
        }
    }
    public void EntityDestroyEnd()
    {

        if(_isPlayer)
        {
            _playerLivesHandler.PlayerDeathEnd();
        }
        else //Enemy
        {
            Destroy(gameObject);
        }
    }
}
