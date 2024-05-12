using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    [SerializeField] private float yOffset;
    [SerializeField] private float tValue;
    [SerializeField] private float followStart;
    [SerializeField] private float speed;

    private float xOffset;

    Vector3 desiredPos;

    private void LateUpdate()
    {

        // ���� �ؿ� ���� �� x ������ ���󰡱�
        if (_target.transform.position.y <= followStart)
        {

            //if (_target.transform.position.x <= minXCamera)
            //{
            //    desiredPos.x = minXCamera;

            //}
            //else if (_target.transform.position.x >= maxXCamera)
            //{
            //    desiredPos.x = maxXCamera;
            //}
            //else
            //{

            //}

            

            desiredPos.y = transform.position.y;

        }
        // y ���� ���󰡱�
        else
        {
            //if (_target.transform.position.x <= minXCamera)
            //{
            //    desiredPos.x = minXCamera;

            //}
            //else if (_target.transform.position.x >= maxXCamera)
            //{
            //    desiredPos.x = maxXCamera;
            //}
            //else
            //{

            //}
            desiredPos.y = _target.transform.position.y + yOffset;
        }


        if (_target.transform.position.x > 4f)
        {
            desiredPos.x = _target.transform.position.x - 1.3f;
        }
       
        else if ( _target.transform.position.x < -4f)
        {
            desiredPos.x = _target.transform.position.x + 1.3f;
        }

        else
        {
            desiredPos.x = _target.transform.position.x;
        }

        //4�̻��� �Ǹ� ī�޶��� x�� 4�� �� �����ȴ� ���� !!

        desiredPos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * tValue);
        

        


        //transform.position = _target.transform.position + cameraPos;


        //transform.position = Vector3.Lerp(transform.position, _target.transform.position, Time.deltaTime * tValue);
        //transform.Translate(xValue * _speed * Time.deltaTime, yValue * _speed * Time.deltaTime, -10f);

    }
}
