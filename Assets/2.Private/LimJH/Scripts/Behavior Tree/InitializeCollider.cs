using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class InitializeCollider : BaseAction
{
    public override TaskStatus OnUpdate()
    {
        if (mob != null)
        {
            mob.Reference.Coll.enabled = false;

            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}