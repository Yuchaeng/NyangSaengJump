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
        //    Debug.Log("우유 생성");
        //}

    }

    private void CreateItem(int start, int add, GameObject itemPrefab, float offsetY)
    {
        for (int i = start; i < stairs.Count; i += add)
        {
            Vector3 itemPos = new Vector3(stairs[i].x, stairs[i].y + offsetY, stairs[i].z);
            Instantiate(itemPrefab, itemPos, itemPrefab.transform.rotation);
        }

        // 4분면 나누기 - 0~50줄사이 : -7.5<=x<0, -6<=y<=67.5 ==> 왼쪽, 0<x<=7.5, -6<=y<=67.5 ==>오른쪽
        //               51줄~100줄사이 : -4.5<=x<0, 69<=y<=217.5 ==> 왼쪽, 0<x<=4.5, 69<=y<=217.5 ==> 오른쪽

        // 아래층 우유 40% 캣닢 20% 기억조각 40%
        // 위층 우유 30% 캣닢 50% 기억조각 20%
        // 4분면 하나 안에서 40%의 줄만 아이템 등장. 한 줄에는 최대 1개만
        // 아이템이 나오도록 정해진 발판에서 위의 확률에 따라 나올 아이템이 정해짐.

        

    }

    private void CreateItemLine() {

        // 알아야할 것
        // 아래층 시작 y값과 아래층 마지막 y값
        // 위층 시작 y값과 위층 마지막 y값
        // 아래층 최소x 값과 최대 x값
        // 위층 최소x 값과 최대 x값

        float intervalX = mapScript.intervalOfX;

        // 왼쪽 아래 아이템 배치
        pickCount = Mathf.FloorToInt(mapScript.middleCount * 0.4f);  //middleCount * 0.4
        //ListShuffle(mapScript.yBottomArray);

        milkRate = 0.4f;
        catnipRate = 0.2f;
        memoryRate = 0.4f;

        for (int i = 0; i < pickCount; i++)
        {
            // 정수일 때 == 5칸짜리
            if (mapScript.yBottomArray[i] % 1 == 0)
            {
                int temp = UnityEngine.Random.Range(0, 2); //0, 1
                randomXBottom = temp * intervalX + mapScript.minXOfBottomFive;  //temp * 3 -6
            }
            // 소수일 때 == 6칸짜리
            else
            {
                int temp = UnityEngine.Random.Range(0, 3);
                randomXBottom = temp * intervalX + mapScript.minXOfBottomSix;
            }

           // pickedStair.Add(new Vector3(randomXBottom, mapScript.yBottomArray[i], 0));

            
        }

        //MakeItems();



        //오른쪽 아래 아이템 배치
        //pickedStair.Clear();
        //ListShuffle(mapScript.yBottomArray);

        for (int i = 0; i < pickCount; i++)
        {
            // 정수일 때 == 5칸짜리
            if (mapScript.yBottomArray[i] % 1 == 0)
            {
                int temp = UnityEngine.Random.Range(-1, 1); //-1, 0
                randomXBottom = temp * intervalX + mapScript.maxXOfBottomFive;  //temp * 3 + 6
            }
            // 소수일 때 == 6칸짜리
            else
            {
                int temp = UnityEngine.Random.Range(-2, 1);
                randomXBottom = temp * intervalX + mapScript.maxXOfBottomSix;
            }

            //pickedStair.Add(new Vector3(randomXBottom, mapScript.yBottomArray[i], 0));
            //Debug.Log(pickedStair[i]);

        }

        //MakeItems();


        //왼쪽 위 아이템 배치
        pickCount = Mathf.FloorToInt(mapScript.topCount * 0.4f);
        //pickedStair.Clear();
        //ListShuffle(mapScript.yTopArray);

        milkRate = 0.3f;
        catnipRate = 0.5f;
        memoryRate = 0.2f;

        for (int i = 0; i < pickCount; i++)
        {
            // 정수일 때 == 3칸짜리
            if (mapScript.yTopArray[i] % 1 == 0)
            {
                // 발판 1개이므로 걍 띄워짐
                randomXTop = mapScript.minXOfTopThree;
            }
            // 소수일 때 == 4칸짜리
            else
            {
                int temp = UnityEngine.Random.Range(0, 2);   //0, 1
                randomXTop = temp * intervalX + mapScript.minXOfTopFour;
            }

            //pickedStair.Add(new Vector3(randomXTop, mapScript.yTopArray[i], 0));
            //Debug.Log(pickedStair[i]);
        }


        //MakeItems();


        //오른쪽 위 아이템 배치
        //pickedStair.Clear();
        //ListShuffle(mapScript.yTopArray);

        for (int i = 0; i < pickCount; i++)
        {
            // 정수일 때 == 3칸짜리
            if (mapScript.yTopArray[i] % 1 == 0)
            {
                // 발판 1개이므로 무조건 생성
                randomXBottom = mapScript.maxXOfTopThree;
            }
            // 소수일 때 == 4칸짜리
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
        // 랜덤으로 줄(발판 y값) 뽑기
        while (yValueSet.Count < pickCount)
        {
            //int temp = Random.Range(mapScript.minYOfBottom, mapScript.maxYOfBottom); //-6, -5..정수 나옴
            int temp = Random.Range(0, mapScript.middleCount);
            randomYBottom = temp * 1.5f + mapScript.minYOfBottom;

            //Debug.Log(randomYBottom);

            //if (randomYBottom % 1 == 0)
            //{
            //    Debug.Log("-> 정수");
            //}
            //else
            //{
            //    Debug.Log("->소수");
            //}

            yValueSet.Add(randomYBottom);
        }


        //list랑 hashset으로 for문돌린 거 중복 제거 넣으니까 out of memory 뜸
        //for (int i = 0; i < pickCount; i++)
        //{
        //    // -6 ~ 69 결과/1.5하고 몫만 떼서 *1.5
        //    // -6 ~ 69 랜덤 범위를 (-6/1.5, 69/1.5)해서 정수만 뽑아서 결과에 *1.5
        //    int temp = Random.Range(mapScript.minYOfBottom, mapScript.maxYOfBottom); //-6, -5..정수 나옴
        //    randomYBottom = Mathf.Round(temp / 1.5f) * 1.5f;

        //    Debug.Log(randomYBottom);
        //    if (randomYBottom % 1 == 0)
        //    {
        //        Debug.Log("-> 정수");
        //    }
        //    else
        //    {
        //        Debug.Log("->소수");
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
        

        // 뽑힌 y값마다 x값 하나씩 뽑기
        // 5칸있는 줄인지 6칸있는 줄인지에 따라 x값의 범위 달라짐
        // 5칸은 y값이 정수, 6칸은 y값이 소수

        foreach (float yValue in yValueSet)
        {
            // 정수일 때 == 5칸짜리
            if (yValue % 1 == 0)
            {
                //int temp = Random.Range(mapScript.minXOfBottomFive / 3, 0);  //-2, -1 / 실수일 때 안되거나 가중치 달라짐
                int temp = Random.Range(0, 2); //0, 1
                randomXBottom = temp * intervalX + mapScript.minXOfBottomFive;  //temp * 3 -6
            }
            // 소수일 때 == 6칸짜리
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

        //// =========왼쪽 아래============
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

        // pickedLine에는 아이템 표시할 줄들이 들어있음
        // 각 줄에서 아이템 넣을 x위치 고르기

        //for (int i = 0; i < pickedLine.Count; i++)
        //{
        //    // key:-6, value:-4.5, -3 중에서 0 나왔따면 앞에꺼가 픽
        //    int randomX = Random.Range(0, leftBottomStairs[pickedLine[i]].Count);
        //    Vector3 itemPos = new Vector3(leftBottomStairs[pickedLine[i]][randomX], pickedLine[i], 0);
        //    pickedStair.Add(itemPos);
        //}

        //// 오른쪽 아래
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

        // pickedStair에는 아이템 표시할 발판 좌표가 있음, 오른쪽 왼쪽 합쳐서 들어가있음
        // 발판 당 우유 40 % 캣닢 20 % 기억조각 40 % 확률 계산하기
        // 아래층 왼쪽 오른쪽 합쳐서 총 개수 = 발판 개수, 총 40:20:40 확률로 발판 개수만큼 생성하고 랜덤으로 뿌리기

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
        //        // milk 4 catnip 2 memory 4 milk부터 milk+catnip = 6까지
        //        if (randomItem < milk)
        //        {
        //            //우유 배치
        //            Vector3 itemPos = new Vector3(pickedStair[j].x, pickedStair[j].y, pickedStair[j].z);
        //            Instantiate(milkPrefab, itemPos, milkPrefab.transform.rotation);
        //        }
        //        else if (randomItem >= milk && randomItem < (milk + catnip))
        //        {
        //            //캣닢 배치
        //            Vector3 itemPos = new Vector3(pickedStair[j].x, pickedStair[j].y, pickedStair[j].z);
        //            Instantiate(CatnipPrefab, itemPos, CatnipPrefab.transform.rotation);
        //        }
        //        else if (randomItem >= (milk + catnip) && randomItem < (milk + catnip + memory))
        //        {
        //            //기억조각 배치
        //            Vector3 itemPos = new Vector3(pickedStair[j].x, pickedStair[j].y, pickedStair[j].z);
        //            Instantiate(memoryPrefab, itemPos, memoryPrefab.transform.rotation);
        //        }
        //    }

        //}
    }


    public void MakeItems(List<Vector3> pickedStair, int count)
    {
        Debug.Log("함수 실행");
        float milk = pickedStair.Count * milkRate;
        float catnip = pickedStair.Count * catnipRate;
        float memory = pickedStair.Count * memoryRate;

        for (int i = 0; i < count; i++)
        {
            int randomItem = UnityEngine.Random.Range(0, count);

            // milk 4 catnip 2 memory 4, milk(포함)부터 milk+catnip = 6까지
            if (randomItem < milk)
            {
                //우유 배치
                Vector3 itemPos = new Vector3(pickedStair[i].x, pickedStair[i].y + 0.6f, pickedStair[i].z);
                Instantiate(milkPrefab, itemPos, milkPrefab.transform.rotation);
                Debug.Log("우유나옴");
            }
            else if (randomItem >= milk && randomItem < (milk + catnip))
            {
                //캣닢 배치
                Vector3 itemPos = new Vector3(pickedStair[i].x, pickedStair[i].y + 0.6f, pickedStair[i].z);
                Instantiate(CatnipPrefab, itemPos, CatnipPrefab.transform.rotation);
            }
            else if (randomItem >= (milk + catnip) && randomItem < (milk + catnip + memory))
            {
                //기억조각 배치
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
            n++;  // 얘 추가안해줘서 무한루프
        }
       
    }





}
