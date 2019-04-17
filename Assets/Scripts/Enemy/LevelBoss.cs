using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoss : MonoBehaviour
{
    public bool _enabled = true;
    public HealthHandler _entityHealth;
    public BoxCollider2D _boxCollider2D;
    public GameObject _explosionObject;

    // Update is called once per frame
    void Update()
    {
        if(_enabled)
        {
            if(_entityHealth._currentHP <= 0)
            {
            _enabled = false;
            _boxCollider2D.enabled = false;
            PhaseChangeExplosions(9 ,0.3f);
            Invoke("EndLevel", 2.7f);
            }
        }
    }

    void EndLevel()
    {
        _entityHealth.SetHealth(0);
    }

    void PhaseChangeExplosions(int amount, float delay)
    {
        for(int i = 0; i < amount; i++)
        {
            Invoke("SpawnExplosion", i * delay);
        }
    }

    void SpawnExplosion()
    {
        Instantiate(_explosionObject, new Vector3(
            transform.position.x + Random.Range(-0.3f,0.3f),
            transform.position.y + Random.Range(-0.3f,0.3f),
            transform.position.z),transform.rotation);
    }
}
