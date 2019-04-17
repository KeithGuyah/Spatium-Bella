using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBulletSpawner : MonoBehaviour
{
    public Transform _position1;
    public Transform _position2;
    public Transform _position3;
    public Transform _position4;
    public Transform[] _positions;
    public GameObject[] _bullets;
    public GameObject _bulletPrefab;
    public float _delayAmount = 0.5f;
    public int _arrayPos = 0;
    public float _timer = 0;
    public bool _startDelaySpawn = false;
    private GameStateManager _gameStateManager;


    // Start is called before the first frame update
    void Start()
    {
        _positions = new Transform[]{_position1, _position2, _position3, _position4};
        _bullets = new GameObject[_positions.Length];
        _gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();

    }

    void Update()
    {
        if(_gameStateManager.StateIsRunning())
        {
            if(_startDelaySpawn)
            {
                _timer += Time.deltaTime;

                if(_timer >= _delayAmount)
                {
                    Instantiate(_bulletPrefab, _positions[_arrayPos]);
                    _timer = 0;
                    _arrayPos++;
                }

                if(_arrayPos > _positions.GetUpperBound(0))
                {
                    _startDelaySpawn = false;
                    _arrayPos = 0;
                }
            }
        }
    }

    public void StartAttack1()
    {
        Instantiate(_bulletPrefab, _position1);
        Instantiate(_bulletPrefab, _position4);
    }

    public void StartAttack2()
    {
        _startDelaySpawn = true;
    }

}
