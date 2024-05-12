using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowCamAdult : MonoBehaviour
{
    //public GameObject target;
    //[SerializeField] private float xOffset;
    //[SerializeField] private float yOffset;
    //[SerializeField] private float zOffset;
    //[SerializeField] private float tValue;

    //private void LateUpdate()
    //{
    //    Vector3 desiredPos = new Vector3(target.transform.position.x + xOffset, target.transform.position.y + yOffset, target.transform.position.z + zOffset);
    //    //transform.LookAt(desiredPos);
    //    transform.rotation = Quaternion.LookRotation(desiredPos);
    //    transform.position = Vector3.Lerp(transform.position, desiredPos, tValue);
    //}

    public Transform character; // 캐릭터의 Transform을 할당합니다.
    public float distance = 10.0f; // 캐릭터와 카메라 사이의 거리입니다.
    public float height = 5.0f; // 캐릭터 대비 카메라의 높이입니다.
    public float rotationDamping = 10.0f; // 회전 시의 감쇠 비율입니다.

    void LateUpdate()
    {
        if (!character) return;

        // 카메라의 예상 위치를 계산합니다.
        Vector3 targetPosition = character.position - character.forward * distance + Vector3.up * height;

        // 카메라를 부드럽게 해당 위치로 이동시킵니다.
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * rotationDamping);

        // 카메라가 캐릭터를 바라보도록 합니다.
        Quaternion targetRotation = Quaternion.LookRotation(character.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationDamping);
    }
}
