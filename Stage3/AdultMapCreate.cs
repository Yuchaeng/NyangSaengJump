using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdultMapCreate : MonoBehaviour
{
    public GameObject stairPrefab;
    private Vector3 nextPosition;
    private Vector3 prevPosition;

    private float startPosX;
    private float startPosY;
    private float startPosZ;

    private Vector3 newStair;

    void Start()
    {
        prevPosition = Vector3.zero;
        CreateStair();
    }

    private void CreateStair()
    {
        startPosY = 0;
        startPosZ = 0;

        int emptyStair;

        for (int i = 0; i < 200; i++)
        {
            if (i % 2 == 0)
            {
                startPosX = -6;

                emptyStair = UnityEngine.Random.Range(0, 5);


                for (int j = 0; j < 5; j++)
                {
                    if (j == emptyStair)
                    {
                        startPosX += 3f;
                        continue;
                    }
                    newStair = new Vector3(startPosX, startPosY, startPosZ);
                    Instantiate(stairPrefab, newStair, stairPrefab.transform.rotation);
                    startPosX += 3f;
                }
               
            }
            else
            {
                startPosX = -7.5f;
                emptyStair = UnityEngine.Random.Range(0, 6);

                for (int j = 0; j < 6; j++)
                {
                    if (j == emptyStair)
                    {
                        startPosX += 3f;
                        continue;
                    }
                    newStair = new Vector3(startPosX, startPosY, startPosZ);
                    Instantiate(stairPrefab, newStair, stairPrefab.transform.rotation);
                    startPosX += 3f;

                }
            }

            startPosY += 1;
            startPosZ += 2;
            
        }
    }

    /*public GameObject platformPrefab; // ���� ������
    public int numberOfPlatforms = 10; // ������ ������ ��
    public float height = 20f; // ������ ����
    public float radius = 5f; // ������ �ظ� ������
    public Vector3 startPosition = new Vector3(-0.7f, -7f, 2f); // ���� ��ġ
    public float verticalSpacing = 1f; // ���� ������ ���� ����
    public float horizontalSpacing = 0f; // ���� ������ ���� ����

    void GenerateSpiralPlatforms()
    {
        float angleStep = 360f / numberOfPlatforms;
        float heightStep = height / numberOfPlatforms;

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            float curHeight = i * (heightStep + verticalSpacing);
            float curRadius = radius + (i * horizontalSpacing);
            float angle = i * angleStep;

            Vector3 platformPosition = startPosition + new Vector3(curRadius * Mathf.Cos(angle * Mathf.Deg2Rad), curHeight, curRadius * Mathf.Sin(angle * Mathf.Deg2Rad));
            Instantiate(platformPrefab, platformPosition, Quaternion.identity);
        }
    }

    void GenerateSpiralPlatforms()
    {
        float heightStep = height / numPlatforms; // �� ���� ������ ���� ����

        for (int i = 0; i < numPlatforms; i++)
        {
            // �� ������ ���̿� ���� ���
            float curHeight = i * heightStep;
            float angle = Mathf.Deg2Rad * (i * 360f / numPlatforms); // 360���� numPlatforms�� ������ �� ������ ȸ�� ���� ���
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            // ������ ��ġ ���
            Vector3 platformPosition = startPosition + new Vector3(x, curHeight, z);

            // ���� ����
            Instantiate(platformPrefab, platformPosition, Quaternion.identity);
        }
    }*/

}
