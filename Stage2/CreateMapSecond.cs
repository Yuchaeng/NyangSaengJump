using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class CreateMapSecond : MonoBehaviour
{
    public GameObject StairPrefab;

    public float offsetX = 1.6f;
    public float offsetY = 0.7f;

    public const int DIR_LEFT = 0;
    public const int DIR_RIGHT = 1;

    public GameObject originStair;
    public Vector3 previousStair;
    public Vector3 newStair;

    [SerializeField] int stairCount = 150;

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

    private float startPosY;
    private float startPosX;
    private float startPosZ;

    public GameObject door;

    public GameObject milkPrefab;
    public GameObject CatnipPrefab;
    public GameObject yarnPrefab;
    public GameObject memoryPrefab;

    float milkRate = 0.3f;
    float catnipRate = 0.3f;
    float yarnRate = 0.2f;
    float memoryRate = 0.2f;
    int randomItem;

    ProgressBaby progress;

    public GameObject cat;

    void Start()
    {
        progress = cat.GetComponent<ProgressBaby>();

        CreateFiveSix();

        Instantiate(door, stairList[stairList.Count - 1] + new Vector3(0, 0.8f, 0), door.transform.rotation);
        stairList.RemoveAt(stairList.Count - 1);


        ListShuffle(stairList);

        int itemCount = Mathf.RoundToInt(stairList.Count * 0.5f);
        Debug.Log($"{itemCount} 아이템 들어갈 개수");

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

        /*leftOriginPos = leftStair.transform.position;
        rightOriginPos = rightStair.transform.position;

        for (int i = 0; i < stairCount; i++)
        {
            // 왼쪽 발판 위치 minX인지 검사
            if (leftOriginPos.x <= minX)
            {
                randomDirectionLeft = DIR_RIGHT;
                //Debug.Log("제일 왼쪽 - " + i);
            }
            else if (leftOriginPos.x >= maxX)
            {
                randomDirectionLeft = DIR_LEFT;
            }
            else
            {
                randomDirectionLeft = Random.Range(DIR_LEFT, DIR_RIGHT + 1);
            }

            // 왼쪽 발판 위에 생성하기
            if (randomDirectionLeft == DIR_LEFT)
            {
                newLeftStair = new Vector3(leftOriginPos.x - 1.5f, leftOriginPos.y + 1.5f, leftOriginPos.z);
                Instantiate(StairPrefab, newLeftStair, StairPrefab.transform.rotation);
            }

            else
            {
                newLeftStair = new Vector3(leftOriginPos.x + 1.5f, leftOriginPos.y + 1.5f, leftOriginPos.z);
                Instantiate(StairPrefab, newLeftStair, StairPrefab.transform.rotation);
            }

            // 오른쪽 발판 위치 maxX인지 검사
            if (rightOriginPos.x >= maxX)
            {
                randomDirectionRight = DIR_LEFT;
                //Debug.Log("제일 오른쪽 - " + i);

            }
            else if (rightOriginPos.x <= minX)
            {
                randomDirectionRight = DIR_RIGHT;
            }
            else
            {
                randomDirectionRight = Random.Range(DIR_LEFT, DIR_RIGHT + 1);
            }

            // 오른쪽 발판 위에 생성하기
            if (randomDirectionRight == DIR_LEFT)
            {
                newRightStair = new Vector3(rightOriginPos.x - 1.5f, rightOriginPos.y + 1.5f, rightOriginPos.z);
                Instantiate(StairPrefab, newRightStair, StairPrefab.transform.rotation);
            }
            else
            {
                newRightStair = new Vector3(rightOriginPos.x + 1.5f, rightOriginPos.y + 1.5f, rightOriginPos.z);
                Instantiate(StairPrefab, newRightStair, StairPrefab.transform.rotation);
            }

            leftOriginPos = newLeftStair;
            rightOriginPos = newRightStair;

            stairList.Add(newLeftStair);
            stairList.Add(newRightStair);
        }*/
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

    private void CreateFiveSix()
    {
        int randomEmpty;
        startPosY = -5f;
        startPosZ = 0;

        for (int i = 0; i < 100; i++)
        {
            if (i % 2 == 0)
            {
                startPosX = -8f;
                randomEmpty = UnityEngine.Random.Range(0, 5);
                for (int j = 0; j < 5; j++)
                {
                    if (randomEmpty == j)
                    {
                        startPosX += 4f;
                        continue;
                    }
                    newStair = new Vector3(startPosX, startPosY, startPosZ);
                    Instantiate(StairPrefab, newStair, originStair.transform.rotation);
                    startPosX += 4;

                    stairList.Add(newStair);
                }
            }
            else
            {
                startPosX = -10f;
                randomEmpty = UnityEngine.Random.Range(0, 6);
                for (int j = 0; j < 6; j++)
                {
                    if (randomEmpty == j)
                    {
                        startPosX += 4f;
                        continue;
                    }
                    newStair = new Vector3(startPosX, startPosY, startPosZ);
                    Instantiate(StairPrefab, newStair, originStair.transform.rotation);
                    startPosX += 4;

                    stairList.Add(newStair);

                }
            }

            startPosY += 2.5f;
        }
        progress._maxHeight = startPosY - 2.5f;

    }


}
