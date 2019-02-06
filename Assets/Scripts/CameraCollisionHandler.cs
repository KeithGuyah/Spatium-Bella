using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionHandler : MonoBehaviour
{
    private EdgeCollider2D _cameraEdgeCollider;
    // Start is called before the first frame update
    void Start()
    {
        _cameraEdgeCollider = GetComponent<EdgeCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag != "Player") // If the camera edge collider collides with anything that isn't the player.
        {
            //Debug.Log("Other Collision!");
            Physics2D.IgnoreCollision(_cameraEdgeCollider, other.collider, true); // ...then ignore that collision.
        }
    }
}
