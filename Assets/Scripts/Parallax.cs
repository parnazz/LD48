using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private float _parallaxEffect;

    [SerializeField]
    private float _offset;

    private float _length, _startPos;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x - _offset;
    }

    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - _parallaxEffect));
        float dist = cam.transform.position.x * _parallaxEffect;

        transform.position = new Vector3(_startPos + dist + _offset, transform.position.y, transform.position.z);

        if (temp > _startPos + _length)
            _startPos += _length;
        else if (temp < _startPos - _length)
            _startPos -= _length;

    }
}
