using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Timers;
using TMPro;
using System.Collections;

public class BossJumpAttack : Action
{
    public BaseBossMonster bossmonster;
    public BossJumpAttack bossJumpAttack;

    public SharedGameObject targetObject; // 공격 대상
    public SharedFloat attackDamage;      // 공격 데미지
    public SharedFloat jumpHeight;        // 점프 높이
    public SharedFloat jumpDuration;      // 점프 지속 시간
    public bool bossJumpOnOff;
    
    private float elapsedTime;            // 경과 시간
    private Vector3 startPosition;        // 점프 시작 위치
    private Vector3 targetPosition;       // 점프 도착 위치

    public override void OnAwake()
    {
		targetObject = GameObject.FindGameObjectWithTag("Player");
    }

    public override void OnStart()
	{
        if (targetObject == null)
			return;

        elapsedTime = 0;
        jumpHeight = 0;
        bossJumpOnOff = false;

        targetPosition = new Vector3(targetObject.Value.transform.position.x,
                                     targetObject.Value.transform.position.y + jumpHeight.Value,
                                     targetObject.Value.transform.position.z);

        // 점프 애니메이션 트리거 추가 가능
        Debug.Log("점프 시작!");



    }

	public override TaskStatus OnUpdate()
	{
        if (targetObject.Value == null)
        {
            Debug.LogWarning("타겟 오브젝트가 존재하지 않습니다.");
            return TaskStatus.Failure;
        }

        bossJumpOnOff = true;

        if (bossJumpOnOff == true)
        {
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
                var player = targetObject.Value.GetComponent<ProjectPlayer>();
                if (player != null)
                {
                    player.TakeDamage(attackDamage.Value);
                    Debug.Log(attackDamage.Value + "의 데미지를 " + targetObject.Value.name + "에게 주었습니다.");
                }
                else
                {
                    Debug.LogWarning("PlayerController 컴포넌트가 존재하지 않습니다.");
                }
                bossJumpOnOff = false;
                if (JumpAttackCool == null)
                {
                    JumpAttackCool = StartCoroutine(CoolTimeJumpAttack());
                }
                
                return TaskStatus.Success;
            }
        }

        return TaskStatus.Running;
    }
    private Coroutine JumpAttackCool;
    private System.Collections.IEnumerator CoolTimeJumpAttack()
    {
        yield return new WaitForSeconds(bossmonster.bossMonsterJumpAttackCool); // 쿨타임 대기
        bossJumpAttack.bossJumpOnOff = true; // 점프 공격 가능 상태로 복구
        Debug.Log("점프 공격 쿨타임 종료");
    }

}