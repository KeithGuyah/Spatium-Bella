using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeHud : MonoBehaviour
{
    public Collider2D _collider;
    public GameObject _hudElement1;
    public GameObject _hudElement2;
    public GameObject _hudElement3;
    private Text[] _textElements;
    private Image[] _imageElements;

    // Start is called before the first frame update
    void Start()
    {
        Text[] _tempTextArray;
        Image[] _tempImageArray;
        int _numOfTextElements = 0;
        int _numOfImageElements = 0;
        GameObject[] hudElements = new GameObject[]{_hudElement1, _hudElement2, _hudElement3};

        //Determine how big the text and image arrays will be.
        foreach(GameObject gameObj in hudElements)
        {
            _numOfTextElements += gameObj.GetComponentsInChildren<Text>().Length;
            _numOfImageElements += gameObj.GetComponentsInChildren<Image>().Length;
        }

        // Set array size
        _textElements = new Text[_numOfTextElements];
        _imageElements = new Image[_numOfImageElements];

        //
        foreach(GameObject gameObj in hudElements)
        {
            _tempTextArray = gameObj.GetComponentsInChildren<Text>();
            _tempImageArray = gameObj.GetComponentsInChildren<Image>();

            for(int i = 0; i < _textElements.Length; i++)
            {
                if(_textElements[i] == null)
                {
                    _tempTextArray.CopyTo(_textElements,i);
                    break;
                }
            }

            for(int i = 0; i < _imageElements.Length; i++)
            {
                if(_imageElements[i] == null)
                {
                    _tempImageArray.CopyTo(_imageElements,i);
                    break;
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            FadeElements();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
       if(other.gameObject.CompareTag("Player"))
        {
            ShowElements();
        }
    }

    void FadeElements()
    {
        foreach (Image img in _imageElements)
        {
            img.color = new Color(img.color.r,img.color.g,img.color.b,0.2f);
        }

        foreach (Text txt in _textElements)
        {
            txt.color = new Color(txt.color.r,txt.color.g,txt.color.b,0.2f);
        }
    }

    void ShowElements()
    {
        foreach (Image img in _imageElements)
        {
            img.color = new Color(img.color.r,img.color.g,img.color.b,1);
        }

        foreach (Text txt in _textElements)
        {
            txt.color = new Color(txt.color.r,txt.color.g,txt.color.b,1);
        }
    }
}
