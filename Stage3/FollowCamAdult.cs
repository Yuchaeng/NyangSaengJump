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

    public Transform character; // ĳ������ Transform�� �Ҵ��մϴ�.
    public float distance = 10.0f; // ĳ���Ϳ� ī�޶� ������ �Ÿ��Դϴ�.
    public float height = 5.0f; // ĳ���� ��� ī�޶��� �����Դϴ�.
    public float rotationDamping = 10.0f; // ȸ�� ���� ���� �����Դϴ�.

    void LateUpdate()
    {
        if (!character) return;

        // ī�޶��� ���� ��ġ�� ����մϴ�.
        Vector3 targetPosition = character.position - character.forward * distance + Vector3.up * height;

        // ī�޶� �ε巴�� �ش� ��ġ�� �̵���ŵ�ϴ�.
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * rotationDamping);

        // ī�޶� ĳ���͸� �ٶ󺸵��� �մϴ�.
        Quaternion targetRotation = Quaternion.LookRotation(character.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationDamping);
    }
}
