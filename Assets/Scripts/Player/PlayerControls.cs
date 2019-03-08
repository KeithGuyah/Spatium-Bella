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
   public float _fireRate = 0.25f;
   private float _nextFire = 0.0f;
   private Rigidbody2D _playerBody;
   private WeaponsHandler shootProjectile;
   private ShieldHandler _shield;
   private Animator _playerAnimator;
   public bool _laserCannonEnabled = false;
   public bool _spreadShotEnabled = false;
   private bool _controlsEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        /*
        gets the rigid body component for the player.
         */
        _weaponNumber = 1;
        _playerBody = GetComponent<Rigidbody2D>();
        shootProjectile = GetComponent<WeaponsHandler>();
        _playerAnimator = GetComponent<Animator>();
        _shield = GameObject.Find("PlayerShield").GetComponent<ShieldHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_controlsEnabled)
        {
            //Fire Weapon
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
            {
                _nextFire = _fireRate + Time.time;
                shootProjectile.FireWeapon(_weaponNumber);
            }
            else if (Input.GetKey(KeyCode.Space) && _weaponNumber == 2) // Special firing case for the laser shot.
            {
                //shootProjectile.FireWeapon(_weaponNumber);
                shootProjectile.LaserCannonEnable();
            }
            else if (Input.GetKeyUp(KeyCode.Space) && _weaponNumber==2)
            {
                shootProjectile.LaserCannonDisable();
            }
            //Weapon Switching
            if (Input.GetKeyDown(KeyCode.Q))
            {
                /*
                 
            if (_weaponNumber == 3)
                {
                    _weaponNumber = 1;
                }
            else
                {
                    _weaponNumber++;
                }
                Debug.Log("Selected Weapon: " + _weaponNumber);
                 */
                 if (_weaponNumber==1 && _laserCannonEnabled)
                {
                    _weaponNumber = 2;
                }
                 else if (_weaponNumber==1 && _spreadShotEnabled)
                {
                    _weaponNumber = 3;
                }
                else if (_weaponNumber == 2 && _spreadShotEnabled)
                {
                    _weaponNumber = 3;
                }
                 else if (_weaponNumber == 2 && _spreadShotEnabled==false)
                {
                    _weaponNumber = 1;
                }
                else if (_weaponNumber == 3)
                {
                    _weaponNumber = 1;
                }
                 else
                {
                    _weaponNumber = 1;
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
        int hAxis = 0;
        int vAxis = 0;
        
        if(_controlsEnabled)
        {
            hAxis = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
            vAxis = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        }

        /*
        sets the velocity the player can move at on the horizontal and vertical axis.
        */
        _playerBody.velocity = new Vector2(hAxis * _velocity, vAxis * _velocity);

        //Update animator
        _playerAnimator.SetInteger("horizMovement", hAxis);
    }

    public void DisableControls()
    {
        _controlsEnabled = false;
    }

    public void EnableControls()
    {
        _controlsEnabled = true;
    }
    public void EnableLaserCannon()
    {
        _laserCannonEnabled = true;
    }
    public void EnableSpreadShot()
    {
        _spreadShotEnabled = true;
    }
}