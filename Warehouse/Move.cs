using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody _myRigid;
    private Vector3 _inputVec;
    [SerializeField] private bool _isMovable;
    [SerializeField] private bool _isDirectionChangeable;
    [SerializeField] private bool _isFalling;
    [SerializeField] private float _moveSpeed = 1.2f;
    [SerializeField] private float _jumpForce = 5f;

    public Animator animator;
    private int direction;
    public bool isJumping = false;

    Vector3 limitPos;

    RaycastHit hit;
    public float lineSize = 2f;

    public float rightRotate;
    public float leftRotate;

    Vector3 lookDirection;

    public Camera camera2d;
    
    private void Start()
    {
        _myRigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        _isMovable = true;
        _isDirectionChangeable = true;
        _isFalling = false;
    }

    private void Update()
    {
        if (_isFalling)
        {
            _isMovable = false;
            _isDirectionChangeable = false;
        }
        else
        {
            _isMovable = true;
            _isDirectionChangeable = true;
        }

        if (_isMovable)
        {
            _inputVec.x = Input.GetAxisRaw("Horizontal");
            _inputVec.z = Input.GetAxisRaw("Vertical");

            animator.SetBool("IsWalking", _inputVec.x != 0 || _inputVec.z != 0);

        }

      


        /*if (_isMovable)
        {
           

            //if (Input.GetKey(KeyCode.RightArrow))
            //{
            //    direction = 1; //坷弗率
            //    _inputVec.x = Input.GetAxisRaw("Horizontal");
            //    animator.SetBool("IsWalking", true);
            //}
            //else if (direction == 1 && !Input.GetKey(KeyCode.RightArrow))
            //{
            //    _inputVec.x = 0;
            //    animator.SetBool("IsWalking", false);
            //}

            //if (Input.GetKey(KeyCode.LeftArrow))
            //{
            //    direction = -1; //哭率
            //    _inputVec.x = Input.GetAxisRaw("Horizontal");
            //    animator.SetBool("IsWalking", true);

            //}
            //else if (direction == -1 && !Input.GetKey(KeyCode.LeftArrow))
            //{
            //    _inputVec.x = 0;
            //    animator.SetBool("IsWalking", false);
            //}
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                direction = 1;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction = -1;
            }
        }

        if (_isDirectionChangeable)
        {
            // 坷弗率
            if (direction == 1) 
            {
                transform.rotation = Quaternion.Euler(0, rightRotate, 0);
            }
            // 哭率
            else if (direction == -1)
            {
                transform.rotation = Quaternion.Euler(0, leftRotate, 0);
            }
        }*/
        


        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !_isFalling)
        {
            isJumping = true;
            _isMovable = false;

            animator.SetTrigger("IsJump");
            _myRigid.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);


            //animator.Play("Jump");
            //_myRigid.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            //animator.SetTrigger("IsJump");

        }




    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Stair")
        {
            if (isJumping)
            {
                isJumping = false;
                _isMovable = true;
            }

            if (_isFalling)
                _isFalling = false;

            //animator.SetBool("IsJumping", false);
        }

        if (collision.gameObject.tag == "Grass")
        {
            if (isJumping)
            {
                isJumping = false;
                _isMovable = true;
            }

            if (_isFalling)
                _isFalling = false;
        }
    }

    private void FixedUpdate()
    {

        //if (camera2d.enabled)
        //{
        //    limitPos = Camera.main.WorldToViewportPoint(transform.position);

        //    if (limitPos.x < 0.045f)
        //        _inputVec.x = 1f;
        //    if (limitPos.x > 0.95f)
        //        _inputVec.x = -1f;

        //}
        

        //if (limitPos.y < 0f)
        //    _inputVec.y = 0f;
        //if (limitPos.y > 1f)
        //    _inputVec.y = 1f;

        //_myRigid.MovePosition(_inputVec.normalized * _moveSpeed * Time.fixedDeltaTime);
        //_myRigid.AddForce(_inputVec.normalized * _moveSpeed * Time.fixedDeltaTime, ForceMode.Force);
        //_myRigid.velocity = _inputVec.normalized * _moveSpeed * Time.fixedDeltaTime;
        transform.position += _inputVec.normalized * _moveSpeed * Time.fixedDeltaTime;

        if ((_inputVec.x != 0 || _inputVec.z != 0) && _isDirectionChangeable)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_inputVec), 20f * Time.fixedDeltaTime);


            //transform.LookAt(transform.position + _inputVec);


            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position + _inputVec), Time.deltaTime);



    }

 

    private void Jump()
    {
        if (!isJumping)
            return;

        animator.SetTrigger("IsJump");
        _myRigid.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

        isJumping = false;
    }

}
