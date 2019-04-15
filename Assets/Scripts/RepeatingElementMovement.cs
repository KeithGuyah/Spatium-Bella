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
    private bool _pause = false;
    private Rigidbody2D _entityBody;
    private GameStateManager _gameStateManager;
    private PlayerControls _playerControlScript;
    private Transform _playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        _entityBody = GetComponent<Rigidbody2D>();
        _gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();

        //
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
            if(!_pause)
            {
                _entityBody.velocity = new Vector2(0,_speed);

                if(_moveWithPlayer && _playerControlScript.ControlsEnabled())
                {
                    int _playerControllerX = _playerControlScript.ReturnPlayerControllerXAxis();

                    if(Mathf.Abs(_playerControllerX) > 0)
                    {
                        float _playerX = _playerTransform.position.x;
                        float _cameraEdgeDist = 0;

                        // Determine player distance from the edge of the camera.
                        if(_playerX > 0)
                        {
                            _cameraEdgeDist = _playerX / 6.4f;
                        }
                        else if(_playerX < 0)
                        {
                            _cameraEdgeDist = _playerX / -6.4f;
                        }

                        // If we aren't at the edge of the camera.
                        if(_cameraEdgeDist < 1)
                        {
                            if(_playerControllerX > 0)
                            {
                                if(_entityBody.position.x < 1.13f)
                                {
                                    _entityBody.velocity = new Vector2(2.5f,_entityBody.velocity.y);
                                }
                            }
                            else if(_playerControllerX < 0)
                            {
                                if(_entityBody.position.x > -1.13f)
                                {
                                    _entityBody.velocity = new Vector2(-2.5f,_entityBody.velocity.y);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                StopMovement();
            }
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
        if(_doNotRepeat == false)
        {
            if(other.gameObject.CompareTag("MainCamera")  && other.gameObject.name == "CameraTriggerDown")
            {
                transform.position = new Vector2(_entityBody.position.x,_cameraTransform.position.y + _repeatOffset);
            }
        }
        
    }
}