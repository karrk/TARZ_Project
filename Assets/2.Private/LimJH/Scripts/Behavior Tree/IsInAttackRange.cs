using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsInAttackRange : Conditional
{
    public SharedGameObject selfObject;
    public SharedGameObject targetObject;
    public SharedFloat attackRange;

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

        Debug.Log(distance);

        if (distance <= attackRange.Value)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}