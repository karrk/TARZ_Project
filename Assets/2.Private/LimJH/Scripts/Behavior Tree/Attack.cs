using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Attack : Action
{
    public SharedGameObject targetObject;
    public SharedFloat attackDamage;

    public override void OnStart()
    {
        if (targetObject.Value == null)
        {
            Debug.LogWarning("타겟 오브젝트가 존재하지 않습니다.");
            return;
        }

        // 공격 초기화 작업 (예: 애니메이션 트리거)
    }

    public override TaskStatus OnUpdate()
    {
        if (targetObject.Value == null)
        {
            Debug.LogWarning("타겟 오브젝트가 존재하지 않습니다.");
            return TaskStatus.Failure;
        }

        //대상의 baseMonster컴포넌트에 데미지 전달
        var player = targetObject.Value.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(attackDamage.Value);
            Debug.Log(attackDamage.Value + "의 데미지를 " + targetObject.Value.name + "에게 주었습니다.");
        }
        else
        {
            Debug.LogWarning("컴포넌트가 존재하지 않습니다.");
        }

        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        // 공격 종료 작업 (예: 애니메이션 정리)
    }
}