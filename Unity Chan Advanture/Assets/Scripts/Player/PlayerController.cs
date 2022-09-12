using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("相机相关")]
    public Transform cameraLookAt;
    public float maxXAngle;
    public float minXAngle;
    public bool isCanY;
    private float updownAngle;
    private float cameraAngle;

    private float MouseX;
    private float MouseY;

    [Header("移动属性")]
    public float moveSpeed = 1;
    public float rotateSpeed = 5f;
    public bool canMoving;
    public bool GetIsRun => isRun;
    public bool GetIsMove => isMoving;

    private float moveH;
    private float moveV;

    private bool isRun;
    private bool isMoving;
    private bool isStop;

    private Animator animator;
    public void PlayerContinue()
    {
        isStop = false;
    }
    public void PlayerStop()
	{
        isStop = true;
    }

	public void Initialized()
	{
        cameraAngle = 0;
        updownAngle = 0;
        isRun = true;
        isStop = false;
    }

	private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        Initialized();
    }
    void Update()
    {
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRun = false;
            moveSpeed /= 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed *= 2;
            isRun = true;
        }
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");
    }
    private void FixedUpdate()
    {
        if (!isStop) {
            if (canMoving)
            {

                if (Mathf.Abs(moveH) > 0.1 || Mathf.Abs(moveV) > 0.1)
                {
                    if (Mathf.Abs(moveH) == 1 && Mathf.Abs(moveV) == 1)
                    {
                        moveH *= 0.6f;
                        moveV *= 0.6f;
                    }
                    isMoving = true;
                    PlayerMove();
                }
                else
                {
                    isMoving = false;
                }
            }
            if (Mathf.Abs(MouseX) > 0.2 || Mathf.Abs(MouseY) > 0.8f)
            {
                if (isCanY)
                {
                    updownAngle += MouseY * Time.fixedDeltaTime * rotateSpeed;
                    if (updownAngle >= maxXAngle || updownAngle <= minXAngle)
                    {
                        MouseY = 0;
                        updownAngle = Mathf.Clamp(updownAngle, minXAngle, maxXAngle);
                    }
                }
                else
                {
                    MouseY = 0;
                    updownAngle = 0;
                }
                CameraRotation();
            }
            MovingAnimation();
        }
    }
    private void PlayerMove()
    {
        float sr = Mathf.Sin(cameraAngle);
        float cr = Mathf.Cos(cameraAngle);
        //坐标系旋转
        Vector3 newDirection = new Vector3((moveV * sr + moveH * cr) * moveSpeed, 0, (moveV * cr - moveH * sr) * moveSpeed);
        transform.Translate(newDirection * Time.fixedDeltaTime, Space.World);
        //转向新的方向
        this.transform.rotation = Quaternion.LookRotation(newDirection);

        //固定cameraLookAt
        float cu = Mathf.Sin(updownAngle * Mathf.Deg2Rad);
        cameraLookAt.rotation = Quaternion.LookRotation(new Vector3(sr, -cu, cr));
    }
    private void CameraRotation()
    {
        //mainCamera.transform.RotateAround(transform.position, Vector3.up, MouseX);
        //mainCamera.transform.LookAt(transform.position);
        //cameraAngle = mainCamera.transform.eulerAngles.y * Mathf.Deg2Rad;
        cameraLookAt.localEulerAngles += new Vector3(MouseY, MouseX, 0) * Time.fixedDeltaTime * rotateSpeed;
        cameraAngle = cameraLookAt.transform.eulerAngles.y * Mathf.Deg2Rad;
    }
    private void MovingAnimation()
    {
        animator.SetBool("isMoving", isMoving && canMoving);
        animator.SetBool("isRun", isRun);
    }
}
