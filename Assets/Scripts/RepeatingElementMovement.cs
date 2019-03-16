using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingElementMovement : MonoBehaviour
{
    public float _speed = 1f;
    public Transform _cameraTransform;
    public float _repeatOffset = 10;
    public bool _doNotRepeat = false;
    public bool _moveWithPlayer = false;
    private float _moveXOffset = 0;
    private float _moveXOffsetMax = 3f;
    private Rigidbody2D _entityBody;
    private GameStateManager _gameStateManager;
    private PlayerControls _playerControlScript;
    private Transform _playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        _entityBody = GetComponent<Rigidbody2D>();
        _gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();
        if(_moveWithPlayer)
        {
            _playerControlScript = GameObject.Find("Player").GetComponent<PlayerControls>();
            _playerTransform = GameObject.Find("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_gameStateManager.StateIsRunning())
        {
            _entityBody.velocity = new Vector2(0,_speed);

            if(_moveWithPlayer)
            {
                int _playerControllerX = _playerControlScript.ReturnPlayerControllerXAxis();
                float _playerX = _playerTransform.position.x;

                if(Mathf.Abs(_moveXOffset) < _moveXOffsetMax)
                    //Determine if we should move the screen left or right.
                    if(_playerControllerX > 0)
                    {
                        _moveXOffset += 0.5f;
                    }
                    else if(_playerControllerX < 0)
                    {
                        _moveXOffset -= 0.5f;
                    }

                    Debug.Log(Mathf.Abs(_moveXOffset));

                    //Determine if we should apply the left/right movement if the left/right movement key is down
                    if(Mathf.Abs(_playerControllerX) > 0 && Mathf.Abs(_playerX) <= 6.4)
                    {
                        _entityBody.velocity = new Vector2(_moveXOffset,_entityBody.velocity.y);
                    }
                    else // Player X axis value is 0 (Player is at a standstill).
                    {
                        _entityBody.velocity = new Vector2(0,_entityBody.velocity.y);
                        _moveXOffset = 0;
                    }
            }
        }
        else
        {
        _entityBody.velocity = new Vector2(0,0);
        }
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