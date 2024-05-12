using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class CreateMap : MonoBehaviour
{
    public GameObject StairPrefab;

    public float offsetX = 1.6f;
    public float offsetY = 0.7f;

    public const int DIR_LEFT = 0;
    public const int DIR_RIGHT = 1;

    public GameObject originStair;
    public Vector3 previousStair;

    public int stairCount = 150;

    float originX;
    float originY;
    float originZ = 0;

    public float maxX = 6f;
    public float minX = -6f;

    public GameObject leftStair;
    public GameObject rightStair;
    Vector3 leftOriginPos;
    Vector3 rightOriginPos;

    int randomDirectionLeft;
    int randomDirectionRight;

    Vector3 newLeftStair;
    Vector3 newRightStair;

    public List<Vector3> stairList = new List<Vector3>();

    private float startPosX;
    private float startPosY;
    private float startPosZ;

    public Vector3 newStair;

    public int minYOfBottom;
    public int maxYOfBottom;
    public int minXOfBottomFive;
    public int maxXOfBottomFive;
    public float minXOfBottomSix;
    public float maxXOfBottomSix;

    public int minXOfTopThree;
    public int maxXOfTopThree;
    public float minXOfTopFour;
    public float maxXOfTopFour;

    public float minYOfTop;
    public float maxYOfTop;
    public float minXOfTop;
    public float maxXOfTop;

    public float intervalOfX;
    public float intervalOfY;

    public int middleCount = 50;
    public int topCount;

    public float[] yBottomArray;
    public float[] yTopArray;

    public ItemCreate itemCreate;

    public GameObject[] itemPrefabs = new GameObject[4];

    public GameObject ratPrefab;
    private float ratStartPos;
    private Vector3 ratNewPos;

    Dictionary<float, List<float>> stairForLine = new Dictionary<float, List<float>>();

    float milkRate = 0.25f;
    float catnipRate = 0.25f;
    float yarnRate = 0.25f;
    float memoryRate = 0.25f;
    int randomItem;

    public GameObject milkPrefab;
    public GameObject CatnipPrefab;
    public GameObject yarnPrefab;
    public GameObject memoryPrefab;

    public GameObject cat;
    ProgressControl progress;

    public GameObject door;

    private void Awake()
    {
        progress = cat.GetComponent<ProgressControl>();
        topCount = stairCount - middleCount;
        yBottomArray = new float[middleCount];
        yTopArray = new float[topCount];

        CreateThreeFour();

        Instantiate(door, stairList[stairList.Count - 1] + new Vector3(0, 0.8f, 0), door.transform.rotation);
        stairList.RemoveAt(stairList.Count - 1);

        for (int i = 7; i < stairList.Count; i += 10)
        {
            Instantiate(ratPrefab, stairList[i] + new Vector3(0, 0.5f, 0), ratPrefab.transform.rotation);
            stairList.RemoveAt(i);
        }

        itemCreate.ListShuffle(stairList);

        
        int itemCount = Mathf.RoundToInt(stairList.Count * 0.5f);

        float milk = itemCount * milkRate;
        float catnip = itemCount * catnipRate;
        float yarn = itemCount * yarnRate;
        float memory = itemCount * memoryRate;

        for (int i = 0; i < stairList.Count * 0.5f; i++)
        {
            randomItem = UnityEngine.Random.Range(0, itemCount);

            if (randomItem < milk)
            {
                //우유 배치
                Vector3 itemPos = new Vector3(stairList[i].x, stairList[i].y + 0.6f, stairList[i].z);
                Instantiate(milkPrefab, itemPos, milkPrefab.transform.rotation);
            }
            else if (randomItem >= milk && randomItem < (milk + catnip))
            {
                //캣닢 배치
                Vector3 itemPos = new Vector3(stairList[i].x, stairList[i].y + 0.6f, stairList[i].z);
                Instantiate(CatnipPrefab, itemPos, CatnipPrefab.transform.rotation);
            }
            else if (randomItem >= (milk + catnip) && randomItem < (milk + catnip + yarn))
            {
                Vector3 itemPos = new Vector3(stairList[i].x, stairList[i].y + 0.5f, stairList[i].z);
                Instantiate(yarnPrefab, itemPos, yarnPrefab.transform.rotation);
            }
            else if (randomItem >= (milk + catnip + yarn) && randomItem < (milk + catnip + yarn + memory))
            {
                //기억조각 배치
                Vector3 itemPos = new Vector3(stairList[i].x, stairList[i].y + 0.6f, stairList[i].z);
                Instantiate(memoryPrefab, itemPos, memoryPrefab.transform.rotation);
            }

        }

        

         

            /*
            originY = -6;

            Instantiate(StairPrefab, new Vector3(-2.5f, originY, originZ), originStair.transform.rotation);
            Instantiate(StairPrefab, new Vector3(2.5f, originY, originZ), originStair.transform.rotation);

            originY = -4.5f;

            for (int i = 1; i < 50; i++)
            {
                if (i % 2 == 0)
                {
                    originX = -2.1f;
                }
                else
                {
                    originX = -0.7f;
                }

                emptyStair = UnityEngine.Random.Range(0, 3);
                Debug.Log(emptyStair);

                for (int j = 0; j < 4; j++)
                {               
                    if (j == emptyStair)
                    {
                        originX += 2.5f;
                        continue;
                    }
                    Instantiate(StairPrefab, new Vector3(originX, originY, originZ), originStair.transform.rotation);
                    originX += 2.5f;
                }

                originY += 1.5f;

            }
            */

            /*
            //Collider collider = originStair.GetComponentInChildren<Collider>();
            //Bounds bounds = collider.bounds;
            //decimal calSize = (decimal)bounds.size.x;

            //previousStair = originStair.transform.position;




            //for (int i = 0; i < stairCount; i++)
            //{
            //    calOriginX = (decimal)previousStair.x;
            //    calOriginY = (decimal)previousStair.y;
            //    calAddX = (decimal)offsetX;
            //    calAddY = (decimal)offsetY;


            //    // 이전 발판이 왼쪽 맨 끝에 있을 때
            //    if (previousStair.x <= minX)    //min, max를 스크린좌표를 기준으로 띄워진 곳으로 할깝쇼
            //    {
            //        direction = DIR_RIGHT;

            //    }
            //    // 이전 발판이 오른쪽 맨 끝에 있을 때
            //    else if (previousStair.x >= maxX)
            //    {
            //        direction = DIR_LEFT;

            //    }
            //    else
            //    {
            //        direction = UnityEngine.Random.Range(DIR_LEFT, DIR_RIGHT + 1);  // 0은 왼쪽 1은 오른쪽

            //    }


            //    //방향에 따라 다음 발판 위치 조정
            //    if (direction == DIR_LEFT)
            //    {
            //        resultX = (float)(calOriginX - calAddX - calSize);
            //        resultY = (float)(calOriginY + calAddY);

            //        newStair = new Vector3(resultX, resultY, 0);
            //    }
            //    else if (direction == DIR_RIGHT)
            //    {
            //        resultX = (float)(calOriginX + calAddX + calSize);
            //        resultY = (float)(calOriginY + calAddY);

            //        newStair = new Vector3(resultX, resultY, 0);
            //    }

            //    Instantiate(StairPrefab, newStair, Quaternion.Euler(0, 0, 0));

            //    previousStair = newStair;
            //}
            */
        }

    // 밑에 1/3은 5~6개 반복, 나머지는 3~4개 반복
    public void CreateStairsForLine()
    {
        startPosY = -6f;
        startPosZ = 0f;

        intervalOfX = 3f;
        intervalOfY = 1.5f;

        int randomEmpty;

        for (int i = 0; i < middleCount; i++)
        {
            // 첫째줄(i==짝수) - 5개, 둘째줄 - 6개 반복
            if (i % 2 == 0)
            {
                startPosX = -6f;
                randomEmpty = UnityEngine.Random.Range(0, 5);
                for (int j = 0; j < 5; j++)
                {
                    if (randomEmpty == j)
                    {
                        startPosX += 3f;
                        continue;
                    }
                    newStair = new Vector3(startPosX, startPosY, startPosZ);
                    Instantiate(StairPrefab, newStair, originStair.transform.rotation);
                    startPosX += 3f;

                    
                    stairList.Add(newStair);

                    if(!stairForLine.ContainsKey(newStair.y))
                    {
                        stairForLine.Add(newStair.y, new List<float> { newStair.x });
                    }
                    else
                    {
                        stairForLine[newStair.y].Add(newStair.x);
                    }

                }
            }
            else
            {
                startPosX = -7.5f;
                randomEmpty = UnityEngine.Random.Range(0, 6);

                for (int j = 0; j < 6; j++)
                {
                    if (randomEmpty == j)
                    {
                        startPosX += 3f;
                        continue;
                    }

                    newStair = new Vector3(startPosX, startPosY, startPosZ);
                    Instantiate(StairPrefab, newStair, originStair.transform.rotation);
                    startPosX += intervalOfX;

                    //if (newStair.x < 0)
                    //{

                    //    if (!leftBottom.ContainsKey(newStair.y))
                    //    {
                    //        leftBottom.Add(newStair.y, new List<float> { newStair.x });
                    //    }
                    //    else
                    //    {
                    //        leftBottom[newStair.y].Add(newStair.x);
                    //    }

                    //}
                    //else if (newStair.x > 0)
                    //{
                    //    if (!rightBottom.ContainsKey(newStair.y))
                    //    {
                    //        rightBottom.Add(newStair.y, new List<float> { newStair.x });
                    //    }
                    //    else
                    //    {
                    //        rightBottom[newStair.y].Add(newStair.x);
                    //    }
                    //}
                    stairList.Add(newStair);

                    if (!stairForLine.ContainsKey(newStair.y))
                    {
                        stairForLine.Add(newStair.y, new List<float> { newStair.x });
                    }
                    else
                    {
                        stairForLine[newStair.y].Add(newStair.x);
                    }
                }
            }
            yBottomArray[i] = startPosY;  // -6부터 67.5까지 들어갔을 것
            startPosY += intervalOfY;
        }

        minXOfBottomFive = -6;
        maxXOfBottomFive = 6;

        minXOfBottomSix = -7.5f;
        maxXOfBottomSix = 7.5f;


        for (int i = 0; i < topCount; i++)
        {
            if (i % 2 == 0)
            {
                startPosX = -3f;
                randomEmpty = UnityEngine.Random.Range(0, 3);
                for (int j = 0; j < 3; j++)
                {
                    if (randomEmpty == j)
                    {
                        startPosX += 3f;
                        continue;
                    }
                    newStair = new Vector3(startPosX, startPosY, startPosZ);
                    Instantiate(StairPrefab, newStair, originStair.transform.rotation);
                    startPosX += intervalOfX;

                    //if (newStair.x < 0)
                    //{
                    //    if (!leftTop.ContainsKey(newStair.y))
                    //    {
                    //        leftTop.Add(newStair.y, new List<float> { newStair.x });
                    //    }
                    //    else
                    //    {
                    //        leftTop[newStair.y].Add(newStair.x);
                    //    }
                    //}
                    //else if (newStair.x > 0)
                    //{
                    //    if (!rightTop.ContainsKey(newStair.y))
                    //    {
                    //        rightTop.Add(newStair.y, new List<float> { newStair.x });
                    //    }
                    //    else
                    //    {
                    //        rightTop[newStair.y].Add(newStair.x);
                    //    }
                    //}
                    stairList.Add(newStair);

                    if (!stairForLine.ContainsKey(newStair.y))
                    {
                        stairForLine.Add(newStair.y, new List<float> { newStair.x });
                    }
                    else
                    {
                        stairForLine[newStair.y].Add(newStair.x);
                    }
                }
            }
            else
            {
                startPosX = -4.5f;
                randomEmpty = UnityEngine.Random.Range(0, 4);
                for (int j = 0; j < 4; j++)
                {
                    if (randomEmpty == j)
                    {
                        startPosX += 3f;
                        continue;
                    }
                    newStair = new Vector3(startPosX, startPosY, startPosZ);
                    Instantiate(StairPrefab, newStair, originStair.transform.rotation);
                    startPosX += intervalOfX;

                    //if (newStair.x < 0)
                    //{
                    //    if (!leftTop.ContainsKey(newStair.y))
                    //    {
                    //        leftTop.Add(newStair.y, new List<float> { newStair.x });
                    //    }
                    //    else
                    //    {
                    //        leftTop[newStair.y].Add(newStair.x);
                    //    }
                    //}
                    //else if (newStair.x > 0)
                    //{
                    //    if (!rightTop.ContainsKey(newStair.y))
                    //    {
                    //        rightTop.Add(newStair.y, new List<float> { newStair.x });
                    //    }
                    //    else
                    //    {
                    //        rightTop[newStair.y].Add(newStair.x);
                    //    }
                    //}
                    stairList.Add(newStair);

                    if (!stairForLine.ContainsKey(newStair.y))
                    {
                        stairForLine.Add(newStair.y, new List<float> { newStair.x });
                    }
                    else
                    {
                        stairForLine[newStair.y].Add(newStair.x);
                    }
                }
            }
       

            yTopArray[i] = startPosY;  // 69부터 끝까지 들어갔을 것
            startPosY += intervalOfY;
        }

        minXOfTopThree = -3;
        maxXOfTopThree = 3;

        minXOfTopFour = -4.5f;
        maxXOfTopFour = 4.5f;

    }

    private void CreateThreeFour()
    {
        int randomEmpty;
        startPosY = -6f;

        for (int i = 0; i < 50; i++)
        {
            if (i % 2 == 0)
            {
                startPosX = -3f;
                randomEmpty = UnityEngine.Random.Range(0, 3);
                for (int j = 0; j < 3; j++)
                {
                    if (randomEmpty == j)
                    {
                        startPosX += 3f;
                        continue;
                    }
                    newStair = new Vector3(startPosX, startPosY, startPosZ);
                    Instantiate(StairPrefab, newStair, originStair.transform.rotation);
                    startPosX += 3;

                    stairList.Add(newStair);
                }
            }
            else
            {
                startPosX = -4.5f;
                randomEmpty = UnityEngine.Random.Range(0, 4);
                for (int j = 0; j < 4; j++)
                {
                    if (randomEmpty == j)
                    {
                        startPosX += 3f;
                        continue;
                    }
                    newStair = new Vector3(startPosX, startPosY, startPosZ);
                    Instantiate(StairPrefab, newStair, originStair.transform.rotation);
                    startPosX += 3;

                    stairList.Add(newStair);

                }
            }

            startPosY += 1.5f;
        }
        progress.MaxHeight = startPosY - 1.5f;
    }

    // 발판 랜덤으로 생성 - start from three stairs, next stairs are two or one
    public void CreateStairsRandom()
    {
        leftOriginPos = leftStair.transform.position;
        rightOriginPos = rightStair.transform.position;

        for (int i = 0; i < stairCount; i++)
        {
            // 왼쪽 발판 위치 minX인지 검사
            if (leftOriginPos.x <= minX)
            {
                randomDirectionLeft = DIR_RIGHT;
            }
            else if (leftOriginPos.x >= maxX)
            {
                randomDirectionLeft = DIR_LEFT;
            }
            else
            {
                randomDirectionLeft = UnityEngine.Random.Range(DIR_LEFT, DIR_RIGHT + 1);
            }

            // 왼쪽 발판 위에 생성하기
            if (randomDirectionLeft == DIR_LEFT)
            {
                newLeftStair = new Vector3(leftOriginPos.x - 1.5f, leftOriginPos.y + 1.5f, leftOriginPos.z);
            }

            else
            {
                newLeftStair = new Vector3(leftOriginPos.x + 1.5f, leftOriginPos.y + 1.5f, leftOriginPos.z);
            }

            // 오른쪽 발판 위치 maxX인지 검사
            if (rightOriginPos.x >= maxX)
            {
                randomDirectionRight = DIR_LEFT;

            }
            else if (rightOriginPos.x <= minX)
            {
                randomDirectionRight = DIR_RIGHT;
            }
            else
            {
                randomDirectionRight = UnityEngine.Random.Range(DIR_LEFT, DIR_RIGHT + 1);
            }

            // 오른쪽 발판 위에 생성하기
            if (randomDirectionRight == DIR_LEFT)
            {
                newRightStair = new Vector3(rightOriginPos.x - 1.5f, rightOriginPos.y + 1.5f, rightOriginPos.z);
            }
            else
            {
                newRightStair = new Vector3(rightOriginPos.x + 1.5f, rightOriginPos.y + 1.5f, rightOriginPos.z);
            }

            // 새 발판들이 겹치는지 비교
            if (newLeftStair == newRightStair)
            {
                Instantiate(StairPrefab, newLeftStair, StairPrefab.transform.rotation);

                stairList.Add(newLeftStair);
            }
            else
            {
                Instantiate(StairPrefab, newLeftStair, StairPrefab.transform.rotation);
                Instantiate(StairPrefab, newRightStair, StairPrefab.transform.rotation);

                stairList.Add(newLeftStair);
                stairList.Add(newRightStair);
            }

            leftOriginPos = newLeftStair;
            rightOriginPos = newRightStair;

        }
    }


 }
