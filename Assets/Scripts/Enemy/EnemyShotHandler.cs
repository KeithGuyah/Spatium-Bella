﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotHandler : MonoBehaviour
{
    public GameObject _projectile;
    public GameObject _projectile2;
    private GameObject _currentProjectile;
    private HealthHandler _healthHandler;
    public float _verticalOffset = 0;
    public float _horizontalOffset = 0;
    public float _frequency = 0;
    private float _timer = 0;
    private GameStateManager _gameStateManager;
    public EntityAudio _enemyAudio;
    void Start()
    {
        _gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();
        _healthHandler = GetComponent<HealthHandler>();
        _currentProjectile = _projectile;
    }

    // Update is called once per frame
    void Update()
    {
        if(_gameStateManager.StateIsRunning())
        {
            _timer += Time.deltaTime;

            if(_timer >= _frequency && _healthHandler._currentHP > 0)
            {
                _timer = 0;
                FireProjectile();
                SwitchProjectiles();
            }
        }
    }

    public void SwitchProjectiles()
    {
        if(_currentProjectile == _projectile && _projectile2 != null)
        {
            _currentProjectile = _projectile2;
        }
        else if(_currentProjectile == _projectile2)
        {
            _currentProjectile = _projectile;
        }
    }

    public void FireProjectile()
    {
        Instantiate(_currentProjectile, new Vector2(transform.position.x + _horizontalOffset,transform.position.y + _verticalOffset),transform.rotation);
        _enemyAudio.PlayShotAudio(1);
    }

    public void ChangeFrequency(float _newFrequency)
    {
        _frequency = _newFrequency;
    }
}
