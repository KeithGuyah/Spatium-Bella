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
    public float _velocityV = 5.0f;
    public float _velocityH = 0.0f;
    public float _lifeTime = 0.5f;
    public int _weaponDamage = 1;
    public bool _tracksPlayer = false;
    public float _trackingSpeed = 5.0f;
    private Vector2 _trackingDirection;
    private float _timeElapsed = 0;
    private Rigidbody2D _shotBody;
    private GameStateManager _gameStateManager;
    
    // Start is called before the first frame update
    void Start()
    {
        /*
        gets the rigid body component of the shot
         */
        _shotBody = GetComponent<Rigidbody2D>();

        _gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();

        if(_tracksPlayer)
        {
            // Find player's position
            Transform _playerPosition = GameObject.Find("Player").GetComponent<Transform>();

            // Create vector2
            _trackingDirection = _playerPosition.position - gameObject.transform.position;
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
            if(_tracksPlayer)
            {
                _shotBody.velocity = _trackingDirection.normalized * _trackingSpeed;
            }
            else
            {
                _shotBody.velocity = new Vector2(_velocityH, _velocityV);
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
    void OnTriggerEnter2D(Collider2D objectHit)
    {
        if (objectHit.gameObject.tag == "Enemy")
        {
            if(!gameObject.CompareTag("Projectile"))
            {
                objectHit.gameObject.GetComponent<HealthHandler>().TakeDamage(_weaponDamage);
                Destroy(gameObject);
            }
        }
        else if(objectHit.gameObject.tag == "Player")
        {
            if(!GameObject.Find("Shield").GetComponent<CircleCollider2D>().enabled)
            {
                objectHit.gameObject.GetComponent<HealthHandler>().TakeDamage(_weaponDamage);
                Destroy(gameObject);
            }
        }
        else if(objectHit.gameObject.tag == "MainCamera")
        {
            Destroy(gameObject);
        }
        else if (objectHit.gameObject.tag == "PlayerShield")
        {
            objectHit.gameObject.GetComponent<ShieldHandler>().TakeDamage(_weaponDamage);
            Destroy(gameObject);
        }

    }
}
