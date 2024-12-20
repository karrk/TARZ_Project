using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Idle : Action
{
    public override TaskStatus OnUpdate()
    {
        Debug.Log("Idle 상태입니다.");
        return TaskStatus.Running;
    }
}