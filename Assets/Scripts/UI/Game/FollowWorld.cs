using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWorld : MonoBehaviour
{
    private Transform _lookAt;

    [SerializeField]
    private Vector3 _offset;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    public void SetLookAt(Transform transform)
    {
        _lookAt = transform;
    }

    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(_lookAt.position + _offset);

        if (transform.position != pos)
            transform.position = pos;
    }
}
