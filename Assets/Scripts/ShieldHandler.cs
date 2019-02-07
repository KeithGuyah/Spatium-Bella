using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHandler : MonoBehaviour
{
    private CircleCollider2D shieldCollider;
    
    private SpriteRenderer shieldRenderer;

    public int _shieldMaxHP;

    public int _shieldHP;

    void Start()
    {
        _shieldMaxHP = 10;
        _shieldHP = 0;
        shieldCollider = GetComponent<CircleCollider2D>();
        shieldRenderer = GetComponent<SpriteRenderer>();
        disableShield();
    }

    void Update()
    {
        transform.position = transform.parent.position;
    }

    public void disableShield()
    {
        shieldCollider.enabled = false;
        shieldRenderer.enabled = false;
    }

    public void enableShield()
    {
        shieldCollider.enabled = true;
        shieldRenderer.enabled = true;
    }

    public void addShieldHP(int addshieldHP)
    {
        if ((addshieldHP+_shieldHP) > _shieldMaxHP)
        {
            _shieldHP = _shieldMaxHP;
            Debug.Log(_shieldHP);
        }
        else
        {
             _shieldHP += addshieldHP;
            Debug.Log(_shieldHP);
        }
    }

    public void addMaxShieldHp(int addMaxshieldHP)
    {
        _shieldMaxHP += addMaxshieldHP;
    }

}