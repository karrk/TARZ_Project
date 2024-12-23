using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsDead : Conditional
{
    public SharedInt health; // 몬스터의 체력
    public override TaskStatus OnUpdate()
    {
        // 체력이 0 이하일 경우 Success 반환
        if (health.Value <= 0)
        {
            return TaskStatus.Success; // 체력이 0 이하일 때만 성공으로 처리
        }

        return TaskStatus.Failure; // 그 외에는 실패로 처리
    }
}