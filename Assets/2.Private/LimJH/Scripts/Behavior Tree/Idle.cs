using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Idle : Action
{
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Running;
    }
}