using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingElementXAxis : MonoBehaviour
{
    public float _speed = 1f;
    public Transform _cameraTransform;
    public float _repeatOffset = 10;
    private bool _pause = false;
    private Rigidbody2D _entityBody;
    
    // Start is called before the first frame update
    void Start()
    {
        _entityBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
            if(!_pause)
            {
                _entityBody.velocity = new Vector2(_speed,0);
            }
            else
            {
                StopMovement();
            }
    }

    void StopMovement()
    {
        _entityBody.velocity = new Vector2(0,0);
    }

    public void SetPause(bool value)
    {
        _pause = value;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("MainCamera")  && other.gameObject.name == "CameraTriggerRight")
        {
            transform.position = new Vector2(_entityBody.position.x - _repeatOffset,_entityBody.position.y);
        }
        
    }
}
