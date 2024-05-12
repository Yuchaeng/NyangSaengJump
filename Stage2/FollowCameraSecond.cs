using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraSecond : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float yOffset;
    [SerializeField] private float xOffset;
    [SerializeField] private float tValue;

    private void LateUpdate()
    {
        //if (_target.transform.position.y >= 2f)
        //{

        //}
        //transform.position = new Vector3(_target.transform.position.x + xOffset, _target.transform.position.y + yOffset, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, new Vector3(_target.transform.position.x + xOffset, _target.transform.position.y + yOffset, transform.position.z), Time.deltaTime * tValue);
    }
}
