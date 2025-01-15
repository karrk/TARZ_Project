using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

public class DashAttack : BaseAction
{
    //public SharedGameObject targetObject;  // 공격 대상
    //public SharedInt rushCount;
    //public SharedInt attackCount;
    //public SharedFloat dashSpeed;          // 돌진 속도
    //public SharedFloat stopDistance;       // 돌진 완료 거리

    private Vector3 dashDirection;         // 돌진 방향
    private bool isDashing;                // 현재 돌진 중인지 확인

    public override void OnStart()
    {
        base.OnStart();

        mob.Stat.rushCount = Random.Range(1, 4); // 1~3 랜덤 횟수 선택

        // 돌진 중 상태 초기화
        isDashing = false;
    }

    public override TaskStatus OnUpdate()
    {
        if (mob.player == null)
        {
            return TaskStatus.Failure; // 타겟이 없으면 실패로 처리
        }

        if (!isDashing)
        {
            StartDash();
        }

        if (isDashing)
        {
            PerformDash();
        }

        return TaskStatus.Running;
    }

    private void StartDash()
    {
        // 현재 위치와 타겟 위치를 기반으로 방향 설정
        dashDirection = (mob.PlayerPos - transform.position).normalized;
        isDashing = true;
    }

    private void PerformDash()
    {
        // 타겟 지점까지 이동
        Vector3 newPosition = transform.position + dashDirection * mob.Stat.dashSpeed * Time.deltaTime;
        transform.position = newPosition;

        // 목표 지점에 도달했는지 확인
        float distanceToTarget = Vector3.Distance(transform.position, mob.PlayerPos);

        if (distanceToTarget <= mob.Stat.stopDistance)
        {
            isDashing = false; // 돌진 완료
        }
    }

    public override void OnEnd()
    {
        base.OnEnd();
        isDashing = false; // 상태 초기화
    }
}