using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class MonsterCatMove : MonoBehaviour
{
    // 시작점 지정, pc보다 2칸 아래
    // 1초에 한 번 점프
    // pc의 위치 알아야 함

    public GameObject enemyCatPrefab;
    [SerializeField] private GameObject enemyCat;
    public GameObject targetCat;
    private ChildCatMovement childMove;
    private Animator _myAnimator;
    private Rigidbody _myRigidbody;

    [SerializeField] private bool isFirstSpawn;
    public bool isSpawn;

    private float moveCurrentTime = 0;
    private float delayTime = 1;
    private int i = 1;
    private int monInitIndex;
    private int catInitIndex;
    private WaitForSeconds waitInitSec;

    public bool followPaused;

    private bool isCorouting;
    private bool isWaitCorouting;

    private int tempIndex;
    private int tempAddI;

    private WaitForSecondsRealtime waitRealtime;

    private void Start()
    {
        enemyCat = Instantiate(enemyCatPrefab);

        enemyCat.SetActive(false);
        
        childMove = targetCat.GetComponent<ChildCatMovement>();

        catInitIndex = 5;

        waitInitSec = new WaitForSeconds(0.5f);
        waitRealtime = new WaitForSecondsRealtime(1f);

        StartCoroutine(FirstSpawn());
    }

    private void Update()
    {
        
        if (!isSpawn && childMove.steppedStair.Count % 40 == 0 && childMove.steppedStair.Count > 1 && !isCorouting)
        {
            Debug.Log("조건맞아서 스폰");
            i = 1;
            tempAddI = 0;
            monInitIndex = childMove.steppedStair.Count - 3;
            isSpawn = true;
            enemyCat.transform.position = childMove.steppedStair[monInitIndex] + new Vector3(0, 0.2f, 0);
            enemyCat.SetActive(true);

            StartCoroutine(FollowCat());
        }

        if (childMove.crashMonster)
        {
            StopAllCoroutines();
            Animator myAnimator = enemyCat.GetComponent<Animator>();
            myAnimator.Play("Neko - punch");
        }

        //if (enemyCat.activeSelf && 
        //    (childMove.isfalling || enemyCat.transform.position.y >= targetCat.transform.position.y))
        //{
        //    followPaused = true;
          
        //}

        //if (followPaused && !isCorouting)
        //{
        //    if (childMove.steppedStair[tempIndex].y <= childMove.steppedStair[childMove.steppedStair.Count - 1].y - 3)
        //    {
        //        followPaused = false;
        //        monInitIndex = childMove.steppedStair.Count - 3;
        //        //monInitIndex = childMove.steppedStair.Count - 3;
        //        i = tempAddI;
        //        StartCoroutine(FollowCat());
        //    }
        //}

        if (childMove.isFallAndDie && (isWaitCorouting || isCorouting))
        {
            StopAllCoroutines();
            enemyCat.SetActive(false);
        }

        // 고양이 떨어지고 떼껄룩 일시정지
        if (childMove.isfalling && !isWaitCorouting)
        {
            Debug.Log("일시정지");
            StopAllCoroutines();

            StartCoroutine(WaitAndFollow());
        }

        if (Time.timeScale == 0 && (isWaitCorouting || isCorouting))
        {
            StopAllCoroutines();
        }

        
    }

    public void MonsterPause()
    {

        StopAllCoroutines();

        StartCoroutine(WaitAndFollow());
    }

    IEnumerator WaitAndFollow()
    {
        Debug.Log("대기 코루틴 시작");
        isWaitCorouting = true;

        while (childMove.transform.position.y - childMove.steppedStair[tempIndex].y < 3)
        {
            Debug.Log($"떼껄룩 멈춘 위치 {childMove.steppedStair[tempIndex]}");
            Debug.Log("꼬라봄");
            enemyCat.transform.LookAt(childMove.transform.position);

            // 조건 충족 대기
            yield return null; // 다음 프레임까지 대기
        }
        Debug.Log("팔로우 재개");
        monInitIndex = childMove.steppedStair.Count - 3 - i;
        i = tempAddI;
        StartCoroutine(FollowCat());
    }

    IEnumerator FirstSpawn()
    {
        while (true)
        {
            if (childMove.steppedStair.Count == catInitIndex && !isFirstSpawn)
            {
                isSpawn = true;
                monInitIndex = catInitIndex - 3;
                enemyCat.transform.position = childMove.steppedStair[monInitIndex] + new Vector3(0, 0.1f, 0);
                enemyCat.SetActive(true);
                StartCoroutine(FollowCat());
                Debug.Log("첫 팔로우");
                yield break;
            }
            yield return waitInitSec;
        }

    }

    IEnumerator FollowCat()
    {

        childMove.isfalling = false;
        isCorouting = true;
        isWaitCorouting = false;
        Debug.Log("코루틴 실행~");
        yield return waitRealtime;

        Animator myAnimator = enemyCat.GetComponent<Animator>();
        _myRigidbody = enemyCat.GetComponent<Rigidbody>();


        while (i < 21)
        {
            myAnimator.Play("Jump");
            //_myRigidbody.AddForce(Vector3.up * 3f, ForceMode.Impulse);

            Debug.Log($"사용할 i는 {i}");
            
            // 오른쪽 위로 가면 오른쪽 방향 회전
            if (childMove.steppedStair[monInitIndex + i].x > enemyCat.transform.position.x)
            {

                enemyCat.transform.eulerAngles = new Vector3(0, 120f, 0);
            }
            else
            {

                enemyCat.transform.eulerAngles = new Vector3(0, 240f, 0);
            }
            Debug.Log(enemyCat.transform.position);
            //enemyCat.transform.position = childMove.steppedStair[monInitIndex + i] + new Vector3(0, 0.1f, 0);
            _myRigidbody.DOMove(childMove.steppedStair[monInitIndex + i] + new Vector3(0, 0.1f, 0), 0.1f);
            //_myRigidbody.DOJump(childMove.steppedStair[monInitIndex + i] + new Vector3(0, 0.1f, 0), 1f, 1, 0.2f,true).SetEase(Ease.Linear);
            
            Debug.Log(childMove.steppedStair[monInitIndex + i] + "갈 곳");

            i++;

            tempAddI = i;


            yield return waitRealtime;


            Debug.Log("while문 도는 중");
        }

        myAnimator.Play("Jump");
        Collider collider = enemyCat.GetComponent<Collider>();
        collider.isTrigger = true;
        _myRigidbody.AddForce(Vector3.forward * 10f, ForceMode.Impulse);
        i = 1;
        tempAddI = 0;
        isSpawn = false;
        yield return new WaitForSeconds(2f);
        Debug.Log("coroutine end");
        collider.isTrigger = false;
        enemyCat.SetActive(false);
        isCorouting = false;
    }


    private void Follow()
    {
        _myAnimator = enemyCat.GetComponent<Animator>();

        moveCurrentTime += Time.deltaTime;

        // if 말고 while을 썼음 ㅇㄴ 전에 돌아갓던 이유는 update문에 썼기 때문
        if (moveCurrentTime > delayTime)
        {
            moveCurrentTime = 0;
            _myAnimator.Play("Jump");

            enemyCat.transform.position = childMove.steppedStair[monInitIndex + i] + new Vector3(0, 0.2f, 0);
            i++;
        }
    }







}
