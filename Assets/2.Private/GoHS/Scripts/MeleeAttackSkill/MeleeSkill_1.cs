using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkill_1 : BaseState
{
    public MeleeSkill_1(ProjectPlayer player) : base(player)
    {
    }

    [SerializeField] private float delay;
    [SerializeField] private float curDelay;
    private bool isStartSkill;

    public override void Enter()
    {
        Debug.Log("근접 스킬 1 시작!");
        curDelay = player.Setting.MeleeSkill1Setting.Delay;

    }

    public override void Update()
    {
        if (curDelay > 0f)
        {
            curDelay -= Time.deltaTime;

        }
        else
        {
            if (!isStartSkill)
            {
                isStartSkill = true;
                GetTarget();
                Debug.Log("원거리 스킬 2 활성화됨");
                curDelay = player.Setting.Skill2Setting.Delay;
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
        Collider[] TargetCollider = Physics.OverlapSphere(player.transform.position, player.Setting.MeleeSkill1Setting.ViewArea, player.Setting.MeleeSkill1Setting.TargetMask);

        for (int i = 0; i < TargetCollider.Length; i++) // 감지된 콜라이더 
        {
            Transform target = TargetCollider[i].transform;
            Vector3 direction = target.position - player.transform.position;

            if (Vector3.Dot(direction.normalized, player.transform.forward) > GetAngle(player.Setting.MeleeSkill1Setting.ViewAngle / 2).z)
            {
                Debug.Log(GetAngle(player.Setting.MeleeSkill1Setting.ViewAngle / 2).z);

                // TODO : 스킬 2번을 사용하였을때 IDamagable 인터페이스를 가지고 있는 몬스터를 확인해서 해당 컴포넌트를 가지고 있으면
                // 데미지를 입히는 방식으로 우선 구현
                IDamagable damagable = target.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    damagable.TakeHit(player.Setting.MeleeSkill1Setting.Damage, true);
                }

                // 플레이어의 3만큼(아마 변경 필요) 앞 방향으로 몬스터에게 위치 전달 
                IPushable pushable = target.GetComponent<IPushable>();
                if (pushable != null)
                {
                    pushable.Push(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 3f));
                }

            }
        }
    }

    public Vector3 GetAngle(float AngleInDegree)
    {
        return new Vector3(Mathf.Sin(AngleInDegree * Mathf.Deg2Rad), 0, Mathf.Cos(AngleInDegree * Mathf.Deg2Rad));
    }
}
