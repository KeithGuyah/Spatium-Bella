using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    /*
    First variable below will define the velocity the player can move at.
    Second variable will define the players body.
    Third Variable reprsents the time since the last projectile was fired.
     */
   public float _velocity=1.0f;
   private Rigidbody2D _playerBody;
   private WeaponsHandler shootProjectile;
   public int _weaponNumber = 1;
  

    // Start is called before the first frame update
    void Start()
    {
        /*
        gets the rigid body component for the player.
         */
        _playerBody= GetComponent<Rigidbody2D>();
        shootProjectile = GetComponent<WeaponsHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shootProjectile.fireWeapon(_weaponNumber);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
           if (_weaponNumber==3)
            {
                _weaponNumber = 1;
            }
           else
            {
                _weaponNumber++;
            }

        }
    }
    void FixedUpdate()
    {
        /*
        float varibles below get the horizontal and vertical axis.
         */
        float hAxis=Input.GetAxis("Horizontal");
        float vAxis=Input.GetAxis("Vertical");

        /*
        sets the velocity the player can move at on the horizontal and vertical axis.
         */
        _playerBody.velocity=new Vector2(hAxis*_velocity,vAxis*_velocity);
    }
}
