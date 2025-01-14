using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using Zenject;

public class CameraController : MonoBehaviour
{
    
    [SerializeField] private Vector3 offset;            // 설정한 오프셋

    [SerializeField] private float rotationSpeed = 2f;   // 카메라 회전 속도
    [SerializeField] private float smoothSpeed = 0.125f; // 부드러운 이동 속도

    // 충돌 감지된 오브젝트들을 담는 리스트
    [HideInInspector] private List<Transform> targets = new List<Transform>();   // 감지된 오브젝트를 담아두는 배열. 현재로선 사용안되도 됨

    [Inject] private ProjectPlayer player;          // 플레이어 위치
    //[Inject] private InputManager input;
    [Inject] private ProjectInstaller.CameraSetting camSetting;
    [Inject] private ProjectInstaller.LockOnSetting lockOnSetting;

    private Vector3 current;

    private bool isLockOn = false;
    public bool IsLockOn {  get { return isLockOn; } set { isLockOn = value; } }

    private PlayerInputAction input;

    private void Start()
    {
        input = new PlayerInputAction();
        input.Enable();

        input.PlayerAction.Rot.performed += SetDeltaValue;
        input.PlayerAction.Rot.canceled += (_)=> { deltaVec = Vector2.zero; };

        //Cursor.lockState = CursorLockMode.Locked;
        CamSetting();
        input.PlayerAction.LockOn.started += (_) => { LockOnDown(); };
        input.PlayerAction.LockOn.canceled += (_) => { LockOnUp(); };
    }

    private void SetDeltaValue(InputAction.CallbackContext obj)
    {
        Vector2 value = obj.ReadValue<Vector2>();

        deltaVec.x = value.x;
        deltaVec.y = value.y;
    }

    private void CamSetting()
    {
        offset.y = camSetting.Height;
        offset.z = camSetting.Dist;
        rotationSpeed = camSetting.RotationSpeed;
        smoothSpeed = camSetting.SmoothSpeed;
    }

    private void LateUpdate()
    {
        if (player == null)
            return;

        HandleCameraRotation();
        FollowTarget();

    }

    private Vector2 deltaVec;

    private void HandleCameraRotation()
    {
        if (isLockOn)
        {
            return;
        }

        current.x += deltaVec.x * rotationSpeed;
        current.y += deltaVec.y * rotationSpeed;

        current.y = Mathf.Clamp(current.y, 7, 7);
    }

    /// <summary>
    /// 카메라가 플레이어를 따라가는 함수
    /// </summary>
    private void FollowTarget()
    {
        if(isLockOn && monster.activeInHierarchy)
        {
            Vector3 directionToMonster = (monster.transform.position - player.transform.position);
            directionToMonster.y = 0;
            directionToMonster.Normalize();
            Quaternion lockOnRotation = Quaternion.LookRotation(directionToMonster);

            transform.position = player.transform.position + offset.z * directionToMonster + offset.y * Vector3.up;
            transform.LookAt(player.transform.position);


            CheckWall();

        }
        else
        {

            Quaternion rotation = Quaternion.Euler(0, current.x, 0f);
            transform.position = player.transform.position + rotation * offset;
            transform.LookAt(player.transform.position);

            CheckWall();
        }

    }

    public GameObject monster;

    // 락온을 누르고 있는상태 => 몬스터가 죽으면
    // 근데, 락온버튼을 떼지 않으면 락온 해제가 안됨

    // 의도 : 락온을 하고있는 상태 => 몬스터가 죽음 => 락온이 끝나야함

    /// <summary>
    /// 락온기능 키고 끄는 함수
    /// </summary>
    private void LockOn()
    {
        //Debug.Log("락온 진행중");
        GetTarget();
        if (targets.Count > 0)
        {
            monster = GetTargetMonster();
            if (monster != null)
            {
                isLockOn = true;
                //Debug.Log("락온 기능 활성화");
            }
        }
        else
        {
            IsLockOn = false;
            //Debug.Log("감지된 몬스터가 없습니다.");
        }
    }

    /// <summary>
    /// 범위 안에 몇 마리의 몬스터가 존재하는지 확인하는 함수
    /// </summary>
    public void GetTarget()
    {
        targets.Clear();    // 배열 초기화
        Collider[] TargetCollider = Physics.OverlapSphere(player.transform.position, lockOnSetting.viewArea, lockOnSetting.targetLayer);

        for (int i = 0; i < TargetCollider.Length; i++) // 감지된 콜라이더 
        {
            Transform target = TargetCollider[i].transform;
            Vector3 direction = target.position - player.transform.position;

            if (Vector3.Dot(direction.normalized, player.transform.forward) > GetAngle(lockOnSetting.viewAngle / 2).z)
            {
                //Debug.Log(GetAngle(lockOnSetting.viewAngle / 2).z);

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
            float distance = Vector3.Distance(player.transform.position, target.position);
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

    private void CheckWall()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y + 2 , player.transform.position.z);
        Vector3 cameraPosition = transform.position;

        Ray ray = new Ray(playerPosition, (cameraPosition - playerPosition ) * Mathf.Abs(offset.z));

        Debug.DrawRay(playerPosition, (cameraPosition - playerPosition) * Mathf.Abs(offset.z), Color.red);

        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Abs(offset.z)))
        {
            //Debug.Log("벽 확인 됨");
            Vector3 hitPoint = hit.point;
            transform.position = hit.point + hit.normal * 0.5f;
        }
        else
        {
            //Debug.Log("벽 확인 안됨");
            //Quaternion rotation = Quaternion.Euler(0, current.x, 0);
            //transform.position = player.transform.position + rotation * offset;
        }
    }
}
