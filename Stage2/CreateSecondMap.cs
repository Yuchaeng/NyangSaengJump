using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSecondMap : MonoBehaviour
{
    // ������ ���� �����յ�
    public GameObject fanPrefab;
    public GameObject smallPipePrefab;
    public GameObject midPipePrefab;
    public GameObject largePipePrefab;
    public GameObject balconyPrefab;

    // ���� ���� ���� �ǿܱ�, ����, ��? -> 1/3 Ȯ���� �Ѱ��� ��ġ, x�� ���� �ȹ����, ���� ���� ã��
    // �����ʵ� �Ȱ�
    // ��� �κ� ���� �������� �ǿܱ� ��ġ -> ������:�ǿܱ�=6:4����? ��/�� x�� ����(������ ����� ���� �ٸ�), ���� ���� ã��

    //�������� ���� �ִ� �ּ� ����
    public float maxXOfHorizontal = 9.2f;
    public float minXOfHorizontal = -9.2f;

    public float offsetX = 2.3f;  //���� ������
    public float offsetY = 2f; //
    public float offsetCross = 1.15f;

    public float offsetXOfVer = 1.15f;  //���� ������ x ��

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

        //���� �ϳ� ������ ������ �� ���� ���� ������ ���̱�

        // ��, ��, ��� �߿� ��������, �� ������ ��� �Ǵ� ��, �� �������� ��� �Ǵ� ��
        // 0�̻� 3�̸��� ��, 3�̻� 6�̸��� ���, 6�̻� 9�̸��� ��

        // ���� ���̸� ���̱�, �ٸ� ����(ũ�ν�)�� x 2.3�� �����/���̿� ���� �ʿ�, �̴������� x���� ���� �ʿ�

        for (int i = 0; i < positionsHori.Length; i++)
        {
            whichCase = Random.Range(0, 3);

            decPrevX = (decimal)previousHorizontal.x;
            decPrevY = (decimal)previousHorizontal.y;
            decOffsetX = (decimal)offsetX;

            // 0�̸� ���� ����
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


            // ���� ������ ���� �� ���� ���� �� - ��� �Ǵ� ������ �߿� ����
            if (previousHorizontal.x == minXOfHorizontal)
            {
                direction = Random.Range(3, 10);
            }
            // ���� ������ ������ �� ���� ���� �� - ��� �Ǵ� ���� �߿� ����
            else if (previousHorizontal.x == maxXOfHorizontal)
            {
                direction = Random.Range(0, 6);
            }
            else  // ���, ��, �� �߿� ����
            {
                direction = Random.Range(0, 10);
            }

            //����
            if (direction >= 0 && direction <= 2)
            {
                decOffsetY = (decimal)offsetCross;

                resultX = (float)(decPrevX - decOffsetX);
                resultY = (float)(decPrevY + decOffsetY);
            }
            //���
            else if (direction >= 3 && direction <= 5)
            {
                decOffsetY = (decimal)offsetY;

                resultX = (float)decPrevX;
                resultY = (float)(decPrevY + decOffsetY);
            }
            //������
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
