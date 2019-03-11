using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotHandler : MonoBehaviour
{
    public GameObject _projectile;
    public float _verticalOffset = 0;
    public float _horizontalOffset = 0;
    public float _frequency = 0;
    private float _timer = 0;
    void Start()
    {
        _timer = _frequency;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= _frequency)
        {
            _timer = 0;
            Instantiate(_projectile, new Vector2(transform.position.x + _horizontalOffset,transform.position.y + _verticalOffset),transform.rotation);
        }
    }
}
