using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class IsInAttackRange : BaseCondition
{
    public override TaskStatus OnUpdate()
    {
        if (mob == null)
        {
            Debug.LogWarning("SelfObject or TargetObject is null!");
            return TaskStatus.Failure;
        }

        if (mob.Dist <= mob.Stat.InAttackRange) // InAttackRange
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}