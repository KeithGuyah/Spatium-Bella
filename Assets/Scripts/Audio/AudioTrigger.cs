using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public enum Type {setVolume, fadeOut, previousVol, fadeToVolumeDown, fadeToVolumeUp};
    public BoxCollider2D _collider2D;
    public StageAudio _stageAudio;
    public Type _triggerType = Type.setVolume;
    public float _volume = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("MainCamera") && other.gameObject.name == "CameraTriggerUp")
        {
            switch(_triggerType)
            {
                case Type.setVolume:
                    _stageAudio.SetVolume(_volume);
                break;
                case Type.fadeOut:
                    _stageAudio.FadeOutAudio();
                break;
                case Type.previousVol:
                    _stageAudio.SetVolumeToPrevious();
                break;
                case Type.fadeToVolumeDown:
                    _stageAudio.FadeVolumeTo(_volume, true);
                break;
                case Type.fadeToVolumeUp:
                    _stageAudio.FadeVolumeTo(_volume, false);
                break;
            }

            Destroy(this.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawCube(transform.position,_collider2D.size);
    }

}
