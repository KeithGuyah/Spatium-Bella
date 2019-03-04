using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingElementMovement : MonoBehaviour
{
    public float _speed = 1f;
    public Transform _cameraTransform;
    public float _repeatOffset = 10;
    public bool _doNotRepeat = false;
    private Rigidbody2D _entityBody;
    
    // Start is called before the first frame update
    void Start()
    {
        _entityBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _entityBody.velocity = new Vector2(0,_speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(_doNotRepeat == false)
        {
            if(other.gameObject.CompareTag("MainCamera")  && other.gameObject.name == "CameraTriggerDown")
            {
                transform.position = new Vector2(_cameraTransform.position.x,_cameraTransform.position.y + _repeatOffset);
            }
        }
    }
}