using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class DrainState : BaseState
{

    //[SerializeField] private ProjectPlayer player;

    //public DrainState (ProjectPlayer player)
    //{
    //    this.player = player;
    //    this.viewAngle = 360f;
    //    this.maxViewArea = 10f;
    //    this.viewSpeed = 10f;
    //    this.targetMask = LayerMask.GetMask("Garbage");
    //}

    public DrainState(ProjectPlayer player) : base(player)
    {
    }

    // 판정 범위 거리
    [SerializeField] private float viewArea;
    [SerializeField] private float maxViewArea;
    [SerializeField] private float viewSpeed;

    // 판정 범위 각도
    [Range(0, 360)]
    [SerializeField] private float viewAngle;

    // 충돌 감지 대상
    [SerializeField] private LayerMask targetMask;

    // 충돌 감지된 오브젝트들을 담는 리스트
    [HideInInspector]
    [SerializeField] private List<Transform> Targets = new List<Transform>();

    [SerializeField] private float drainSpeed;

    public override void Enter()
    {
        Debug.Log("@@@@@@@@@@@@@@수집상태 진입 성공");
        player.Refernece.Animator.SetBool("Drain", true);
        isDrainMode = true;
    }

    private bool isDrainMode = false;

    

    public void StopDrain()
    {
        player.Setting.DrainSetting.ViewArea = 0;
        player.Refernece.Animator.SetBool("Drain", false);
        isDrainMode = false;
    }

    public override void Update()
    {
        if(isDrainMode)
        {
            Debug.Log("드레인 진행중!");
            IncreaseViewArea();
            GetTarget();
        }
        else
        {
            player.Setting.DrainSetting.ViewArea = 0;
            player.Refernece.Animator.SetBool("Drain", false);
            player.ChangeState(E_State.Idle);
        }

    }
    
    /// <summary>
    /// 판정 범위 거리를 0부터 서서히 maxViewArea값까지 올려주는 함수
    /// </summary>
    private void IncreaseViewArea()
    {
        //Debug.Log(viewArea);
        player.Setting.DrainSetting.ViewArea += player.Setting.DrainSetting.ViewSpeed * Time.deltaTime;
        player.Setting.DrainSetting.ViewArea = Mathf.Clamp(player.Setting.DrainSetting.ViewArea, 0, player.Setting.DrainSetting.MaxViewArea);
    }


    /// <summary>
    /// 설정한 범위내의 충돌체를 감지하는 함수
    /// </summary>
    public void GetTarget()
    {
        Targets.Clear();    // 배열 초기화
        Collider[] TargetCollider = Physics.OverlapSphere(player.transform.position, player.Setting.DrainSetting.ViewArea, player.Setting.DrainSetting.TargetMask);

        for (int i = 0; i < TargetCollider.Length; i++)
        {
            Transform target = TargetCollider[i].transform;
            Vector3 direction = target.position - player.transform.position;

            if (Vector3.Dot(direction.normalized, player.transform.forward) > GetAngle(player.Setting.DrainSetting.ViewAngle / 2).z)
            {
                Debug.Log(GetAngle(player.Setting.DrainSetting.ViewAngle / 2).z);
                Targets.Add(target);

                IDrainable drainable = target.GetComponent<IDrainable>();
                if (drainable != null)
                {
                    drainable.DrainTowards(player.transform.position, player.Setting.DrainSetting.DrainSpeed);
                    Debug.Log($"빨아들이는중 {target.name}");
                }

            }
        }
    }

    public Vector3 GetAngle(float AngleInDegree)
    {
        return new Vector3(Mathf.Sin(AngleInDegree * Mathf.Deg2Rad), 0, Mathf.Cos(AngleInDegree * Mathf.Deg2Rad));
    }


    public override void Exit()
    {
        StopDrain();
    }
}
