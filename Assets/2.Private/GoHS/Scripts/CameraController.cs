using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;          // 플레이어 위치
    [SerializeField] private Vector3 offset;            // 설정한 오프셋

    [SerializeField] private float rotationSpeed = 2f;   // 카메라 회전 속도
    [SerializeField] private float smoothSpeed = 0.125f; // 부드러운 이동 속도

    private float currenX = 0f;
    private float currenY = 0f;

    private Vector3 current;

    [Inject]
    private void Init(ProjectPlayer player, ProjectInstaller.CameraSetting setting, InputManager manager)
    {
        Cursor.lockState = CursorLockMode.Locked;   // 마우스 커서를 고정
        this.player = player.transform;
        manager.OnControlledRightStick += HandleCameraRotation;
        CamSetting(setting);
    }

    private void CamSetting(ProjectInstaller.CameraSetting setting)
    {
        offset.y = setting.Height;
        offset.z = setting.Dist;
        rotationSpeed = setting.RotationSpeed;
        smoothSpeed = setting.SmoothSpeed;
    }

    private void LateUpdate()
    {
        if (player == null)
            return;

        //HandleCameraRotation();
        FollowTarget();
    }

    private void HandleCameraRotation(Vector3 vec)
    {
        current.x += vec.x * rotationSpeed;
        current.y -= vec.y * rotationSpeed;

        current.y = Mathf.Clamp(current.y, 7, 7);
        //currenX += Input.GetAxis("Mouse X") * rotationSpeed;
        //currenY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        //currenY = Mathf.Clamp(currenY, 7, 7);
    }

    private void FollowTarget()
    {
        Quaternion rotation = Quaternion.Euler(current.y, current.x, 0f);
        //Vector3 desiredPosition = target.position + rotation * offset;
        //transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = player.position + rotation * offset;
        transform.LookAt(player.position);
    }


}
