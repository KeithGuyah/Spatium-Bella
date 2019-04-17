using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserController : MonoBehaviour
{
    public BossLaser _laser1;
    public BossLaser _laser2;
    public BossLaser _laser3;
    public BossLaser _laser4;
    public BossLaser _laser5;
    public BossLaser _laser6;
    public BossLaser _laser7;
    public BossLaser _laser8;
    public AudioSource _audioSource;
    public AudioClip _fireSound;

    void PlayFireSound()
    {
        _audioSource.clip = _fireSound;
        _audioSource.volume = 0.9f;
        Invoke("PlaySound", 2);
    }

    void PlaySound()
    {
        _audioSource.Play();
    }
    public void StartAttack1()
    {
        _laser1.EnableLaser();
        _laser3.EnableLaser();
        PlayFireSound();
    }
    public void StartAttack2()
    {
        _laser2.EnableLaser();
        _laser4.EnableLaser();
        PlayFireSound();
    }
    
    public void StartAttack3()
    {
        _laser1.EnableLaser();
        _laser2.EnableLaser();
        _laser3.EnableLaser();
        _laser4.EnableLaser();
        PlayFireSound();
    }

    public void StartAttack4()
    {
        _laser5.EnableLaser();
        _laser6.EnableLaser();
        _laser7.EnableLaser();
        _laser8.EnableLaser();
        PlayFireSound();
    }
}
