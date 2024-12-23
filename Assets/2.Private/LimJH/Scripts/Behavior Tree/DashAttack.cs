using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class DashAttack : Action
{
    public SharedInt rushCount;
    public SharedInt attackCount;

    public override void OnStart()
    {
        rushCount.Value = Random.Range(1, 4); // 1~3 랜덤 횟수 선택
        Debug.Log($"{rushCount.Value}번 돌진 실행");
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        // 공격 횟수 초기화
        //attackCount.Value = 0;
    }
}