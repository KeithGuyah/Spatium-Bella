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
   Vector2 burstShotStart;
   float burstShotFireRate=0.5f;
      float nextFire=0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void shoot()
    {
        //burstShotStart=transform.position;
        //if(Time.time >nextFire)
        //{
            Instantiate(BurstShot,transform.position,transform.rotation);
        //}
    }
}
