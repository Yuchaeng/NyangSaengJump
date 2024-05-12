using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceMove : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 dir = Vector3.zero;

        dir.x = Input.acceleration.y;
        dir.z = Input.acceleration.z;

        if (dir.sqrMagnitude > 1)
        {
            dir.Normalize();
        }

        dir *= Time.deltaTime;

        transform.position = new Vector3(dir.x * speed, transform.position.y, dir.y * speed);
        //rb.velocity = new Vector3(dir.x * speed, transform.position.y, dir.y * speed);
    }
}
