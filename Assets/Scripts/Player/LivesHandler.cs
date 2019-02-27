using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesHandler : MonoBehaviour
{
    public int _lives = 3;
    private bool _isDead = false;
    private float _respawnTimer = 0;
    private float _respawnTimerMax = 2;
    private Vector2 _playerRespawnPoint;
    private PlayerControls _playerControls;
    private SpriteRenderer _playerSpriteRenderer;
    private HealthHandler _playerHealthHandler;

    // Start is called before the first frame update
    void Start()
    {
        _playerControls = GetComponent<PlayerControls>();
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        _playerHealthHandler = GetComponent<HealthHandler>();
        _playerRespawnPoint = new Vector2(0, -4);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player ran out of lives.
        if(_lives <= 0)
        {
            
        }

        // Check if we need to respawn the player.
        if(_isDead && _lives > 0)
        {
            _respawnTimer += Time.deltaTime;
            if(_respawnTimer >= _respawnTimerMax)
            {
                _respawnTimer = 0;
                PlayerRespawn();
            }
        }
    }

    public void PlayerDeathStart()
    {
        _playerControls.DisableControls();
        _isDead = true;
    }

    public void PlayerDeathEnd()
    {
        _playerSpriteRenderer.enabled = false;
        _lives -= 1;
        Debug.Log("Player lives: " + _lives);
    }

    public void PlayerRespawn()
    {
        _isDead = false;
        transform.position = _playerRespawnPoint;
        _playerHealthHandler.SetHealth(_playerHealthHandler._maxHP);
        _playerControls.EnableControls();
        _playerSpriteRenderer.enabled = true;
    }
}
