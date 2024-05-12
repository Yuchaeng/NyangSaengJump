using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public abstract class PlayerMoveBase : MonoBehaviour
{
    public Rigidbody _myRigid;
    protected Animator _animator;

    protected Ray _ray;
    protected Ray _rayDown;
    public LayerMask stairLayer;

    private Vector3 _fwd;

    protected abstract float RayUpDistance { get; }
    protected abstract float RayUpCastingDistance { get; }
    protected abstract float RayDownCastingDistance { get; }

    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private bool _isJumping = false;
    public bool isMovable;
    [SerializeField] private float _xValue;
    [SerializeField] private float _yValue;

    protected GameObject lastStair;
    protected GameObject currentStair;

    public bool isFallAndDie;
    [SerializeField] private bool isStun;
    float currentTime = 0;
    float stunTime = 2;

    public bool fallButLive;

    ItemPickUp _itemPickUp;
    
    protected virtual void Start()
    {
        _myRigid = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _itemPickUp = GetComponent<ItemPickUp>();

        isMovable = true;

        _animator.Play(0);
    }

    protected void Update()
    {
        // 터치
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).position.x > Screen.width / 2 && _isJumping == false)  // 화면 가로 중간보다 오른쪽 영역
            {
                
                _animator.Play("Jump");

                _isJumping = true;

               
                transform.eulerAngles = new Vector3(-9f, 120f, 7f);
                _myRigid.velocity = new Vector3(_xValue, _yValue, 0.0f) * _jumpForce;
            }
            else if (Input.GetTouch(0).position.x < Screen.width / 2 && _isJumping == false)
            {
               
                _animator.Play("Jump");

                _isJumping = true;

                
                transform.eulerAngles = new Vector3(-9f, 240f, -7f);
                _myRigid.velocity = new Vector3(-_xValue, _yValue, 0.0f) * _jumpForce;
            }
        }

        // 키보드
        if (Input.GetKeyDown(KeyCode.RightArrow) && _isJumping == false && isMovable)
        {
            _animator.Play("Jump");
            _isJumping = true;
            isMovable = false;

            transform.eulerAngles = new Vector3(0.0f, 110f, 0.0f);

            _myRigid.velocity = new Vector3(_xValue, _yValue, 0.0f) * _jumpForce;

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && _isJumping == false && isMovable)
        {

            _animator.Play("Jump");

            _isJumping = true;
            isMovable = false;

            transform.eulerAngles = new Vector3(0.0f, 250f, 0.0f);

            _myRigid.velocity = new Vector3(-_xValue, _yValue, 0.0f) * _jumpForce;
        }
    }

    protected virtual void FixedUpdate()
    {
        _fwd = transform.TransformDirection(Vector3.forward + Vector3.up * RayUpDistance);
        _ray = new Ray(transform.position, _fwd);
        RaycastHit hit;
        Debug.DrawRay(transform.position, _fwd * RayUpCastingDistance, Color.red);

        if (Physics.Raycast(_ray, out hit, RayUpCastingDistance, stairLayer))
        {
            Debug.Log("위에 있는");
            Vector3 correctPos = new Vector3(hit.collider.bounds.center.x, hit.collider.bounds.center.y + 0.2f, 0);
            _myRigid.DOMove(correctPos, 0.1f);
            //_myRigid.MovePosition(correctPos);
        }


        _rayDown = new Ray(transform.position, Vector3.down);
        RaycastHit downHit;
        Debug.DrawRay(transform.position, Vector3.down * RayDownCastingDistance, Color.blue);

        if (Physics.Raycast(_rayDown, out downHit, RayDownCastingDistance, stairLayer))
        {
            if (transform.position.x != downHit.collider.bounds.center.x)
            {
                transform.position = new Vector3(downHit.collider.bounds.center.x, transform.position.y, 0);
            }
        }

        // 낙하 판정
        if (_itemPickUp.isCatnipMove)
        {
            // 캣닙먹고 위로 뛰었는데 옆으로 점프했다가 빠져서 추락하면 게임오버

            if (lastStair.transform.position.y - transform.position.y >= 10f)
            {
                isFallAndDie = true;

                StageManager.Instance.StopGame();
            }
        }
        else if (isStun)
        {
            // 스턴 걸린 경우, 스턴 중에는 판정X

            currentTime += Time.fixedDeltaTime;

            if (currentTime > stunTime)
            {
                isStun = false;
                isMovable = true;
                lastStair = null;   // 이렇게 안하면 계속 판정해서 스턴 안풀림
                currentTime = 0;
            }
        }
        else if (currentStair != null && lastStair != null)
        {
            // 스턴 중 아니고 캣닙 먹은 거 아닐 때 판정

            if (lastStair.transform.position.y - transform.position.y >= 12f)
            {
                isFallAndDie = true;
                StageManager.Instance.StopGame();
            }
            else if (lastStair.transform.position.y - currentStair.transform.position.y >= 7.5f &&
                     lastStair.transform.position.y - currentStair.transform.position.y < 12f)
            {
                isStun = true;
                fallButLive = true;
                isMovable = false;
            }


        }

    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Stair") && transform.position.y > collision.transform.position.y)
        {
            if (currentStair != collision.gameObject)
            {
                lastStair = currentStair;
                currentStair = collision.gameObject;
            }

        }

        if (collision.transform.CompareTag("Stair"))
        {
            if (_isJumping)
            {
                _isJumping = false;
                isMovable = true;
            }
        }

        if (_itemPickUp.isCatnipMove)
        {
            if (collision.transform.CompareTag("Stair") && transform.position.y > collision.transform.position.y)
            {
                Debug.Log("캣닙 풀어줌");

                lastStair = null;    // 뛰었다가 착지한 거 판정되면 안됨
                _itemPickUp.isCatnipMove = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            StageManager.Instance.ClearGame();
        }
    }
}
