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

    // 판정 범위 거리
    [SerializeField] private float viewArea;

    // 판정 범위 각도
    [Range(0, 360)]
    [SerializeField] private float viewAngle;

    // 충돌 감지 대상
    [SerializeField] private LayerMask targetMask;

    // 충돌 감지된 오브젝트들을 담는 리스트
    [HideInInspector]
    [SerializeField] private List<Transform> targets = new List<Transform>();   // 감지된 오브젝트를 담아두는 배열. 현재로선 사용안되도 됨

    private float currenX = 0f;
    private float currenY = 0f;

    private Vector3 current;

    private bool isLockOn = false;
    public bool IsLockOn {  get { return isLockOn; } set { isLockOn = value; } }


    [Inject]
    private void Init(ProjectPlayer player, ProjectInstaller.CameraSetting setting, InputManager manager)
    {
        Cursor.lockState = CursorLockMode.Locked;   // 마우스 커서를 고정
        this.player = player.transform;
        manager.OnControlledRightStick += HandleCameraRotation;
        manager.OnDownL2Key += LockOnDown;
        manager.OnUpL2Key += LockOnUp;
        CamSetting(setting);
    }

    private void CamSetting(ProjectInstaller.CameraSetting setting)
    {
        offset.y = setting.Height;
        offset.z = setting.Dist;
        rotationSpeed = setting.RotationSpeed;
        smoothSpeed = setting.SmoothSpeed;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.J))
        {


        }

        if(Input.GetKeyUp(KeyCode.J))
        {

        }
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
        if(isLockOn)
        {
            return;
        }            

        current.x += vec.x * rotationSpeed;
        current.y -= vec.y * rotationSpeed;

        current.y = Mathf.Clamp(current.y, 7, 7);
        //currenX += Input.GetAxis("Mouse X") * rotationSpeed;
        //currenY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        //currenY = Mathf.Clamp(currenY, 7, 7);
    }

    /// <summary>
    /// 카메라가 플레이어를 따라가는 함수
    /// </summary>
    private void FollowTarget()
    {
        if(isLockOn && monster != null)
        {
            Vector3 directionToMonster = (monster.transform.position - player.position);
            directionToMonster.y = 0;
            directionToMonster.Normalize();
            Quaternion lockOnRotation = Quaternion.LookRotation(directionToMonster);

            transform.position = player.position + offset.z * directionToMonster + offset.y * Vector3.up;

            //Vector3 resultDirection = new Vector3(directionToMonster.x, directionToMonster.y, directionToMonster.z);
            //transform.position = player.position + resultDirection;
            transform.LookAt(player.position);
        }
        else
        {
            Quaternion rotation = Quaternion.Euler(0, current.x, 0f);
            //Vector3 desiredPosition = target.position + rotation * offset;
            //transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = player.position + rotation * offset;
            transform.LookAt(player.position);
        }

    }

    public GameObject monster;


    /// <summary>
    /// 락온기능 키고 끄는 함수
    /// </summary>
    private void LockOn()
    {
        //if (!isLockOn)
        //{
        //    GetTarget();
        //    if (targets.Count > 0)
        //    {
        //        monster = GetTargetMonster();
        //        if (monster != null)
        //        {
        //            isLockOn = true;
        //            Debug.Log("락온 기능 활성화");
        //        }
        //    }
        //    else
        //    {
        //        IsLockOn = false;
        //        Debug.Log("감지된 몬스터가 없습니다.");
        //    }
        //}
        //else
        //{
        //    isLockOn = false;
        //    monster = null;
        //    Debug.Log("락온 기능 비활성화");
        //}

        GetTarget();
        if (targets.Count > 0)
        {
            monster = GetTargetMonster();
            if (monster != null)
            {
                isLockOn = true;
                Debug.Log("락온 기능 활성화");
            }
        }
        else
        {
            IsLockOn = false;
            Debug.Log("감지된 몬스터가 없습니다.");
        }
    }

    /// <summary>
    /// 범위 안에 몇 마리의 몬스터가 존재하는지 확인하는 함수
    /// </summary>
    public void GetTarget()
    {
        targets.Clear();    // 배열 초기화
        Collider[] TargetCollider = Physics.OverlapSphere(player.transform.position, viewArea, targetMask);

        for (int i = 0; i < TargetCollider.Length; i++) // 감지된 콜라이더 
        {
            Transform target = TargetCollider[i].transform;
            Vector3 direction = target.position - player.transform.position;

            if (Vector3.Dot(direction.normalized, player.transform.forward) > GetAngle(viewAngle / 2).z)
            {
                Debug.Log(GetAngle(viewAngle / 2).z);

                IDamagable damagable = target.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    targets.Add(target);
                }
            }
        }
    }


    /// <summary>
    /// 가장 가까이에 있는 몬스터를 판별하여 리턴해주는 함수
    /// </summary>
    /// <returns></returns>
    private GameObject GetTargetMonster()
    {
        GameObject targetMonster = null;
        float minDistance = float.MaxValue;

        foreach (Transform target in targets)
        {
            float distance = Vector3.Distance(player.position, target.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                targetMonster = target.gameObject;
            }
        }

        return targetMonster;
    }

    public Vector3 GetAngle(float AngleInDegree)
    {
        return new Vector3(Mathf.Sin(AngleInDegree * Mathf.Deg2Rad), 0, Mathf.Cos(AngleInDegree * Mathf.Deg2Rad));
    }

    private void LockOnDown()
    {
        if (!isLockOn)
        {
            LockOn();
        }
        else
        {
            return;
        }
    }

    private void LockOnUp()
    {
        monster = null;
        isLockOn = false;
    }
}
