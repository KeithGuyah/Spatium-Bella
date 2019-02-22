using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingElementMovement : MonoBehaviour
{
    public float _speed = 1f;
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
}
