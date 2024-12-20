using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class JumpAttack : Action
{
    public SharedGameObject targetObject; // 공격 대상
    public SharedFloat attackDamage;      // 공격 데미지
    public SharedFloat jumpHeight;        // 점프 높이
    public SharedFloat jumpDuration;      // 점프 지속 시간

    private float elapsedTime;            // 경과 시간
    private Vector3 startPosition;        // 점프 시작 위치
    private Vector3 targetPosition;       // 점프 도착 위치

    private EliteMonster1 eliteMonster;

    public override void OnStart()
    {
        eliteMonster = gameObject.GetComponent<EliteMonster1>();

        if (targetObject.Value == null)
        {
            Debug.LogWarning("타겟 오브젝트가 존재하지 않습니다.");
            return;
        }

        // 점프 초기화
        elapsedTime = 0f;
        startPosition = transform.position;

        // 목표 위치는 타겟 위치의 위쪽 (기본 점프 높이 적용)
        targetPosition = new Vector3(targetObject.Value.transform.position.x,
                                     targetObject.Value.transform.position.y + jumpHeight.Value,
                                     targetObject.Value.transform.position.z);

        // 점프 애니메이션 트리거 추가 가능
        Debug.Log("점프 시작!");

        // 점프 공격 불가능 상태로 설정
        if (eliteMonster != null)
        {
            eliteMonster.canJumpAttack = false; // 점프 공격 비활성화
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (targetObject.Value == null)
        {
            Debug.LogWarning("타겟 오브젝트가 존재하지 않습니다.");
            return TaskStatus.Failure;
        }

        // 점프 처리
        elapsedTime += Time.deltaTime;
        float progress = elapsedTime / jumpDuration.Value;

        // 점프 곡선 (포물선 형태)
        Vector3 currentPosition = Vector3.Lerp(startPosition, targetPosition, progress);
        currentPosition.y += Mathf.Sin(progress * Mathf.PI) * jumpHeight.Value;
        transform.position = currentPosition;

        // 점프 완료
        if (progress >= 1f)
        {
            // 공격 처리
            var player = targetObject.Value.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(attackDamage.Value);
                Debug.Log(attackDamage.Value + "의 데미지를 " + targetObject.Value.name + "에게 주었습니다.");
            }
            else
            {
                Debug.LogWarning("PlayerController 컴포넌트가 존재하지 않습니다.");
            }

            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        // 점프 및 공격 종료 작업
        Debug.Log("점프 공격 완료");

        // 점프 공격 쿨타임 시작
        if (eliteMonster != null)
        {
            eliteMonster.StartCoroutine(StartJumpAttackCooldown(eliteMonster));
        }
    }

    private System.Collections.IEnumerator StartJumpAttackCooldown(EliteMonster1 monster)
    {
        yield return new WaitForSeconds(monster.jumpAttackCoolTime); // 쿨타임 대기
        monster.canJumpAttack = true; // 점프 공격 가능 상태로 복구
        Debug.Log("점프 공격 쿨타임 종료");
    }
}