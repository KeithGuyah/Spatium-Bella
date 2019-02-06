using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstShot : MonoBehaviour
{
        /*
    First variable below will define the vertical velocity of the burst shot.
    Second variable below defines the horizontal velocity of the burst shot.
    Third variable defines the shots body.
     */
   public float _velocityV=5.0f;
   public float _velocityH=0.0f;

   public float _lifeTime = 0.5f;

   private float _timeElapsed = 0;
   private Rigidbody2D _shotBody;
    // Start is called before the first frame update
    
    void Start()
    {
        /*
        gets the rigid body component of the shot
         */
        _shotBody=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        defines the velocity of the burst shot.
         */
        _shotBody.velocity=new Vector2(_velocityH,_velocityV);

        // Removes the entity after a set amount of time.
        _timeElapsed += Time.deltaTime;
        if(_timeElapsed >= _lifeTime)
        {
            Destroy(gameObject);
        }
    }

}
