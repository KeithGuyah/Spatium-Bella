using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public bool _isPlayer = false;
    public bool _isShield = false;
    public int _maxHP=1;
    public int _currentHP;
    // Start is called before the first frame update
    void Start()
    {
        _currentHP = _maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentHP>_maxHP)
        {
            _currentHP = _maxHP;
        }        
    }
    public void takeDamage(int damage)
    {
        _currentHP -= damage;

        if (_currentHP <= 0)
        {
            if (_isPlayer == true)
            {
                Destroy(gameObject);
                death();
            }
            else if (_isShield == true)
            {
                //need to workout mechanics for if sheild health is equal to zero.
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    void death()
    {

        //put restart level here.
        //Application.LoadLevel(Application.loadedLevel);
    }
}
