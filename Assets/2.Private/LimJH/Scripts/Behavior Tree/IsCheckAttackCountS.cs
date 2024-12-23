using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsCheckAttackCountS : Conditional
{
    public SharedInt attackCount;
    public SharedInt specialAttackCount;

    public override TaskStatus OnUpdate()
    {
        // 공격 횟수가 필요 공격 횟수 이상일 때는 특수 공격을 수행
        return attackCount.Value >= specialAttackCount.Value? TaskStatus.Success : TaskStatus.Failure;
    }
}