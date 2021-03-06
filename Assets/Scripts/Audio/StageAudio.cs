﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageAudio : MonoBehaviour
{
    public AudioSource _audioSource;
    public AudioClip _stageMusic;

    //Boss Music
    public AudioClip _bossTheme;
    public AudioClip _bossTheme2;
    public bool _playBossMusic = false;

    //Fade music to volume 0
    public bool _fadeCurrentMusic = false;

    //Fade music to specified volume
    public bool _fadeToVolume = false;
    public float _volumeAtFadeStart = 0;
    public float _fadeToVolumeLevel = 0;
    public bool _fadeDown = true;

    //Fade Over Time
    public bool _fadeVolumeOverTime = false;
    public float _fadeTime = 2;
    public float _fadeTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(_stageMusic != null)
        {
            _audioSource.clip = _stageMusic;
            _audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_fadeToVolume)
        {
            if(_fadeDown)
            {
                _audioSource.volume -=  Time.deltaTime;

                if(_audioSource.volume <= _fadeToVolumeLevel)
                {
                    _fadeToVolume = false;
                }
            }
            else
            {
                _audioSource.volume +=  Time.deltaTime;

                if(_audioSource.volume >= _fadeToVolumeLevel)
                {
                    _fadeToVolume = false;
                }
            }
        }
        else if(_fadeCurrentMusic)
        {
            _audioSource.volume -= Time.deltaTime * 0.5f;

            if(_audioSource.volume <= 0)
            {
                _audioSource.volume = 0;
                _fadeCurrentMusic = false;
            }
        }
        else if(_playBossMusic == true)
        {
            if(_audioSource.isPlaying == false && _bossTheme2 != null)
            {
                _audioSource.clip = _bossTheme2;
                _audioSource.Play();
                _audioSource.loop = true;
                _playBossMusic = false;
            }
            else if(_audioSource.isPlaying == true && _bossTheme2 == null)
            {
                _playBossMusic = false;
            }
        }
        else if(_fadeVolumeOverTime == true)
        {
            _fadeTimer += Time.deltaTime;
            _audioSource.volume = _volumeAtFadeStart - (_volumeAtFadeStart * (_fadeTimer / _fadeTime));

            if(_fadeTimer >= _fadeTime)
            {
                _audioSource.volume = 0;
                _fadeTimer = 0;
                _fadeVolumeOverTime = false;
            }
        }
    }

    public void UndoFadeVolumeOnTimer()
    {
        _fadeTimer = 0;
        _fadeVolumeOverTime = false;
        SetVolumeToPrevious();
    }

    public void FadeVolumeOnTimer(float time)
    {
        _volumeAtFadeStart = _audioSource.volume;
        _fadeTime = time;
        _fadeVolumeOverTime = true;
    }

    public float ReturnVolumeLevel()
    {
        return _audioSource.volume;
    }
    public void PlayBossTheme()
    {
        _audioSource.clip = _bossTheme;
        SetVolume(1);
        _audioSource.loop = false;
        _audioSource.Play();
        _playBossMusic = true;
    }

    public void EnableLoop()
    {
        _audioSource.loop = true;
    }

    public void SetVolume(float volume)
    {
        _volumeAtFadeStart = _audioSource.volume;
        _audioSource.volume = volume;
    }

    public void SetVolumeToPrevious()
    {
        _audioSource.volume = _volumeAtFadeStart;
    }

    public void FadeOutAudio()
    {
        _fadeCurrentMusic = true;
        _volumeAtFadeStart = _audioSource.volume;
    }

    public void FadeVolumeTo(float volume, bool fadeDown)
    {
        _fadeToVolume = true;
        _fadeDown = fadeDown;
        _fadeToVolumeLevel = volume;
        _volumeAtFadeStart = _audioSource.volume;
    }
}
