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

    /*private async UniTask Rush(int count)
    {
        for(int i = 0; i < count; i++)
    }*/

    public override TaskStatus OnUpdate()
    {
        if (mob.player == null)
        {
            return TaskStatus.Failure; // 타겟이 없으면 실패로 처리
        }

        if (!isDashing)
        {
            if (mob.Stat.rushCount <= 0)
            {
                return TaskStatus.Success; // 모든 대시를 완료했으면 성공 반환
            }
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
        Debug.Log("방향설정");
        // 현재 위치와 타겟 위치를 기반으로 방향 설정
        dashDirection = (mob.PlayerPos - transform.position).normalized;
        isDashing = true;
    }

    private void PerformDash()
    {
        Debug.Log("대쉬진행");
        // 타겟 지점까지 이동
        Vector3 newPosition = transform.position + dashDirection * mob.Stat.dashSpeed;
        transform.position = newPosition;

        // 목표 지점에 도달했는지 확인
        float distanceToTarget = Vector3.Distance(transform.position, mob.PlayerPos);

        if (distanceToTarget <= mob.Stat.stopDistanceDash)
        {
            isDashing = false; // 돌진 완료
            mob.Stat.rushCount--;
        }
    }

    public override void OnEnd()
    {
        base.OnEnd();
        isDashing = false; // 상태 초기화
    }
}