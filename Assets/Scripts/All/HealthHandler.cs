using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    public bool _isPlayer = false;
    public bool _isShield = false;
    public int _maxHP = 1;
    public int _currentHP = 1;
    private Animator _entityAnimator;
    private Collider2D _entityCollider2D;
    private LivesHandler _playerLivesHandler;
    public Text _healthBarText;
    public Image _healthSlider;

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

    // Update is called once per frame
    void Update()
    {
        //if (_currentHP > _maxHP)
        //{
        //    _currentHP = _maxHP;
        //
    }

    public void TakeDamage(int damage)
    {
      
        if (_currentHP <= damage)
        {
            _currentHP = 0;
        }
        else
        {
            _currentHP -= damage;
        }
        setHealthUI();
        _entityAnimator.SetInteger("currentHP", _currentHP);
    }
    public void setHealthUI()
    {
        if (_isPlayer)
        {
            _healthBarText.text = _currentHP + "/" + _maxHP;
            _healthSlider.fillAmount = (float)_currentHP / (float)_maxHP;
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
        if (_isShield)
        {
        
        }
        else if(_isPlayer)
        {
            _playerLivesHandler.PlayerDeathStart();
        }
        else //Enemy
        {
            //Stop enemy movement while the destroy animation is playing.
            GetComponent<EnemyMovementHandler>().StopMovement();

            // Allows player projectiles to pass through enemy objects playing the 'destroy' animation.
            _entityCollider2D.enabled = false;
        }
    }
    public void EntityDestroyEnd()
    {
        if (_isShield)
        {
            
        }
        else if(_isPlayer)
        {
            _playerLivesHandler.PlayerDeathEnd();
        }
        else //Enemy
        {
            Destroy(gameObject);
        }
        //put restart level here.
        //Application.LoadLevel(Application.loadedLevel);
    }
}
