using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSecondMap : MonoBehaviour
{
    // 생성할 발판 프리팹들
    public GameObject fanPrefab;
    public GameObject smallPipePrefab;
    public GameObject midPipePrefab;
    public GameObject largePipePrefab;
    public GameObject balconyPrefab;

    // 왼쪽 벽에 붙을 실외기, 난간, 벽? -> 1/3 확률로 한개씩 배치, x는 범위 안벗어나게, 높이 간격 찾기
    // 오른쪽도 똑같
    // 가운데 부분 가로 파이프와 실외기 배치 -> 파이프:실외기=6:4비율? 왼/오 x값 고정(파이프 사이즈에 따라 다름), 높이 간격 찾기

    //파이프의 가로 최대 최소 범위
    public float maxXOfHorizontal = 9.2f;
    public float minXOfHorizontal = -9.2f;

    public float offsetX = 2.3f;  //가로 파이프
    public float offsetY = 2f; //
    public float offsetCross = 1.15f;

    public float offsetXOfVer = 1.15f;  //세로 파이프 x 값

    public int zOfHorizontal = -5;
    public int zOfVertical = -4; 

    decimal decPrevX;
    decimal decPrevY;
    decimal decOffsetX;
    decimal decOffsetY;
    float resultX;
    float resultY;

    public GameObject originHorizontal;
    public GameObject originVertical;
    public Vector3 previousHorizontal;
    public Vector3 previousVertical;

    public Vector3 newHorizontal;
    public Vector3 newLeft;
    public Vector3 newRight;

    public Vector3[] positionsHori = new Vector3[50];
    public Vector3[] positionsVer = new Vector3[100];

    int direction;
    float randomHeight;
    int whichCase;

    private void Start()
    {
        previousHorizontal = originHorizontal.transform.position;

        //가로 하나 생성될 때마다 양 옆에 세로 파이프 붙이기

        // 좌, 우, 가운데 중에 랜덤으로, 맨 왼쪽은 가운데 또는 우, 맨 오른쪽은 가운데 또는 왼
        // 0이상 3미만은 좌, 3이상 6미만은 가운데, 6이상 9미만은 우

        // 같은 높이면 붙이기, 다른 높이(크로스)면 x 2.3씩 띄워서/사이에 세로 필요, 이단점프면 x같고 세로 필요

        for (int i = 0; i < positionsHori.Length; i++)
        {
            whichCase = Random.Range(0, 3);

            decPrevX = (decimal)previousHorizontal.x;
            decPrevY = (decimal)previousHorizontal.y;
            decOffsetX = (decimal)offsetX;

            // 0이면 같은 높이
            //if (whichCase == 0)
            //{
            //    if (previousHorizontal.x == minXOfHorizontal)
            //        newHorizontal = new Vector3(previousHorizontal.x + 2.28f, previousHorizontal.y, zOfHorizontal);

            //    else if (previousHorizontal.x == maxXOfHorizontal)
            //        newHorizontal = new Vector3(previousHorizontal.x - 2.28f, previousHorizontal.y, zOfHorizontal);
            //    else
            //    {
            //        int rand = Random.Range(0, 2);

            //        if (rand == 0)
            //            newHorizontal = new Vector3(previousHorizontal.x - 2.28f, previousHorizontal.y, zOfHorizontal);
            //        else
            //            newHorizontal = new Vector3(previousHorizontal.x + 2.28f, previousHorizontal.y, zOfHorizontal);
            //    }

            //}

            //else if (whichCase == 1)
            //{

            //}

        }

        for (int i = 0; i < positionsHori.Length; i++)
        {
            decPrevX = (decimal)previousHorizontal.x;
            decPrevY = (decimal)previousHorizontal.y;
            decOffsetX = (decimal)offsetX;


            // 이전 발판이 왼쪽 맨 끝에 있을 때 - 가운데 또는 오른쪽 중에 랜덤
            if (previousHorizontal.x == minXOfHorizontal)
            {
                direction = Random.Range(3, 10);
            }
            // 이전 발판이 오른쪽 맨 끝에 있을 때 - 가운데 또는 왼쪽 중에 랜덤
            else if (previousHorizontal.x == maxXOfHorizontal)
            {
                direction = Random.Range(0, 6);
            }
            else  // 가운데, 좌, 우 중에 랜덤
            {
                direction = Random.Range(0, 10);
            }

            //왼쪽
            if (direction >= 0 && direction <= 2)
            {
                decOffsetY = (decimal)offsetCross;

                resultX = (float)(decPrevX - decOffsetX);
                resultY = (float)(decPrevY + decOffsetY);
            }
            //가운데
            else if (direction >= 3 && direction <= 5)
            {
                decOffsetY = (decimal)offsetY;

                resultX = (float)decPrevX;
                resultY = (float)(decPrevY + decOffsetY);
            }
            //오른쪽
            else if (direction >= 7 && direction <= 9)
            {
                decOffsetY = (decimal)offsetCross;

                resultX = (float)(decPrevX + decOffsetX);
                resultY = (float)(decPrevY + decOffsetY);
            }

            newHorizontal = new Vector3(resultX, resultY, zOfHorizontal);

            //randomHeight = Random.Range(resultY-0.85f, resultY + 0.85f);
            newLeft = new Vector3(resultX - offsetXOfVer, resultY, zOfVertical);
            newRight = new Vector3(resultX + offsetXOfVer, resultY, zOfVertical);

            positionsHori[i] = newHorizontal;
            positionsVer[i*2] = newLeft;
            positionsVer[i*2 + 1] = newRight;

            //Instantiate(HorizontalPrefab, newHorizontal, originHorizontal.transform.rotation);
            //Instantiate(VerticalPrefab, newLeft, VerticalPrefab.transform.rotation);
            //Instantiate(VerticalPrefab, newRight, VerticalPrefab.transform.rotation);

            previousHorizontal = newHorizontal;
            newHorizontal = Vector3.zero;

        }
    }
}
