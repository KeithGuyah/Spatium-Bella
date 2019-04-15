using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{
    public LineRenderer _lineRenderer;
    public bool _enabled = false;
    public bool _startup = true;
    public float _timer = 0;
    public float _startUpMax = 5;
    public float _laserActive = 1;
    public Transform _laserLeft;
    public Transform _laserRight;
    private RaycastHit2D _raycast;
    public Transform _laserFill;
    public GameObject _laserBG;
    private float _laserFillMax = 4.5f;

    // Update is called once per frame
    void Update()
    {
        if(_enabled)
        {
            if(_startup)
            {
                float _percent = _timer / _startUpMax;

                _laserFill.localScale = new Vector3(
                    _laserFillMax * _percent,
                    _laserFill.localScale.y,
                    _laserFill.localScale.z);

                if(_timer >= _startUpMax)
                {
                    StartupComplete();
                    _timer = 0;
                }
            }
            else
            {
                RecalculateLaser();

                if(_timer >= _laserActive)
                {
                    _timer = 0;
                    DisableLaser();
                }
            }
            _timer += Time.deltaTime;
        }
    }

    void RecalculateLaser()
    {
        _lineRenderer.SetPosition(0, new Vector3(
            _laserLeft.position.x + 0.3f,
            _laserLeft.position.y,
            _laserLeft.position.z));

        _raycast = Physics2D.Raycast(new Vector2(
            _laserLeft.position.x + 0.3f,
            _laserLeft.position.y)
        , _laserLeft.transform.right);

        //Debug.Log(_raycast.transform.position.x + " " + _raycast.transform.position.y);
        if(_raycast.transform.CompareTag("Player"))
        {
            _lineRenderer.SetPosition(1, _raycast.point);
            _raycast.transform.gameObject.GetComponent<HealthHandler>().TakeDamage(1);
        }
        else if(_raycast.transform.CompareTag("PlayerShield"))
        {
            _lineRenderer.SetPosition(1, _raycast.point);
            _raycast.transform.gameObject.GetComponent<ShieldHandler>().TakeDamage(1);
        }
        else
        {
            _lineRenderer.SetPosition(1, new Vector3(
            _laserRight.position.x - 0.3f,
            _laserRight.position.y,
            _laserRight.position.z
            ));
        }
    }

    void StartupComplete()
    {
        DisableLaserBG();
        DisableLaserFill();
        _startup = false;
    }

    void EnableLaserBG()
    {
        _laserBG.SetActive(true);
    }

    void DisableLaserBG()
    {
        _laserBG.SetActive(false);
    }

    void DisableLaserFill()
    {
        _laserFill.localScale = new Vector3(
            0,
            _laserFill.localScale.y,
            _laserFill.localScale.z);
    }

    void EnableLaserFill()
    {
        _laserFill.localScale = new Vector3(
            _laserFillMax,
            _laserFill.localScale.y,
            _laserFill.localScale.z);
    }
    public void EnableLaser()
    {
        EnableLaserBG();
        EnableLaserFill();
        _enabled = true;
        _startup = true;
    }

    void DisableLaser()
    {
        _enabled = false;
        _lineRenderer.SetPosition(0,_laserLeft.position);
        _lineRenderer.SetPosition(1,_laserLeft.position);
    }
}
