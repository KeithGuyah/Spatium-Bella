using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementNode : MonoBehaviour
{
    public GameObject _nextMoveNode;
    public float _speed = 5;

    public GameObject SendNextMoveNode()
    {
        return _nextMoveNode;
    }

    public float SetSpeed()
    {
        return _speed;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Node location
        Gizmos.DrawWireCube(transform.position, new Vector3(0.5f,0.5f,0));

        //Draw a line that points to the next node (only if _nextmovenode isn't empty).
        if(_nextMoveNode != null)
        {
            Gizmos.DrawLine(transform.position,_nextMoveNode.transform.position);
        }
    }
}
