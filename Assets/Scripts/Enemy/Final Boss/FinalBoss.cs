using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public bool _enabled = false;
    public int _phase = 1;
    public GameObject _explosion;

    //Timers
    public float _timer = 0;
    public float _phaseExplodeTimer = 0;

    //Timer max
    public float _timeBetweenAttacks = 3;
    public float _timeBetweenLaser = 3;
    public float _phaseTimeBetweenAttacks = 4;
    public float _shieldRespawnTimer = 0;
    public float _phaseExplodeMax = 0.417f;

    //Attack variables
    public int _attackRotation = 0;
    public int _attackRotationMax = 4;

    //Shield References
    public GameObject _bossShieldStatus;
    public GameObject _bossShieldPrefab;

    // Object Handlers
    public HealthHandler _bossHealthHandler;
    public EnemyShotHandler _bossShotHandler;
    public BossLaserController _bossLaserController;
    public SpriteRenderer _bossSpriteRenderer;
    public TrackingBulletSpawner _bossBulletSpawner;
    public AudioSource _stageAudio;
    private GameStateManager _gameStateManager;

    void Start()
    {
        _timer = _timeBetweenAttacks;
        _gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_gameStateManager.StateIsRunning())
        {
            if(_enabled)
            {
                DetectPhase();

                // Check Shield (Phase 2 only)
                if(_phase == 2)
                {
                    CheckShield();

                    if(_bossShieldStatus == null)
                    {
                        _shieldRespawnTimer += Time.deltaTime;
                    }
                }

                if(_timer >= _timeBetweenAttacks)
                {
                    Debug.Log("Activating Attack " + _attackRotation);
                    ActivateAttackRotation(_attackRotation);

                    if(_attackRotation > _attackRotationMax)
                    {
                        _attackRotation = 0;
                    }

                    _timer = 0;
                }

                _timer += Time.deltaTime;
            }
        }
    }

    void ActivateAttackRotation(int attack)
    {
        switch(attack)
        {
            case 0:
                switch(_phase)
                {
                    case 1:
                        _bossShotHandler._frequency = 2;
                        _timeBetweenAttacks = 4;
                    break;
                    case 2:
                        _bossShotHandler._frequency = 1.5f;
                        _timeBetweenAttacks = 2.5f;
                    break;
                    case 3:
                        _bossShotHandler._frequency = 1f;
                        _timeBetweenAttacks = 1.5f;
                    break;
                }
            break;
            case 1:
                _bossLaserController.StartAttack1();
                _timeBetweenAttacks = _timeBetweenLaser;
            break;
            case 2:
                _bossLaserController.StartAttack2();
                if(_phase == 1)
                {
                    _timeBetweenAttacks = _phaseTimeBetweenAttacks;
                }
                else
                {
                    _timeBetweenAttacks = _timeBetweenLaser;
                }
            break;
            case 3:
                _bossLaserController.StartAttack3();
            break;
            case 4:
                _bossLaserController.StartAttack4();
            break;
            case 5:
                _bossShotHandler._frequency = 99;
                _timeBetweenAttacks = 2.5f;
                _bossBulletSpawner.StartAttack1();
            break;
            case 6:
                _timeBetweenAttacks = 6f;
                _bossBulletSpawner.StartAttack2();
            break;
        }

        _attackRotation++;
    }
    void SpawnExplosion()
    {
        Instantiate(_explosion, new Vector3(
            transform.position.x + Random.Range(-0.3f,0.3f),
            transform.position.y + Random.Range(-0.3f,0.3f),
            transform.position.z),transform.rotation);
    }
    void PhaseChangeExplosions(int amount, float delay)
    {
        for(int i = 0; i < amount; i++)
        {
            Invoke("SpawnExplosion", i * delay);
        }
    }

    void CheckShield()
    {
        if(_shieldRespawnTimer >= 5)
        {
            SpawnShield();
            _shieldRespawnTimer = 0;
        }
    }

    void SpawnShield()
    {
        _bossShieldStatus = Instantiate(_bossShieldPrefab, new Vector3(
        transform.position.x + 8,
        transform.position.y - 1.5f,
        transform.position.z),transform.rotation,this.gameObject.transform);
        _bossShieldStatus.transform.localScale = new Vector3(0.6f,0.6f,0.6f);
    }

    void DetectPhase()
    {
        if(_bossHealthHandler._currentHP <= 200 && _bossHealthHandler._currentHP > 100 && _phase != 2)
        {
            PhaseChangeExplosions(5 ,0.3f);
            _phase = 2;
            _phaseTimeBetweenAttacks = 2.5f;
            _bossShotHandler._frequency = 1.5f;
            _attackRotationMax = 3;
            SpawnShield();
        }
        else if(_bossHealthHandler._currentHP < 100 && _bossHealthHandler._currentHP > 0 && _phase != 3)
        {
            PhaseChangeExplosions(6 ,0.3f);
            _phase = 3;
            _attackRotationMax = 6;
            _bossShotHandler._frequency = 1f;
            _phaseTimeBetweenAttacks = 1.5f;
        }
        else if (_bossHealthHandler._currentHP <= 0)
        {
            GameObject _playerRef = GameObject.Find("Player");
            _playerRef.GetComponent<LivesHandler>().SetPlayerInvincible();
            _playerRef.GetComponent<PlayerControls>().DisableControls();
            _stageAudio.volume = 0;
            _enabled = false;
            PhaseChangeExplosions(9 ,0.3f);
            Invoke("EndBossFight", 2.7f);
        }
    }

    void EndBossFight()
    {
        _bossHealthHandler.SetHealth(0);
    }
}
