﻿using System.Collections;
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
        _audioSource.loop = false;
        _audioSource.volume = 0.75f;
        PlayAudio();
    }

    public void PlayDestroyAudio()
    {
        Debug.Log("Playing Destroy Audio" + gameObject.name);
        _audioSource.clip = _destroy;
        _audioSource.loop = false;
        _audioSource.volume = 1f;
        PlayAudio();
    }

    public void PlayShotAudio(int type)
    {
        switch(type)
        {
            case 1:
                _audioSource.volume = 0.5f;
                _audioSource.loop = false;
                _audioSource.clip = _weapon1;
                PlayAudio();
            break;
            case 2:
                _audioSource.volume = 0.4f;
                _audioSource.loop = false;
                _audioSource.clip = _weapon2;
                PlayAudio();
            break;
            case 3:
                if(_audioSource.isPlaying == false)
                {   
                    _audioSource.volume = 0.5f;
                    _audioSource.loop = true;
                    _audioSource.clip = _weapon3;
                    PlayAudio();
                }
            break;
        }
    }

    public void StopCurrentAudioClip()
    {
        _audioSource.Stop();
    }

    void PlayAudio()
    {
        if(_audioSource.clip)
        {
            _audioSource.Play();
        }
    }
}