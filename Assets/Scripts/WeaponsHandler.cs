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
   public GameObject BurstShot;
   public GameObject _spreadShotForward;
   public GameObject _spreadShotLeft; 
   public GameObject _spreadShotRight;
   Vector2 shotStartPos;
   public float _horzOffset = 0.50f;
   public float _vertOffset=1.0f;
   float _fireRate=0.5f;
   float nextFire=0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void fireWeapon(int weaponNumber)
    {

        
        if(Time.time >nextFire)
        {
            if (weaponNumber == 2)
            {
                laserCannon();
            }
            if (weaponNumber == 3)
            {
                spreadShot();
            }
            else
            {
                burstShot();
            }
        }
    }
    void burstShot()
    {
        shotStartPos = transform.position;
        shotStartPos += new Vector2(0.0f, _vertOffset);
        Instantiate(BurstShot, shotStartPos, transform.rotation);
    }
    void spreadShot()
    {
        shotStartPos = transform.position;
        shotStartPos += new Vector2(0.0f, _vertOffset);
        Instantiate(_spreadShotForward, shotStartPos, transform.rotation);

        shotStartPos = transform.position;
        shotStartPos += new Vector2(-_horzOffset, _vertOffset);
        Instantiate(_spreadShotLeft, shotStartPos, transform.rotation);

        shotStartPos = transform.position;
        shotStartPos += new Vector2(_horzOffset, _vertOffset);
        Instantiate(_spreadShotRight, shotStartPos, transform.rotation);
    }
    void laserCannon()
    {

    }

}
