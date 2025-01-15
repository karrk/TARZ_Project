using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ResetAttackCount : BaseAction
{
    public override TaskStatus OnUpdate()
    {
        // 공격 횟수 초기화
        mob.attackCount = 0;
        // Debug.Log("공격 횟수 초기화 완료");

        return TaskStatus.Success;
    }
}