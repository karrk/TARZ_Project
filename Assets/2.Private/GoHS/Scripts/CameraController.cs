using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    [SerializeField] private float rotationSpeed = 2f;   // 카메라 회전 속도
    [SerializeField] private float smoothSpeed = 0.125f; // 부드러운 이동 속도

    private float currenX = 0f;
    private float currenY = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   // 마우스 커서를 고정
    }

    private void LateUpdate()
    {
        HandleCameraRotation();
        FollowTarget();
    }

    private void HandleCameraRotation()
    {
        currenX += Input.GetAxis("Mouse X") * rotationSpeed;
        currenY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        currenY = Mathf.Clamp(currenY, 7, 7);
    }

    private void FollowTarget()
    {
        Quaternion rotation = Quaternion.Euler(currenY, currenX, 0f);
        //Vector3 desiredPosition = target.position + rotation * offset;
        //transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = target.position + rotation * offset;
        transform.LookAt(target.position);
    }


}
