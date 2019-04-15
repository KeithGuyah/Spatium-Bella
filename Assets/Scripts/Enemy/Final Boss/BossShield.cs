using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    private Transform _player;
    public EnemyShotHandler _shotHandler;
    public Rigidbody2D _rigidBody2D;
    public float _shotTimer = 0;
    public float _shotTimerMax = 5;
    public float _speed = 5;
    public bool _movingRight = false;
    private GameStateManager _gameStateManager;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();
        _shotTimer = _shotTimerMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(_gameStateManager.StateIsRunning())
        {
            //Track player X
            if(_player.position.x > transform.position.x)
            {
                _rigidBody2D.velocity = new Vector2(_speed,0);
            }
            else if(_player.position.x < transform.position.x)
            {
                _rigidBody2D.velocity = new Vector2(-_speed,0);
            }
             
            if(System.Math.Round(_player.position.x,1) == System.Math.Round(transform.position.x,1))
            {
                _rigidBody2D.velocity = new Vector2(0,0);
            }

            // Fire weapon
            _shotTimer -= Time.deltaTime;
            if (_shotTimer <= 0)
            {
                _shotHandler.FireProjectile();
                _shotTimer = _shotTimerMax;
            }
        }
        else
        {
            _rigidBody2D.velocity = new Vector2(0,0);
        }
    }
}
