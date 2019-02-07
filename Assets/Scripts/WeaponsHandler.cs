using System.Collections;
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
   public GameObject LaserShot;
   public GameObject _spreadShot;
   public float _horzOffset = 0.50f;
   public float _vertOffset = 1.0f;
   public float _fireRate = 0.5f;
   private float nextFire = 0.0f;
   private Vector2 shotStartPos;
   
    public void fireWeapon(int weaponNumber)
    {
        if(Time.time > nextFire && weaponNumber != 2)
        {
            nextFire = Time.time + _fireRate;
            if (weaponNumber == 3)
            {
                spreadShot();
            }
            else
            {
                burstShot();
            }
        }
        else if (weaponNumber == 2)
        {
            laserCannon();
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
        Instantiate(_spreadShot, shotStartPos, transform.rotation);
    }
    void laserCannon()
    {
        shotStartPos = transform.position;
        shotStartPos += new Vector2(0,_vertOffset);
        Instantiate(LaserShot, shotStartPos, transform.rotation);
    }

}
