using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    Camera camera = new Camera();
    [SerializeField] private float MoveSpeed = 0;
    [SerializeField] private int CameraSpeed = 1;
    //[SerializeField] Vector3 CameraVertRotateAxis = new Vector3(0, 1, 0);
    //[SerializeField] Vector3 CameraHorRotateAxis = new Vector3(1, 0, 0);
    [SerializeField] Rigidbody body;


    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        SerPosition();
        body = GetComponent<Rigidbody>();
        SetCameraSpeed();
    }

    private void SerPosition()
    {
        camera.transform.position = new Vector3(0,2,4);
    }

    /// <summary>
    /// moveSpeed equal CameraSpeed multiply deltaTime
    /// </summary>

    // Update is called once per frame
    void Update()
    {
        CameraAllEvents();
    }

    private void CameraAllEvents()
    {
        SetCameraSpeed();
        MakeMoveCamera();
        MakeRotateCamera();
    }

    /// <summary>
    /// Check if leftShift is pressed, speed multiply by 5, esle speed is 1
    /// </summary>
    private void SetCameraSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            CameraSpeed = 5;
        }
        else
        {
            CameraSpeed = 1;
        }
        MoveSpeed = CameraSpeed * Time.deltaTime;
    }

    /// <summary>
    /// move camera forward or back, left or right by press buttons "W", "S", "A", "D"
    /// if mouse height position (axis y) is lower then 2, camera height set 2, if higher then 10, camera height set to 10.
    /// this is for camera dont fall on floor or dont get very hight up.
    /// </summary>
    private void MakeMoveCamera()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 0, MoveSpeed), relativeTo: Space.Self);
            float y = transform.position.y;
            if (y < 2)
            {
                transform.position = new Vector3(transform.position.x, 2, transform.position.z);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, 0, MoveSpeed * -1), relativeTo: Space.Self);
            float y = transform.position.y;
            if (y > 10)
            {
                transform.position = new Vector3(transform.position.x, 10, transform.position.z);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(MoveSpeed * -1, 0, 0), relativeTo: Space.Self);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(MoveSpeed, 0, 0), relativeTo: Space.Self);
        }
    }

    private void MakeRotateCamera()
    {
        if (Input.GetKey(KeyCode.Mouse2)) // Mouse2 is middle mouse button (roller) press
        {
            float Y = Input.GetAxis("Mouse Y"); // mouse horizontal axis
            float X = Input.GetAxis("Mouse X"); // mouse vertical axis

            // if mouse moove Left or Right when pressed middleMouseButton
            if (Y > 0)
            {
                transform.Rotate(new Vector3(-1, 0, 0), relativeTo: Space.Self); 
            }
            else if(Y < 0)
            {
                transform.Rotate(new Vector3(1, 0, 0), relativeTo: Space.Self);
            }
            // if mouse moove Up or Down when pressed middleMouseButton
            if (X > 0.1)
            {
                transform.Rotate(new Vector3(0, 2, 0), relativeTo: Space.World);
            }
            else if (X < -0.1)
            {
                transform.Rotate(new Vector3(0, -2, 0), relativeTo:Space.World);
            }
        }
    }


}
