using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserController : MonoBehaviour
{
    public BossLaser _laser1;
    public BossLaser _laser2;
    public BossLaser _laser3;
    public BossLaser _laser4;

    public void StartAttack1()
    {
        _laser1.EnableLaser();
        _laser3.EnableLaser();
    }
    public void StartAttack2()
    {
        _laser2.EnableLaser();
        _laser4.EnableLaser();
    }
    
    public void StartAttack3()
    {
        _laser1.EnableLaser();
        _laser2.EnableLaser();
        _laser3.EnableLaser();
        _laser4.EnableLaser();
    }
}
