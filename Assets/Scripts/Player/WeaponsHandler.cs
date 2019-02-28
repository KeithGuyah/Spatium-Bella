﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsHandler : MonoBehaviour
{
       /*
    First variable represents the burst shot object.
    Second variable represents the position the burst shot should be created at.
    Third variable represents the rate at which the burst shot can be fired.
    */
   public GameObject _burstShot;
   //public GameObject _laserShot;
   public GameObject _spreadShot;
   public LineRenderer _laserCannon;
   public int _laserCannonDamage=1;
   public float _laserCannonDamageRate = 1.0f;
   public float _horzOffset = 0.50f;
   public float _vertOffset = 1.0f;
   public float _fireRate = 0.5f;
   private float _nextFire = 0.0f;
   private float nextFire = 0.0f;
   private RaycastHit2D hitInfo;
   private Vector2 shotStartPos;
   
    public void FireWeapon(int weaponNumber)
    {
        if(Time.time > nextFire && weaponNumber != 2)
        {
            nextFire = Time.time + _fireRate;
            if (weaponNumber == 3)
            {
                SpreadShot();
            }
            else
            {
                BurstShot();
            }
        }
        /*
        else if (weaponNumber == 2)
        {
            LaserCannon();
        }
        */
    }
    void BurstShot()
    {
        shotStartPos = transform.position;
        shotStartPos += new Vector2(0.0f, _vertOffset);
        Instantiate(_burstShot, shotStartPos, transform.rotation);
    }
    void SpreadShot()
    {
        shotStartPos = transform.position;
        shotStartPos += new Vector2(0.0f, _vertOffset);
        Instantiate(_spreadShot, shotStartPos, transform.rotation);
    }
    
    void LaserCannon()
    {
        //shotStartPos = transform.position;
        //shotStartPos += new Vector2(0, _vertOffset);
        //Instantiate(_laserShot, shotStartPos, transform.rotation);  
        if (_laserCannon.enabled == true)
        {
            shotStartPos = transform.position;
            shotStartPos += new Vector2(0, _vertOffset);


            hitInfo = Physics2D.Raycast(shotStartPos, transform.up);
            if (hitInfo)
            {
                Debug.Log(hitInfo.transform.name + " has been hit");
            }
            _laserCannon.SetPosition(0, shotStartPos);
            if (hitInfo.transform.tag == "Enemy")
            {
                _laserCannon.SetPosition(1, hitInfo.point);
                LaserCannonDamage();   
            }
            else
            {
                _laserCannon.SetPosition(1, shotStartPos + new Vector2(0, 10));
            }
        }

    }
    public void LaserCannonDamage()
    {
        
        Debug.Log(Time.time);
        if (Time.time   > _nextFire)
       {
           _nextFire= _laserCannonDamageRate + Time.time;
            hitInfo.transform.gameObject.GetComponent<HealthHandler>().TakeDamage(_laserCannonDamage);
            
        }

        
        
    }
    
    public void LaserCannonEnable()
    {
        _laserCannon.enabled = true;
    }
    public void LaserCannonDisable()
    {
        _laserCannon.enabled = false;
    }
    void Update()
    {
        LaserCannon();
    }
}
