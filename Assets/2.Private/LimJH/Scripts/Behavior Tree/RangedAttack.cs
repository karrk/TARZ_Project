using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class rangedAttack : Action
{
    public SharedInt attackCount;

    public override void OnStart()
    {
        Debug.Log("원거리공격 테스트");
        // 원거리 공격 로직 추가
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        // 공격 횟수 초기화
        attackCount.Value = 0;
    }
}