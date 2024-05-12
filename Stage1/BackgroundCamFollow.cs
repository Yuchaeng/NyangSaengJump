using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCamFollow : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _target;
    [SerializeField] private float _maxHeight;
    private float lastValueY;

    private void LateUpdate()
    {
        // stage1 : 7.8f
        if (transform.position.y <= _maxHeight && lastValueY != _target.transform.position.y)
        {
            transform.position += new Vector3(0, Time.deltaTime * _speed, 0);
        }

        lastValueY = _target.transform.position.y;

    }
}
