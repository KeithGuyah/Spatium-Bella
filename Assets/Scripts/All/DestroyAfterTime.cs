using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float _time = 10;
    public float _timer = 0;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= _time)
        {
            Destroy(this.gameObject);
        }
    }
}
