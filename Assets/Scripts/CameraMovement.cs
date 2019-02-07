using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float _speed = 0.5f;
    private Rigidbody2D _cameraBody;
    
    // Start is called before the first frame update
    void Start()
    {
        _cameraBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _cameraBody.velocity = new Vector2(0,_speed);
    }
}
