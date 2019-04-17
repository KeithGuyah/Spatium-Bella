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
    public bool _movementEnabled = true;
    public float _movementDelay = 0;
    public float _velocityV = 5.0f;
    public float _velocityH = 0.0f;
    public float _lifeTime = 0.5f;
    public int _weaponDamage = 1;
    public bool _tracksPlayer = false;
    public bool _trackPlayerXOnly = false;
    public float _trackingSpeed = 5.0f;
    public bool _destroyOnHit = true;
    private Vector2 _trackingDirection;
    private float _timeElapsed = 0;
    private Rigidbody2D _shotBody;
    private GameStateManager _gameStateManager;
    private Transform _playerPosition;

    public void SetVelocity(Vector2 _newVelocity)
    {
        _velocityH = _newVelocity.x;
        _velocityV = _newVelocity.y;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        /*
        gets the rigid body component of the shot
         */
        _shotBody = GetComponent<Rigidbody2D>();

        _gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();

        if(_tracksPlayer || _trackPlayerXOnly)
        {
            // Find player's position
            _playerPosition = GameObject.Find("Player").GetComponent<Transform>();

            if(_tracksPlayer)
            {
                // Create vector2
                _trackingDirection = _playerPosition.position - gameObject.transform.position;
            }
            else if(_trackPlayerXOnly)
            {
                _movementEnabled = false;
                Invoke("EnableMovement", _movementDelay);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        defines the velocity of the burst shot.
         */
         if(_gameStateManager.StateIsRunning())
         {
            if(_movementEnabled)
            {
                if(_tracksPlayer)
                {
                    _shotBody.velocity = _trackingDirection.normalized * _trackingSpeed;
                }
                else if(_trackPlayerXOnly)
                {
                    
                    //Track player X
                    if(_playerPosition.position.x > transform.position.x)
                    {
                        _shotBody.velocity = new Vector2(_trackingSpeed,_velocityV);
                    }
                    else if(_playerPosition.position.x < transform.position.x)
                    {
                        _shotBody.velocity = new Vector2(-_trackingSpeed,_velocityV);
                    }

                    if(System.Math.Round(_playerPosition.position.x,1) == System.Math.Round(transform.position.x,1))
                    {
                        _shotBody.velocity = new Vector2(0,_velocityV);
                    }
                }
                else
                {
                    _shotBody.velocity = new Vector2(_velocityH, _velocityV);
                }
            }

            // Removes the entity after a set amount of time.
            _timeElapsed += Time.deltaTime;

            if (_timeElapsed >= _lifeTime)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            _shotBody.velocity = new Vector2(0, 0);
        }
    }

    void EnableMovement()
    {
        _movementEnabled = true;
    }

    void RemoveProjectile()
    {
        if(_destroyOnHit)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D objectHit)
    {
        if (objectHit.gameObject.CompareTag("Enemy"))
        {
            if(!gameObject.CompareTag("Projectile"))
            {
                objectHit.gameObject.GetComponent<HealthHandler>().TakeDamage(_weaponDamage);
                RemoveProjectile();
            }
        }
        else if(objectHit.gameObject.CompareTag("Player"))
        {
            if(!GameObject.Find("Shield").GetComponent<CircleCollider2D>().enabled)
            {
                objectHit.gameObject.GetComponent<HealthHandler>().TakeDamage(_weaponDamage);
                RemoveProjectile();
            }
        }
        else if(objectHit.gameObject.CompareTag("MainCamera") )
        {
            RemoveProjectile();
        }
        else if(objectHit.gameObject.CompareTag("Destructible") || objectHit.gameObject.CompareTag("Indestructable"))
        {
            if(objectHit.gameObject.CompareTag("Destructible"))
            {
                EnvironmentCollisionHandler _enviromentCollisionScript = objectHit.gameObject.GetComponent<EnvironmentCollisionHandler>();

                Vector3 _collisionVector = new Vector3(transform.position.x,transform.position.y + 0.3f,transform.position.z);
                _enviromentCollisionScript.RemoveTile(_collisionVector);

                _collisionVector.x += 0.1f;
                _enviromentCollisionScript.RemoveTile(_collisionVector);

                _collisionVector.x -= 0.2f;
                _enviromentCollisionScript.RemoveTile(_collisionVector);

                // Remove the projectile
                RemoveProjectile();
            }
            else
            {
                RemoveProjectile();
            }
        }
        else if (objectHit.gameObject.CompareTag("PlayerShield"))
        {
            if (objectHit.gameObject.CompareTag("Enemy") || gameObject.CompareTag("Projectile"))
            {
                objectHit.gameObject.GetComponent<ShieldHandler>().TakeDamage(_weaponDamage);
                RemoveProjectile();
            }
        }

    }
}
