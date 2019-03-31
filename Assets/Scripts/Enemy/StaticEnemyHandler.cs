using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyHandler : MonoBehaviour
{
    public GameObject _enemy;
    private EnemyMovementHandler _enemyMoveHandler;

    // Start is called before the first frame update
    void Start()
    {
        _enemy.SetActive(false);
        _enemyMoveHandler = _enemy.GetComponent<EnemyMovementHandler>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
            if(other.gameObject.CompareTag("MainCamera") && other.gameObject.name == "CameraTriggerUp")
            {
                _enemy.SetActive(true);
            }
            if(_enemyMoveHandler._nextMoveNode == null && other.gameObject.name == "CameraTriggerDown")
            {
                Destroy(_enemy);
                Destroy(this.gameObject);
            }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

}
