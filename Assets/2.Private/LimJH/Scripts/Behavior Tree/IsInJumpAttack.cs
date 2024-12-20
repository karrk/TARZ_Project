using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsInJumpAttack : Conditional
{
    public SharedGameObject selfObject;
    public SharedGameObject targetObject;
    public SharedFloat detectRange;
    public SharedBool canJumpAttack;

    public override TaskStatus OnUpdate()
    {
        if (selfObject.Value == null || targetObject.Value == null)
        {
            Debug.LogWarning("SelfObject or TargetObject is null!");
            return TaskStatus.Failure;
        }
        float distance =
            Vector3.Distance(selfObject.Value.transform.position,
            targetObject.Value.transform.position);

        //Debug.Log(distance);
        
        if (distance <= detectRange.Value && canJumpAttack.Value)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}