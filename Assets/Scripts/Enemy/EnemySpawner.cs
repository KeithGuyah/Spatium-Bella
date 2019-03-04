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

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("MainCamera"))
        {
            //Spawn enemy object
            _enemyObject = Instantiate(_enemyObject,_enemySpawnLocation.transform.position,_enemySpawnLocation.transform.rotation,_enemyObjectContainer);
            
            if(_nodeObject != null)
            {
                //Spawn node object
                _nodeObject = Instantiate(_nodeObject,_nodeSpawnLocation.transform.position,_nodeSpawnLocation.transform.rotation,_enemyObjectContainer);
                
                // Attach the node to the enemy object.
                _enemyObject.GetComponent<EnemyMovementHandler>().SetMoveNode(_nodeObject);
            }

            // Delete this object after everything has been set.
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        // Edge collider position
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x + 5, transform.position.y));
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x - 5, transform.position.y));

        // Enemy object spawn location
        Gizmos.DrawWireCube(_enemySpawnLocation.transform.position, new Vector3(0.5f,0.5f,0.5f));

        // Node object spawn location
        Gizmos.DrawWireCube(_nodeSpawnLocation.transform.position, new Vector3(0.5f,0.5f,0.5f));
    }
    
}
