using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldHandler : MonoBehaviour
{
    private CircleCollider2D shieldCollider;
    private SpriteRenderer shieldRenderer;
    private PlayerControls _playerControls;
    public int _shieldMaxHP = 5;
    public Text _shieldBarText;
    public Image _shieldSlider;
    public AudioSource _mainAudio;
    public AudioSource _extraAudio;
    public AudioClip _shieldLoop;
    public AudioClip _shieldBreak;
    public AudioClip _shieldDamage;
    public bool _isPlayer;
    public int _shieldHP = 0;

    void Start()
    {
        setShieldUI();
        shieldCollider = GetComponent<CircleCollider2D>();
        shieldRenderer = GetComponent<SpriteRenderer>();
        _playerControls = GetComponentInParent<PlayerControls>();
        DisableShield();
    }

    void Update()
    {
        transform.position = transform.parent.position;
    }

    public void setShieldUI()
    {
        if (_isPlayer)
        {
            _shieldBarText.text = _shieldHP + "/" + _shieldMaxHP;
            _shieldSlider.fillAmount = (float)_shieldHP / (float)_shieldMaxHP;
        }
    }
    public void TakeDamage(int damage)
    {
        if (_shieldHP <= damage)
        {
            _shieldHP = 0;
            _mainAudio.clip = _shieldBreak;
            _mainAudio.Play();
            DisableShield();
            setShieldUI();
        }
        else
        {
            shieldRenderer.color = new Color(0,0.5f,1,1);
            Invoke("ResetShieldColor", 0.05f);
            _mainAudio.clip = _shieldDamage;
            _mainAudio.Play();
            _shieldHP = _shieldHP - damage;
            setShieldUI();
        }
    }

    void ResetShieldColor()
    {
        shieldRenderer.color = new Color(0,0.5f,1,0.3f);
    }

    public void EnableShield()
    {
        if (_shieldHP != 0)
        {
            _extraAudio.clip = _shieldLoop;
            _extraAudio.Play();
            shieldCollider.enabled = true;
            shieldRenderer.enabled = true;
            _playerControls.DisableFiring();
        }
    }

    public void DisableShield()
    {
        _extraAudio.Stop();
        shieldCollider.enabled = false;
        shieldRenderer.enabled = false;
        _playerControls.EnableFiring();
    }

    public void AddShieldHP(int addshieldHP)
    {
        if (_shieldHP==0)//Shield was disabled but because this method is called the shield health is about to be greater than zero.
        {
            EnableShield();
        }
        if ((addshieldHP+_shieldHP) > _shieldMaxHP)
        {
            _shieldHP = _shieldMaxHP;
            setShieldUI();
            Debug.Log(_shieldHP);
        }
        else
        {
             _shieldHP += addshieldHP;
            setShieldUI();
            Debug.Log(_shieldHP);
        }
    }

    public void AddMaxShieldHp(int addMaxshieldHP)
    {
        _shieldMaxHP += addMaxshieldHP;
        setShieldUI();
    }

}