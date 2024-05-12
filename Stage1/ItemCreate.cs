using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemCreate : MonoBehaviour
{
    public GameObject createMap;
    public CreateMap mapScript;
    public List<Vector3> stairs = new List<Vector3>();

    public GameObject milkPrefab;
    public GameObject CatnipPrefab;
    public GameObject yarnPrefab;
    public GameObject memoryPrefab;

    //List<Vector3> pickedStair = new List<Vector3>();

    public int pickCount;

    private float randomYBottom;
    private float randomXBottom;

    private int randomYTop;
    private float randomXTop;

    //private HashSet<float> yValueSet = new HashSet<float>();

    float milkRate;
    float catnipRate;
    float memoryRate;

    private void Start()
    {
        mapScript = createMap.GetComponent<CreateMap>();
        stairs = mapScript.stairList;
        //pickedStair = mapScript.stairList;

        //MakeItems(stairs);

        //CreateItemLine();

    }

    private void Update()
    {
 
        //if (currentTime == spawnTime && idx <= createMap.stairList.Count)
        //{
        //    randomNum = Random.Range(-1, 2);
        //    Vector3 newPos = createMap.stairList[idx + randomNum];
        //    Vector3 milkPos = new Vector3(newPos.x, newPos.y + 0.8f, newPos.z);
        //    Instantiate(milkPrefab, milkPos, milkPrefab.transform.rotation);
        //    Debug.Log("���� ����");
        //}

    }

    private void CreateItem(int start, int add, GameObject itemPrefab, float offsetY)
    {
        for (int i = start; i < stairs.Count; i += add)
        {
            Vector3 itemPos = new Vector3(stairs[i].x, stairs[i].y + offsetY, stairs[i].z);
            Instantiate(itemPrefab, itemPos, itemPrefab.transform.rotation);
        }

        // 4�и� ������ - 0~50�ٻ��� : -7.5<=x<0, -6<=y<=67.5 ==> ����, 0<x<=7.5, -6<=y<=67.5 ==>������
        //               51��~100�ٻ��� : -4.5<=x<0, 69<=y<=217.5 ==> ����, 0<x<=4.5, 69<=y<=217.5 ==> ������

        // �Ʒ��� ���� 40% Ĺ�� 20% ������� 40%
        // ���� ���� 30% Ĺ�� 50% ������� 20%
        // 4�и� �ϳ� �ȿ��� 40%�� �ٸ� ������ ����. �� �ٿ��� �ִ� 1����
        // �������� �������� ������ ���ǿ��� ���� Ȯ���� ���� ���� �������� ������.

        

    }

    private void CreateItemLine() {

        // �˾ƾ��� ��
        // �Ʒ��� ���� y���� �Ʒ��� ������ y��
        // ���� ���� y���� ���� ������ y��
        // �Ʒ��� �ּ�x ���� �ִ� x��
        // ���� �ּ�x ���� �ִ� x��

        float intervalX = mapScript.intervalOfX;

        // ���� �Ʒ� ������ ��ġ
        pickCount = Mathf.FloorToInt(mapScript.middleCount * 0.4f);  //middleCount * 0.4
        //ListShuffle(mapScript.yBottomArray);

        milkRate = 0.4f;
        catnipRate = 0.2f;
        memoryRate = 0.4f;

        for (int i = 0; i < pickCount; i++)
        {
            // ������ �� == 5ĭ¥��
            if (mapScript.yBottomArray[i] % 1 == 0)
            {
                int temp = UnityEngine.Random.Range(0, 2); //0, 1
                randomXBottom = temp * intervalX + mapScript.minXOfBottomFive;  //temp * 3 -6
            }
            // �Ҽ��� �� == 6ĭ¥��
            else
            {
                int temp = UnityEngine.Random.Range(0, 3);
                randomXBottom = temp * intervalX + mapScript.minXOfBottomSix;
            }

           // pickedStair.Add(new Vector3(randomXBottom, mapScript.yBottomArray[i], 0));

            
        }

        //MakeItems();



        //������ �Ʒ� ������ ��ġ
        //pickedStair.Clear();
        //ListShuffle(mapScript.yBottomArray);

        for (int i = 0; i < pickCount; i++)
        {
            // ������ �� == 5ĭ¥��
            if (mapScript.yBottomArray[i] % 1 == 0)
            {
                int temp = UnityEngine.Random.Range(-1, 1); //-1, 0
                randomXBottom = temp * intervalX + mapScript.maxXOfBottomFive;  //temp * 3 + 6
            }
            // �Ҽ��� �� == 6ĭ¥��
            else
            {
                int temp = UnityEngine.Random.Range(-2, 1);
                randomXBottom = temp * intervalX + mapScript.maxXOfBottomSix;
            }

            //pickedStair.Add(new Vector3(randomXBottom, mapScript.yBottomArray[i], 0));
            //Debug.Log(pickedStair[i]);

        }

        //MakeItems();


        //���� �� ������ ��ġ
        pickCount = Mathf.FloorToInt(mapScript.topCount * 0.4f);
        //pickedStair.Clear();
        //ListShuffle(mapScript.yTopArray);

        milkRate = 0.3f;
        catnipRate = 0.5f;
        memoryRate = 0.2f;

        for (int i = 0; i < pickCount; i++)
        {
            // ������ �� == 3ĭ¥��
            if (mapScript.yTopArray[i] % 1 == 0)
            {
                // ���� 1���̹Ƿ� �� �����
                randomXTop = mapScript.minXOfTopThree;
            }
            // �Ҽ��� �� == 4ĭ¥��
            else
            {
                int temp = UnityEngine.Random.Range(0, 2);   //0, 1
                randomXTop = temp * intervalX + mapScript.minXOfTopFour;
            }

            //pickedStair.Add(new Vector3(randomXTop, mapScript.yTopArray[i], 0));
            //Debug.Log(pickedStair[i]);
        }


        //MakeItems();


        //������ �� ������ ��ġ
        //pickedStair.Clear();
        //ListShuffle(mapScript.yTopArray);

        for (int i = 0; i < pickCount; i++)
        {
            // ������ �� == 3ĭ¥��
            if (mapScript.yTopArray[i] % 1 == 0)
            {
                // ���� 1���̹Ƿ� ������ ����
                randomXBottom = mapScript.maxXOfTopThree;
            }
            // �Ҽ��� �� == 4ĭ¥��
            else
            {
                int temp = UnityEngine.Random.Range(-1, 1);   //-1, 0
                randomXBottom = temp * intervalX + mapScript.maxXOfTopFour;
            }

            //pickedStair.Add(new Vector3(randomXBottom, mapScript.yTopArray[i], 0));
            //Debug.Log(pickedStair[i]);

        }
       

        //MakeItems();

        //pickedStair.Clear();


        //float intervalY = mapScript.intervalOfY;

        /*
        // �������� ��(���� y��) �̱�
        while (yValueSet.Count < pickCount)
        {
            //int temp = Random.Range(mapScript.minYOfBottom, mapScript.maxYOfBottom); //-6, -5..���� ����
            int temp = Random.Range(0, mapScript.middleCount);
            randomYBottom = temp * 1.5f + mapScript.minYOfBottom;

            //Debug.Log(randomYBottom);

            //if (randomYBottom % 1 == 0)
            //{
            //    Debug.Log("-> ����");
            //}
            //else
            //{
            //    Debug.Log("->�Ҽ�");
            //}

            yValueSet.Add(randomYBottom);
        }


        //list�� hashset���� for������ �� �ߺ� ���� �����ϱ� out of memory ��
        //for (int i = 0; i < pickCount; i++)
        //{
        //    // -6 ~ 69 ���/1.5�ϰ� �� ���� *1.5
        //    // -6 ~ 69 ���� ������ (-6/1.5, 69/1.5)�ؼ� ������ �̾Ƽ� ����� *1.5
        //    int temp = Random.Range(mapScript.minYOfBottom, mapScript.maxYOfBottom); //-6, -5..���� ����
        //    randomYBottom = Mathf.Round(temp / 1.5f) * 1.5f;

        //    Debug.Log(randomYBottom);
        //    if (randomYBottom % 1 == 0)
        //    {
        //        Debug.Log("-> ����");
        //    }
        //    else
        //    {
        //        Debug.Log("->�Ҽ�");
        //    }

        //    yBottomSet.Add(randomYBottom);


        //    //yBottomList.Add(randomYBottom);

        //    //if (yBottomList.Contains(randomYBottom))
        //    //{
        //    //    yBottomList.Remove(randomYBottom);
        //    //    i--;
        //    //    continue;
        //    //}
        //}
        

        // ���� y������ x�� �ϳ��� �̱�
        // 5ĭ�ִ� ������ 6ĭ�ִ� �������� ���� x���� ���� �޶���
        // 5ĭ�� y���� ����, 6ĭ�� y���� �Ҽ�

        foreach (float yValue in yValueSet)
        {
            // ������ �� == 5ĭ¥��
            if (yValue % 1 == 0)
            {
                //int temp = Random.Range(mapScript.minXOfBottomFive / 3, 0);  //-2, -1 / �Ǽ��� �� �ȵǰų� ����ġ �޶���
                int temp = Random.Range(0, 2); //0, 1
                randomXBottom = temp * intervalX + mapScript.minXOfBottomFive;  //temp * 3 -6
            }
            // �Ҽ��� �� == 6ĭ¥��
            else
            {
                int temp = Random.Range(0, 3);  // 0, 1, 2
                randomXBottom = temp * intervalX + mapScript.minXOfBottomSix;
            }

            pickedStair.Add(new Vector3(randomXBottom, yValue, 0));

        }

        //ListShuffle(pickedStair);
        */



        //List<Vector3> pickedStair = new List<Vector3>();

        //// =========���� �Ʒ�============
        //pickCount = (int)(leftBottomStairs.Count * 0.4f);

        //for (int i = 0; i < pickCount; i++)
        //{
        //    int randomIndex = Random.Range(0, leftBottomStairs.Count);
        //    float key = leftBottomStairs.Keys.ElementAt(randomIndex);

        //    int randomX = Random.Range(0, leftBottomStairs[key].Count);
        //    Vector3 itemPos = new Vector3(leftBottomStairs[key][randomX], key, 0);
        //    pickedStair.Add(itemPos);

        //    leftBottomStairs.Remove(key);

        //}

        // pickedLine���� ������ ǥ���� �ٵ��� �������
        // �� �ٿ��� ������ ���� x��ġ ����

        //for (int i = 0; i < pickedLine.Count; i++)
        //{
        //    // key:-6, value:-4.5, -3 �߿��� 0 ���Ե��� �տ����� ��
        //    int randomX = Random.Range(0, leftBottomStairs[pickedLine[i]].Count);
        //    Vector3 itemPos = new Vector3(leftBottomStairs[pickedLine[i]][randomX], pickedLine[i], 0);
        //    pickedStair.Add(itemPos);
        //}

        //// ������ �Ʒ�
        //for (int i = 0; i < pickCount; i++)
        //{
        //    int randomIndex = Random.Range(0, leftBottomStairs.Count);
        //    float key = rightBottomStairs.Keys.ElementAt(randomIndex);

        //    int randomX = Random.Range(0, rightBottomStairs[key].Count);
        //    Vector3 itemPos = new Vector3(rightBottomStairs[key][randomX], key, 0);
        //    pickedStair.Add(itemPos);

        //    rightBottomStairs.Remove(key);

        //}

        //for (int i = 0; i < pickedLine.Count; i++)
        //{
        //    int randomX = Random.Range(0, rightBottomStairs[pickedLine[i]].Count);
        //    Vector3 itemPos = new Vector3(rightBottomStairs[pickedLine[i]][randomX], pickedLine[i], 0);
        //    pickedStair.Add(itemPos);
        //}

        // pickedStair���� ������ ǥ���� ���� ��ǥ�� ����, ������ ���� ���ļ� ������
        // ���� �� ���� 40 % Ĺ�� 20 % ������� 40 % Ȯ�� ����ϱ�
        // �Ʒ��� ���� ������ ���ļ� �� ���� = ���� ����, �� 40:20:40 Ȯ���� ���� ������ŭ �����ϰ� �������� �Ѹ���

        //for (int i = 0; i < pickedStair.Count; i++)
        //{
        //    int randomItem = Random.Range(0, pickedStair.Count);

        //    float milkRate = 0.4f;
        //    float catnipRate = 0.2f;
        //    float memoryRate = 0.4f;

        //    float milk = pickedStair.Count * milkRate;
        //    float catnip = pickedStair.Count * catnipRate;
        //    float memory = pickedStair.Count * memoryRate;

        //    ListShuffle(pickedStair);

        //    for (int j = 0; j < pickedStair.Count; j++)
        //    {
        //        // milk 4 catnip 2 memory 4 milk���� milk+catnip = 6����
        //        if (randomItem < milk)
        //        {
        //            //���� ��ġ
        //            Vector3 itemPos = new Vector3(pickedStair[j].x, pickedStair[j].y, pickedStair[j].z);
        //            Instantiate(milkPrefab, itemPos, milkPrefab.transform.rotation);
        //        }
        //        else if (randomItem >= milk && randomItem < (milk + catnip))
        //        {
        //            //Ĺ�� ��ġ
        //            Vector3 itemPos = new Vector3(pickedStair[j].x, pickedStair[j].y, pickedStair[j].z);
        //            Instantiate(CatnipPrefab, itemPos, CatnipPrefab.transform.rotation);
        //        }
        //        else if (randomItem >= (milk + catnip) && randomItem < (milk + catnip + memory))
        //        {
        //            //������� ��ġ
        //            Vector3 itemPos = new Vector3(pickedStair[j].x, pickedStair[j].y, pickedStair[j].z);
        //            Instantiate(memoryPrefab, itemPos, memoryPrefab.transform.rotation);
        //        }
        //    }

        //}
    }


    public void MakeItems(List<Vector3> pickedStair, int count)
    {
        Debug.Log("�Լ� ����");
        float milk = pickedStair.Count * milkRate;
        float catnip = pickedStair.Count * catnipRate;
        float memory = pickedStair.Count * memoryRate;

        for (int i = 0; i < count; i++)
        {
            int randomItem = UnityEngine.Random.Range(0, count);

            // milk 4 catnip 2 memory 4, milk(����)���� milk+catnip = 6����
            if (randomItem < milk)
            {
                //���� ��ġ
                Vector3 itemPos = new Vector3(pickedStair[i].x, pickedStair[i].y + 0.6f, pickedStair[i].z);
                Instantiate(milkPrefab, itemPos, milkPrefab.transform.rotation);
                Debug.Log("��������");
            }
            else if (randomItem >= milk && randomItem < (milk + catnip))
            {
                //Ĺ�� ��ġ
                Vector3 itemPos = new Vector3(pickedStair[i].x, pickedStair[i].y + 0.6f, pickedStair[i].z);
                Instantiate(CatnipPrefab, itemPos, CatnipPrefab.transform.rotation);
            }
            else if (randomItem >= (milk + catnip) && randomItem < (milk + catnip + memory))
            {
                //������� ��ġ
                Vector3 itemPos = new Vector3(pickedStair[i].x, pickedStair[i].y + 0.6f, pickedStair[i].z);
                Instantiate(memoryPrefab, itemPos, memoryPrefab.transform.rotation);
            }

            //Debug.Log(pickedStair[i]);
        }
    }

    public void ListShuffle<T>(List<T> list)
    {
        int n = 0;
        int random;
        T temp;

        while (n + 1 < list.Count)
        {
            random = UnityEngine.Random.Range(n + 1, list.Count);
            temp = list[n];
            list[n] = list[random];
            list[random] = temp;
            n++;  // �� �߰������༭ ���ѷ���
        }
       
    }





}
