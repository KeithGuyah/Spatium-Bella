using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentDamage : MonoBehaviour
{
    private bool _cameraFlag = false;
    private bool _environmentFlag = false;
    private HealthHandler _playerHealthHandler;
    private RaycastHit2D _raycastInfo;

    // Start is called before the first frame update
    void Start()
    {
        _playerHealthHandler = GetComponent<HealthHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_cameraFlag && _environmentFlag)
        {
            _playerHealthHandler.SetHealth(0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "CameraTriggerDown")
        {
            _cameraFlag = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(_cameraFlag)
        {
            _raycastInfo = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y + 0.31f), transform.up);
            //Debug.Log(_raycastInfo.transform.tag);
            if(_raycastInfo.transform.CompareTag("Indestructable") || _raycastInfo.transform.CompareTag("Destructible"))
            {
                //Debug.Log(_raycastInfo.transform.tag + ": " + _raycastInfo.distance);
                if(_raycastInfo.distance <= 0.1f)
                {
                    _playerHealthHandler.SetHealth(0);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name == "CameraTriggerDown")
        {
            _cameraFlag = false;
        }
    }
}