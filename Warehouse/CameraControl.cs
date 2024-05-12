using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera camera2d;
    public Camera camera3d;

    // Start is called before the first frame update
    void Start()
    {
        camera2d.enabled = true;
        camera3d.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (camera2d.enabled)
            {
                camera2d.enabled = false;
                camera3d.enabled = true;
            }
            else
            {
                camera2d.enabled = true;
                camera3d.enabled = false;
            }
        }
    }
}
