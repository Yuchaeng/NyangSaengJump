using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAdultMove : MonoBehaviour
{
    //public FixedJoystick joystick;
    private Rigidbody rb;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    Animator animator;
    Vector3 inputVec;
    Ray _rayDown;
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] private bool _isJumping;
    float horizontal;
    float vertical;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.EulerRotation(inputVec), 20f * Time.fixedDeltaTime);

        //Vector3 dir = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        //dir.Normalize();
        //transform.position += dir * Time.deltaTime * _moveSpeed;
        //animator.SetBool("IsWalking", joystick.Horizontal != 0 || joystick.Vertical != 0);

        //if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20f * Time.fixedDeltaTime);
       
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.z = Input.GetAxisRaw("Vertical");
        
        //rb.velocity = inputVec * _moveSpeed;
        
        if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
        {
            _isJumping = true;
            animator.SetTrigger("IsJump");
            rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        //if (Input.GetTouch(0).phase == TouchPhase.Ended)
        //{
        //    animator.SetTrigger("IsJump");
        //    rb.AddForce(Vector3.up * _jumpForce);
        //}

        //_rayDown = new Ray(transform.position, Vector3.down * 0.5f);
        //RaycastHit downHit;
        //Debug.DrawRay(transform.position, Vector3.down * 0.5f, Color.blue);

        //if (Physics.Raycast(_rayDown, out downHit, 0.5f, stairLayer))
        //{
        //    if (transform.position.x != downHit.collider.bounds.center.x)
        //    {
        //        transform.position = new Vector3(downHit.collider.bounds.center.x, transform.position.y, transform.position.z);
        //    }
        //}


    }
    private void FixedUpdate()
    {
        transform.position += inputVec.normalized * _moveSpeed * Time.deltaTime;
        //if (inputVec.x != 0 || inputVec.z != 0)
        //    transform.rotation = Quaternion.LookRotation(inputVec);
        transform.LookAt(transform.position + inputVec);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Stair")
        {
            if (_isJumping)
            {
                _isJumping = false;

            }
        }
    }
}
