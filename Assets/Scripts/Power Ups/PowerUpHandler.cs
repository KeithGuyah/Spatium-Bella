﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHandler : MonoBehaviour
{
    public int _powerUpValue = 1;
    public bool _isSheild_PU = false;
    public bool _isWeapons_PU = false;
    public bool _isShieldMaxHP_PU = false;
    public bool _isLaserCannonEnabled_PU = false;
    public bool _isSpreadShotEnabled_PU = false;
    public bool _static = true;
    private Rigidbody2D _powerUpRigidBody;
    private Vector2 _movementVelocity;
    private float _timer = 0;

    void Start()
    {
        _powerUpRigidBody = GetComponent<Rigidbody2D>();
        
        if(_static)
        {
            _movementVelocity = new Vector2(0,0);
        }
        else
        {
            _movementVelocity = new Vector2(4,4);
        }
    }

    void Update()
    {
        if(!_static)
        {
            _powerUpRigidBody.velocity = _movementVelocity;
        }
        else
        {
            _powerUpRigidBody.velocity = new Vector2(0,0);
        }

        _timer = Time.deltaTime;
        if(_timer >= 10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D objectHit)
    {
        if (objectHit.gameObject.tag == "Player")
        {
            //Debug.Log(objectHit.gameObject.tag);
            if (_isSheild_PU == true)
            {
                //Debug.Log("Collison detected");
                powerUpShield();
            }
            if (_isShieldMaxHP_PU == true)
            {
                //Debug.Log("Collison detected");
                powerUpShieldMaxHP();
            }
            if (_isLaserCannonEnabled_PU == true)
            {
                //Debug.Log("Collison detected");
                powerUpEnableLaserCannon();
            }
            if (_isSpreadShotEnabled_PU == true)
            {
                //Debug.Log("Collison detected");
                powerUpEnableSpreadShot();
            }
            Destroy(gameObject);
        }
        else if(objectHit.gameObject.tag == "MainCamera" && !_static)
        {
            if(objectHit.name == "CameraTriggerLeft" || objectHit.name == "CameraTriggerRight")
            {
                _movementVelocity = new Vector2(_powerUpRigidBody.velocity.x * -1, _powerUpRigidBody.velocity.y);
            }
            else if(objectHit.name == "CameraTriggerUp" || objectHit.name == "CameraTriggerDown")
            {
                _movementVelocity = new Vector2(_powerUpRigidBody.velocity.x ,_powerUpRigidBody.velocity.y * -1);
            }
        }
    }

    void powerUpShield()
    {
        GameObject.Find("Player Audio").GetComponent<EntityAudio>().PlayPowerUpAudio(3);
        GameObject.Find("PlayerShield").GetComponent<ShieldHandler>().AddShieldHP(_powerUpValue);
    }

    void powerUpWeapons()
    {

    }

    void powerUpShieldMaxHP()
    {
        GameObject.Find("PlayerShield").GetComponent<ShieldHandler>().AddMaxShieldHp(_powerUpValue);
    }
    void powerUpEnableLaserCannon()
    {
        GameObject.Find("Player Audio").GetComponent<EntityAudio>().PlayPowerUpAudio(2);
        GameObject.Find("Player").GetComponent<PlayerControls>().EnableLaserCannon();
    }
    void powerUpEnableSpreadShot()
    {
        GameObject.Find("Player Audio").GetComponent<EntityAudio>().PlayPowerUpAudio(1);
        GameObject.Find("Player").GetComponent<PlayerControls>().EnableSpreadShot();
    }

}
