using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Attack : Action
{
    public SharedGameObject selfObject;
    public SharedGameObject targetObject;
    public SharedFloat attackDamage;
    public SharedInt attackCount;

    public float attackRange;
    public float angle;

    private Animator animator; // Animator 컴포넌트

    public override void OnStart()
    {
        if (targetObject.Value == null)
        {
            Debug.LogWarning("타겟 오브젝트가 존재하지 않습니다.");
            return;
        }

        // Animator 컴포넌트 가져오기
        if (selfObject.Value != null)
        {
            animator = selfObject.Value.GetComponent<Animator>();
        }

        animator.SetBool("isAttack", true);
    }

    public override TaskStatus OnUpdate()
    {
        if (targetObject.Value == null)
        {
            Debug.LogWarning("타겟 오브젝트가 존재하지 않습니다.");
            return TaskStatus.Failure;
        }

        //대상의 baseMonster컴포넌트에 데미지 전달
        var player = targetObject.Value.GetComponent<ProjectPlayer>();
        if (player != null && GetAngleHit(targetObject.Value.transform))
        {
            if(attackCount.Value!=-1) 
            {
                player.TakeDamage(attackDamage.Value);
                Debug.Log(attackDamage.Value + "의 데미지를 " + targetObject.Value.name + "에게 주었습니다.");
                //attackCount.Value++;
            }
            attackCount.Value++;
        }
        else
        {
            Debug.LogWarning("컴포넌트가 존재하지 않습니다.");
        }

        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        // 공격 종료 작업
        if (animator != null)
        {
            animator.SetBool("isMove", false);
        }
    }

    public bool GetAngleHit(Transform target)
    {
        // 내각 및 거리 계산
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

        if (angleToTarget < angle / 2) // 내각 체크
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= attackRange) // 거리 체크
            {
                Debug.DrawRay(transform.position + Vector3.up, directionToTarget * distanceToTarget, Color.red, 1.0f);
                return true; // 타격 성공
            }
        }

        return false; // 타격 실패
    }
}