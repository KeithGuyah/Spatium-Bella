using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHandler : MonoBehaviour
{
    public int _powerUpValue = 1;
    public bool _isSheild_PU = false;
    public bool _isWeapons_PU = false;
    public bool _isShieldMaxHP_PU = false;
    private CircleCollider2D _powerUpCollider;
    // Start is called before the first frame update
    void Start()
    {
        _powerUpCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D objectHit)
    {
        if (objectHit.gameObject.tag == "Player")
        {
            Debug.Log(objectHit.gameObject.tag);
            if (_isSheild_PU == true)
            {
                Debug.Log("Collison detected");
                powerUpShield();
            }
            if (_isShieldMaxHP_PU == true)
            {
                Debug.Log("Collison detected");
                powerUpShieldMaxHP();
            }
            Destroy(gameObject);
        }
        else 
        {
            Debug.Log("Other Collision");
            Physics2D.IgnoreCollision(_powerUpCollider, objectHit.collider, true); 
        }

    }
    void powerUpShield()
    {
     
        GameObject.Find("PlayerShield").GetComponent<ShieldHandler>().addShieldHP(_powerUpValue);
        
    }
    void powerUpWeapons()
    {

    }
    void powerUpShieldMaxHP()
    {
        GameObject.Find("PlayerShield").GetComponent<ShieldHandler>().addMaxShieldHp(_powerUpValue);
    }

}
