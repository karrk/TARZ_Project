using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class LongRangeSkill_2 : BaseState
{
    [SerializeField] private ProjectPlayer player;
    [SerializeField] private float skill_Str;       // 스킬 공격력. 현재 기획서상 200 데미지

    public LongRangeSkill_2(ProjectPlayer player)
    {
        this.player = player;
        this.viewArea = 8f;             // 사정거리 우선 8f
        this.viewAngle = 60f;           // 우선 60도
        this.targetMask = LayerMask.GetMask("Monster");

        this.delay = 0.5f;
    }

    // 판정 범위 거리
    [SerializeField] private float viewArea;

    // 판정 범위 각도
    [Range(0, 360)]
    [SerializeField] private float viewAngle;

    // 충돌 감지 대상
    [SerializeField] private LayerMask targetMask;

    // 충돌 감지된 오브젝트들을 담는 리스트
    [HideInInspector]
    [SerializeField] private List<Transform> Targets = new List<Transform>();   // 감지된 오브젝트를 담아두는 배열. 현재로선 사용안되도 됨


    [SerializeField] private float delay;
    [SerializeField] private float curDelay;
    private bool isStartSkill;

    public override void Enter()
    {
        Debug.Log("스킬 2 시전 시작!");
        curDelay = delay;
    }

    public override void Update()
    {
        if (curDelay > 0f)
        {
            curDelay -= Time.deltaTime;

        }
        else
        {
            if(!isStartSkill)
            {
                isStartSkill = true;
                GetTarget();
                Debug.Log("원거리 스킬 2 활성화됨");
                curDelay = delay;
            }
            else
            {
                Debug.Log("딜레이 다 지나감");
                player.ChangeState(E_State.Idle);
            }
        }
    }

    public override void Exit()
    {
        isStartSkill = false;
    }

    public void GetTarget()
    {
        Targets.Clear();    // 배열 초기화
        Collider[] TargetCollider = Physics.OverlapSphere(player.transform.position, viewArea, targetMask);

        for (int i = 0; i < TargetCollider.Length; i++) // 감지된 콜라이더 
        {
            Transform target = TargetCollider[i].transform;
            Vector3 direction = target.position - player.transform.position;

            if (Vector3.Dot(direction.normalized, player.transform.forward) > GetAngle(viewAngle / 2).z)
            {
                Debug.Log(GetAngle(viewAngle / 2).z);
                Targets.Add(target);

                // TODO : 스킬 2번을 사용하였을때 IDamagable 인터페이스를 가지고 있는 몬스터를 확인해서 해당 컴포넌트를 가지고 있으면
                // 데미지를 입히는 방식으로 우선 구현
                IDamagable damagable = target.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    damagable.TakeHit(skill_Str, true);
                }

            }
        }
    }

    public Vector3 GetAngle(float AngleInDegree)
    {
        return new Vector3(Mathf.Sin(AngleInDegree * Mathf.Deg2Rad), 0, Mathf.Cos(AngleInDegree * Mathf.Deg2Rad));
    }
}
