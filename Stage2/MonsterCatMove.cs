using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class MonsterCatMove : MonoBehaviour
{
    // ������ ����, pc���� 2ĭ �Ʒ�
    // 1�ʿ� �� �� ����
    // pc�� ��ġ �˾ƾ� ��

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
            Debug.Log("���Ǹ¾Ƽ� ����");
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

        // ����� �������� ������ �Ͻ�����
        if (childMove.isfalling && !isWaitCorouting)
        {
            Debug.Log("�Ͻ�����");
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
        Debug.Log("��� �ڷ�ƾ ����");
        isWaitCorouting = true;

        while (childMove.transform.position.y - childMove.steppedStair[tempIndex].y < 3)
        {
            Debug.Log($"������ ���� ��ġ {childMove.steppedStair[tempIndex]}");
            Debug.Log("����");
            enemyCat.transform.LookAt(childMove.transform.position);

            // ���� ���� ���
            yield return null; // ���� �����ӱ��� ���
        }
        Debug.Log("�ȷο� �簳");
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
                Debug.Log("ù �ȷο�");
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
        Debug.Log("�ڷ�ƾ ����~");
        yield return waitRealtime;

        Animator myAnimator = enemyCat.GetComponent<Animator>();
        _myRigidbody = enemyCat.GetComponent<Rigidbody>();


        while (i < 21)
        {
            myAnimator.Play("Jump");
            //_myRigidbody.AddForce(Vector3.up * 3f, ForceMode.Impulse);

            Debug.Log($"����� i�� {i}");
            
            // ������ ���� ���� ������ ���� ȸ��
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
            
            Debug.Log(childMove.steppedStair[monInitIndex + i] + "�� ��");

            i++;

            tempAddI = i;


            yield return waitRealtime;


            Debug.Log("while�� ���� ��");
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

        // if ���� while�� ���� ���� ���� ���ư��� ������ update���� ��� ����
        if (moveCurrentTime > delayTime)
        {
            moveCurrentTime = 0;
            _myAnimator.Play("Jump");

            enemyCat.transform.position = childMove.steppedStair[monInitIndex + i] + new Vector3(0, 0.2f, 0);
            i++;
        }
    }







}
