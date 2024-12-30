using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Unity.VisualScripting;

public class IsCheckAttackCountS : Conditional
{
    public SharedInt attackCount;
    public SharedInt specialAttackCount;

    public SharedBool isDelay;

    public override TaskStatus OnUpdate()
    {
        // 쿨타임 중이면 실패 반환
        if (isDelay.Value)
        {
            return TaskStatus.Failure;
        }

        // 공격 횟수가 필요 공격 횟수 이상일 때는 특수 공격을 수행
        return attackCount.Value >= specialAttackCount.Value? TaskStatus.Success : TaskStatus.Failure;
    }


}