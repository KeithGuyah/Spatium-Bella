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
    private CircleCollider2D _powerUpCollider;
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D objectHit)
    {
        if (objectHit.gameObject.tag == "Player")
        {
            Debug.Log(objectHit.gameObject.tag);
            if (_isSheild_PU == true)
            {
                Debug.Log("Collison detected");
                powerUpShield();
            }
            if (_isShieldMaxHP_PU == true)
            {
                Debug.Log("Collison detected");
                powerUpShieldMaxHP();
            }
            if (_isLaserCannonEnabled_PU == true)
            {
                Debug.Log("Collison detected");
                powerUpEnableLaserCannon();
            }
            if (_isSpreadShotEnabled_PU == true)
            {
                Debug.Log("Collison detected");
                powerUpEnableSpreadShot();
            }
            Destroy(gameObject);
        }
    }

    void powerUpShield()
    {
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
        GameObject.Find("Player").GetComponent<PlayerControls>().EnableLaserCannon();
    }
    void powerUpEnableSpreadShot()
    {
        GameObject.Find("Player").GetComponent<PlayerControls>().EnableSpreadShot();
    }

}
