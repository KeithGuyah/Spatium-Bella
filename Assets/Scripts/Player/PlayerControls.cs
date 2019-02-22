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
   public float _velocity = 1.0f;
   public int _weaponNumber = 1;
   private Rigidbody2D _playerBody;
   private WeaponsHandler shootProjectile;
   private ShieldHandler _shield;
   private bool _controlsEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        /*
        gets the rigid body component for the player.
         */
        _playerBody = GetComponent<Rigidbody2D>();
        shootProjectile = GetComponent<WeaponsHandler>();
        _shield = GameObject.Find("PlayerShield").GetComponent<ShieldHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_controlsEnabled)
        {
            //Fire Weapon
            if (Input.GetKeyDown(KeyCode.Space))
            {
                shootProjectile.FireWeapon(_weaponNumber);
            }
            else if (Input.GetKey(KeyCode.Space) && _weaponNumber == 2) // Special firing case for the laser shot.
            {
                shootProjectile.FireWeapon(_weaponNumber);
            }
            //Weapon Switching
            if (Input.GetKeyDown(KeyCode.Q))
            {
            if (_weaponNumber == 3)
                {
                    _weaponNumber = 1;
                }
            else
                {
                    _weaponNumber++;
                }
                Debug.Log("Selected Weapon: " + _weaponNumber);
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                _shield.DisableShield();
            }
            if (Input.GetKey(KeyCode.E))
            {
                _shield.EnableShield();
            }
        }
    }
    void FixedUpdate()
    {
        /*
        float varibles below get the horizontal and vertical axis.
         */
        float hAxis = 0;
        float vAxis = 0;
        
        if(_controlsEnabled)
        {
            hAxis = Input.GetAxisRaw("Horizontal");
            vAxis = Input.GetAxisRaw("Vertical");
        }

        /*
        sets the velocity the player can move at on the horizontal and vertical axis.
        */
        _playerBody.velocity = new Vector2(hAxis * _velocity, vAxis * _velocity);
    }

    public void DisableControls()
    {
        _controlsEnabled = false;
    }
}