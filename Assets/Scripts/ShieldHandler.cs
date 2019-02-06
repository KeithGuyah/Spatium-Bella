using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHandler : MonoBehaviour
{
    private CircleCollider2D shieldCollider;
    private SpriteRenderer shieldRenderer;
    void Start()
    {
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
}