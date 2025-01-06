using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsDead : BaseCondition
{
    public override TaskStatus OnUpdate()
    {
        // 체력이 0 이하일 경우 Success 반환
        if (mob.health <= 0)
        {
            return TaskStatus.Success; // 체력이 0 이하일 때만 성공으로 처리
        }

        return TaskStatus.Failure; // 그 외에는 실패로 처리
    }
}