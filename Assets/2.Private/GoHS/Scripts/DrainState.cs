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
            viewArea = 0;
            player.Refernece.Animator.SetBool("Drain", false);
            player.ChangeState(E_State.Idle);
        }

        //if(Input.GetKey(KeyCode.LeftControl))
        //{
        //    Debug.Log("드레인 진행중!");
        //    IncreaseViewArea();
        //    GetTarget();
        //}
        //else
        //{
        //    viewArea = 0f;
        //    player.animator.SetBool("Drain", false);

        //    AnimatorStateInfo stateInfo = player.animator.GetCurrentAnimatorStateInfo(0);
        //    if(stateInfo.IsName("Absorb_End_KHS") && stateInfo.normalizedTime >= 1.0f)
        //    {
        //        Debug.Log("드레인 끝 확인됨");
        //        player.ChangeState(E_State.Idle);
        //    }
        //    else if(stateInfo.normalizedTime >= 0.5f)
        //    {
        //        Debug.Log("드레인 끝 확인됨");
        //        player.ChangeState(E_State.Idle);
        //    }

        //}
    }
    
    /// <summary>
    /// 판정 범위 거리를 0부터 서서히 maxViewArea값까지 올려주는 함수
    /// </summary>
    private void IncreaseViewArea()
    {
        //Debug.Log(viewArea);
        viewArea += viewSpeed * Time.deltaTime;
        viewArea = Mathf.Clamp(viewArea, 0, maxViewArea);
    }


    /// <summary>
    /// 설정한 범위내의 충돌체를 감지하는 함수
    /// </summary>
    public void GetTarget()
    {
        Targets.Clear();    // 배열 초기화
        Collider[] TargetCollider = Physics.OverlapSphere(player.transform.position, viewArea, targetMask);

        for (int i = 0; i < TargetCollider.Length; i++)
        {
            Transform target = TargetCollider[i].transform;
            Vector3 direction = target.position - player.transform.position;

            if (Vector3.Dot(direction.normalized, player.transform.forward) > GetAngle(viewAngle / 2).z)
            {
                Debug.Log(GetAngle(viewAngle / 2).z);
                Targets.Add(target);

                IDrainable drainable = target.GetComponent<IDrainable>();
                if (drainable != null)
                {
                    drainable.DrainTowards(player.transform.position, drainSpeed);
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

    }

    //public void OnDrawGizmos()
    //{
    //    Debug.Log("~~~~~~~~~기즈모 실행중~~~~~~~~~~~");
    //    Handles.DrawWireArc(player.transform.position, Vector3.up, player.transform.forward, 360, viewArea);
    //    Handles.DrawLine(player.transform.position, player.transform.position + GetAngle(-viewAngle / 2) * viewArea);
    //    Handles.DrawLine(player.transform.position, player.transform.position + GetAngle(viewAngle / 2) * viewArea);

    //    foreach (Transform Target in Targets)
    //    {
    //        Handles.DrawLine(player.transform.position, Target.position);
    //    }
    //}
}
