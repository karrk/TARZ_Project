using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsGimmickStart : Conditional
{
    public SharedFloat health;          // 몬스터의 현재 체력
    public SharedBool isGimmickActive;  // 기믹 활성화 상태 플래그
    public SharedFloat halfHealth;

    public override TaskStatus OnUpdate()
    {
        // 체력이 절반 이하이고, 기믹이 활성화되지 않았다면 Success 반환
        if (health.Value <= halfHealth.Value && !isGimmickActive.Value)
        {
            Debug.Log("체력절반");
            return TaskStatus.Success;
        }

        // 조건 미충족 시 Failure 반환
        return TaskStatus.Failure;
    }
}