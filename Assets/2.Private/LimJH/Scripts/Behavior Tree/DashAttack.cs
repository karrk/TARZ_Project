using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Unity.VisualScripting;

public class DashAttack : Action
{
    public SharedGameObject targetObject;  // 공격 대상
    public SharedInt rushCount;
    public SharedInt attackCount;
    public SharedFloat dashSpeed;          // 돌진 속도
    public SharedFloat stopDistance;       // 돌진 완료 거리

    private Vector3 dashDirection;         // 돌진 방향
    private bool isDashing;                // 현재 돌진 중인지 확인
    private int initialRushCount;          // 초기 설정된 돌진 횟수

    private MidBossMonster midBossMonster;

    public SharedBool isDelay;

    public override void OnStart()
    {
        rushCount.Value = Random.Range(1, 4); // 1~3 랜덤 횟수 선택
        Debug.Log($"{rushCount.Value}번 돌진 실행");

        if(midBossMonster == null)
        {
            midBossMonster = gameObject.GetComponent<MidBossMonster>();
        }
        
        /*if (targetObject.Value != null)
        {
            // 타겟 방향 계산
            Vector3 targetPosition = targetObject.Value.transform.position;
            Vector3 currentPosition = transform.position;

            dashDirection = (targetPosition - currentPosition).normalized; // 방향 계산
            isDashing = true;

            if (rushCount.Value <= 0)
            {
                rushCount.Value = Random.Range(1, 4); // 1~3 랜덤 횟수 설정
            }

            initialRushCount = rushCount.Value;
            Debug.Log($"{initialRushCount}번 돌진 시작: 방향 - {dashDirection}");
        }
        else
        {
            Debug.LogError("TargetObject가 설정되지 않았습니다.");
            isDashing = false;
        }*/

    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
        /*if (!isDashing)
        {
            return TaskStatus.Failure; // 돌진 불가능한 상태
        }

        // 돌진 실행
        transform.position += dashDirection * dashSpeed.Value * Time.deltaTime;

        // 타겟과의 거리 계산
        float distanceToTarget = Vector3.Distance(transform.position, targetObject.Value.transform.position);

        if (distanceToTarget <= stopDistance.Value)
        {
            Debug.Log($"돌진 완료, 남은 돌진 횟수: {rushCount.Value - 1}");

            rushCount.Value--; // 돌진 횟수 감소
            if (rushCount.Value <= 0)
            {
                Debug.Log("모든 돌진 완료");
                isDashing = false; // 돌진 종료
                return TaskStatus.Success;
            }

            // 다음 돌진을 위해 방향 재계산
            Vector3 targetPosition = targetObject.Value.transform.position;
            Vector3 currentPosition = transform.position;
            dashDirection = (targetPosition - currentPosition).normalized; // 새로운 방향 계산
        }

        // 아직 돌진 중인 상태
        return TaskStatus.Running;*/
    }

    public override void OnEnd()
    {
        // 점프 및 공격 종료 작업
        Debug.Log("대쉬 특수 공격 완료");

        // 점프 공격 쿨타임 시작
        if (midBossMonster != null)
        {
            midBossMonster.StartCoroutine(StartDashAttackDelay(midBossMonster));
        }

        //attackCount.Value = 0;
    }

    private System.Collections.IEnumerator StartDashAttackDelay(MidBossMonster monster)
    {
        isDelay.Value = true;
        yield return new WaitForSeconds(monster.DashAttackDelay);
        attackCount.Value = 0;
        isDelay.Value = false;
    }
}