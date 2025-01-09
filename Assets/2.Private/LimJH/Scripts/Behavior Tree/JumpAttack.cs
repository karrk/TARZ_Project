using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class JumpAttack : BaseAction
{
    private float elapsedTime;            // 경과 시간
    private Vector3 startPosition;        // 점프 시작 위치
    private Vector3 targetPosition;       // 점프 도착 위치

    public override void OnStart()
    {
        base.OnStart();

        if (mob == null)
        {
            Debug.LogWarning("몬스터가 존재하지 않습니다.");
            return;
        }

        // 점프 초기화
        elapsedTime = 0f;
        startPosition = transform.position;

        // 목표 위치는 타겟 위치의 위쪽 (기본 점프 높이 적용)
        // targetPosition = new Vector3(transform.position.x,
        //                              transform.position.y + mob.Stat.jumpHeight,
        //                              mob.PlayerPos.z);

                                     targetPosition = mob.PlayerPos;

        // 점프 애니메이션 트리거 추가 가능
        Debug.Log("점프 시작!");

        // 점프 공격 불가능 상태로 설정
        if (mob != null)
        {
            mob.Stat.canJumpAttack = false; // 점프 공격 비활성화
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (mob == null)
        {
            Debug.LogWarning("몬스터가 존재하지 않습니다.");
            return TaskStatus.Failure;
        }

        // 점프 처리
        elapsedTime += Time.deltaTime;
        float progress = elapsedTime / mob.Stat.jumpDuration;

        // 점프 곡선 (포물선 형태)
        Vector3 currentPosition = Vector3.Lerp(startPosition, targetPosition, progress);
        currentPosition.y += Mathf.Sin(progress * Mathf.PI) * mob.Stat.jumpHeight;
        transform.position = currentPosition;

        // 점프 완료
        if (progress >= 1f)
        {
            //공격처리

            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        // 점프 및 공격 종료 작업
        Debug.Log("점프 공격 완료");

        // 점프 공격 쿨타임 시작
        if (jumpMob != null)
        {
            jumpMob.StartCoroutine(StartJumpAttackCooldown(jumpMob));
        }
    }

    private System.Collections.IEnumerator StartJumpAttackCooldown(EliteMonster1 monster)
    {
        yield return new WaitForSeconds(monster.jumpAttackCoolTime); // 쿨타임 대기
        mob.Stat.canJumpAttack = true; // 점프 공격 가능 상태로 복구
        Debug.Log("점프 공격 쿨타임 종료");
    }
}