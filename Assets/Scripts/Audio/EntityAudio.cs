using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAudio : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip _switchWeapon;
    public AudioClip _weapon1;
    public AudioClip _weapon2;
    public AudioClip _weapon3;
    public AudioClip _destroy;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySwitchAudio()
    {
        _audioSource.clip = _switchWeapon;
        _audioSource.volume = 0.5f;
        PlayAudio();
    }

    public void PlayDestroyAudio()
    {
        _audioSource.clip = _destroy;
        _audioSource.volume = 0.25f;
        PlayAudio();
    }

    public void PlayShotAudio(int type)
    {
        switch(type)
        {
            case 1:
                _audioSource.clip = _weapon1;
            break;
            case 2:
                _audioSource.clip = _weapon2;
            break;
            case 3:
                _audioSource.clip = _weapon3;
            break;
        }

        _audioSource.volume = 0.75f;
        PlayAudio();
    }

    void PlayAudio()
    {
        if(_audioSource.clip)
        {
            _audioSource.Play();
        }
    }
}