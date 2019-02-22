using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotHandler : MonoBehaviour
{
    public GameObject _projectile;
    public float _verticalOffset = -0.5f;
    public float _horizontalOffset = 0;
    public float _frequency = 0.5f;
    private float _timer = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        _timer += Time.deltaTime;
        if(_timer >= _frequency)
        {
            _timer = 0;
            Instantiate(_projectile,new Vector2(transform.position.x + _horizontalOffset,transform.position.y + _verticalOffset),transform.rotation);
        }
    }
}
