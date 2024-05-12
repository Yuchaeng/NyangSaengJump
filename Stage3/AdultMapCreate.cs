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

    /*public GameObject platformPrefab; // 발판 프리팹
    public int numberOfPlatforms = 10; // 생성할 발판의 수
    public float height = 20f; // 원뿔의 높이
    public float radius = 5f; // 원뿔의 밑면 반지름
    public Vector3 startPosition = new Vector3(-0.7f, -7f, 2f); // 시작 위치
    public float verticalSpacing = 1f; // 발판 사이의 수직 간격
    public float horizontalSpacing = 0f; // 발판 사이의 수평 간격

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
        float heightStep = height / numPlatforms; // 각 발판 사이의 높이 차이

        for (int i = 0; i < numPlatforms; i++)
        {
            // 각 발판의 높이와 각도 계산
            float curHeight = i * heightStep;
            float angle = Mathf.Deg2Rad * (i * 360f / numPlatforms); // 360도를 numPlatforms로 나누어 각 발판의 회전 각도 계산
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            // 발판의 위치 계산
            Vector3 platformPosition = startPosition + new Vector3(x, curHeight, z);

            // 발판 생성
            Instantiate(platformPrefab, platformPosition, Quaternion.identity);
        }
    }*/

}
