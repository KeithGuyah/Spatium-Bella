using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject _enemyObject;
    public GameObject _nodeObject;
    public Transform _enemySpawnLocation;
    public Transform _nodeSpawnLocation;
    public Transform _enemyObjectContainer;
    public Transform _movementNodeContainer;
    public int _amount = 1;
    public float _spawnDelay = 35.0f;
    public float _speedOverride = 0;
    public float _fireRateOverride = 0;
    private bool _spawnStart = false;
    private float _timer = 0;

    void FixedUpdate()
    {
        if(_spawnStart && _spawnDelay > 0)
        {
            _timer += Time.deltaTime;

            if(_timer >= _spawnDelay)
            {
                SpawnEnemy();

                _timer = 0;
            }
        }
                
        if(_amount <= 0)
        {
            Destroy(gameObject);
        }
    }

    void SpawnEnemy()
    {
        _enemyObject = Instantiate(_enemyObject,_enemySpawnLocation.transform.position,_enemySpawnLocation.transform.rotation,_enemyObjectContainer);
        EnemyMovementHandler _enemyMovementHandler = _enemyObject.GetComponent<EnemyMovementHandler>();
        EnemyShotHandler _enemyShotHandler = _enemyObject.GetComponent<EnemyShotHandler>();

        //Set new speed
        if(_speedOverride > 0)
        {
            _enemyMovementHandler.SetSpeed(_speedOverride);

        }

        //Attach node if available
        if(_nodeObject != null)
        {
            // Attach the node to the enemy object.
            _enemyMovementHandler.SetMoveNode(_nodeObject);
        }

        if(_fireRateOverride > 0)
        {
            _enemyShotHandler.ChangeFrequency(_fireRateOverride);
        }

        _amount -= 1;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("MainCamera"))
        {
            _spawnStart = true;

            if(_nodeObject != null)
            {
                //Spawn node object
                _nodeObject = Instantiate(_nodeObject,_nodeSpawnLocation.transform.position,_nodeSpawnLocation.transform.rotation,_movementNodeContainer);
            }

             //Spawn first enemy
            SpawnEnemy();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        // Edge collider position
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x + 5, transform.position.y));
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x - 5, transform.position.y));

        Gizmos.color = Color.red;

        // Enemy object spawn location
        Gizmos.DrawWireCube(_enemySpawnLocation.transform.position, new Vector3(0.5f,0.5f,0.5f));

        Gizmos.color = Color.green;

        // Node object spawn location
        Gizmos.DrawWireCube(_nodeSpawnLocation.transform.position, new Vector3(0.5f,0.5f,0.5f));
    }
}
