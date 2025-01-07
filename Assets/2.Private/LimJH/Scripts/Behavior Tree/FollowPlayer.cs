using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class FollowPlayer : BaseCondition
{
    public override void OnStart()
    {
        base.OnStart();

        // NavMeshAgent 컴포넌트를 가져옴
        if (mob.Reference.Nav != null)
        {
            mob.Reference.Anim.SetBool("isMove", true);

            mob.Reference.Nav.isStopped = false;
            mob.Reference.Nav.stoppingDistance = mob.Stat.StopDist;
            mob.Reference.Nav.speed = mob.Stat.MoveSpeed;
        }
        else
        {
            Debug.Log("Nav 가 없음");
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (mob == null)
        {
            Debug.LogWarning("SelfObject or TargetObject is null!");
            return TaskStatus.Failure;
        }

        if (mob.Reference.Nav == null)
        {
            Debug.LogError("NavMeshAgent를 찾을 수 없습니다.");
            return TaskStatus.Failure;
        }

        // 대상 위치 설정
        mob.Reference.Nav.SetDestination(mob.PlayerPos);

        // 이동 완료 여부 확인
        if (!mob.Reference.Nav.pathPending &&
            mob.Reference.Nav.remainingDistance <= mob.Reference.Nav.stoppingDistance + 0.1f)
        {
            return TaskStatus.Success; // 목표 지점에 도달
        }

        return TaskStatus.Running; // 아직 이동 중
    }

    public override void OnEnd()
    {
        mob.Reference.Anim.SetBool("isMove", false);

        if (mob.Reference.Nav != null)
        {
            mob.Reference.Nav.isStopped = true;  // NavMeshAgent 멈추기
            mob.Reference.Nav.velocity = Vector3.zero;  // 이동 속도 초기화
        }
    }
}