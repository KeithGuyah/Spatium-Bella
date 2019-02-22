using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public bool _isPlayer = false;
    public bool _isShield = false;
    public int _maxHP = 10;
    private int _currentHP = 1;
    private Animator _entityAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _entityAnimator = GetComponent<Animator>();
        _currentHP = _maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        //if (_currentHP > _maxHP)
        //{
        //    _currentHP = _maxHP;
        //
        _entityAnimator.SetInteger("currentHP", _currentHP);
    }

    public void TakeDamage(int damage)
    {
        _currentHP -= damage;
    }

    public void EntityDestroyStart()
    {
        // Prevent's shots from blocking entities playing the destroy animation. Also prevents the player from moving when they run out of health.
        GetComponent<Collider2D>().enabled = false;

        //Disable the player's controls.
        if(_isPlayer)
        {
            GetComponent<PlayerControls>().DisableControls();
        }
    }
    public void EntityDestroyEnd()
    {
        if (_isShield == true)
        {
        
        }
        else //Enemy or Player
        {
            Destroy(gameObject);
        }
        //put restart level here.
        //Application.LoadLevel(Application.loadedLevel);
    }
}
