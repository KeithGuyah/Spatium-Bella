using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementHandler : MonoBehaviour
{
    public GameObject _nextMoveNode = null;
    public float _speed = 5;
    private Vector2 _movementVector;
    private Rigidbody2D _entityRigidBody2D;
    private bool _moveEntity = true;

    void Start()
    {
        _entityRigidBody2D = GetComponent<Rigidbody2D>();

        if(_nextMoveNode != null)
        {
            _movementVector = CreateDestinationVector2();
        }
    }

    void Update()
    {
        if(_moveEntity)
        {
            if(_nextMoveNode != null)
            {
                _entityRigidBody2D.velocity = _movementVector.normalized * _speed;
            }
            else
            {
                StopMovement();
            }
        }
    }

    public void StopMovement()
    {
        _moveEntity = false;
        _entityRigidBody2D.velocity = new Vector2(0,0);
    }

    public void SetMoveNode(GameObject node)
    {
        _nextMoveNode = node;
    }

    private Vector2 CreateDestinationVector2()
    {
        /*
        Creates a new Vector2 by subtracting a destination Vector2 (the _nextMoveNode's position) and a origin Vector2 (the enemy's position).
        This Vector2 points towards the _nextMoveNode, so we can set the enemy's RigidBody2D velocity to this in order to move it in the direction
        of the _nextMoveNode.
        The result is unnormalized, so normalize the result Vector2 before using it or else the movement speed will be uneven.
        */
        return _nextMoveNode.transform.position - transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "EnemyMoveNode" || other.gameObject == _nextMoveNode)
        {
            EnemyMovementNode _nextNodeScript = other.gameObject.GetComponent<EnemyMovementNode>();
            _nextMoveNode = _nextNodeScript.SendNextMoveNode();
            
            if(_nextMoveNode != null) //Re-calculate destination vector only if we received another node.
            {
                _speed = _nextNodeScript.SetSpeed();
                _movementVector = CreateDestinationVector2();
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Draw a line to the attached node.
        if(_nextMoveNode != null)
        {
            Gizmos.DrawLine(transform.position,_nextMoveNode.transform.position);
        }
    }
}
