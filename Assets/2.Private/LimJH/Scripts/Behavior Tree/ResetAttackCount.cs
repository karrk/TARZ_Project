using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ResetAttackCount : Action
{
    public SharedInt attackCount;

    public override void OnStart()
    {
        Debug.Log("공격 횟수 초기화 시작");
    }

    public override TaskStatus OnUpdate()
    {
        // 공격 횟수 초기화
        attackCount.Value = 0;
        Debug.Log("공격 횟수 초기화 완료");

        return TaskStatus.Success;
    }
}