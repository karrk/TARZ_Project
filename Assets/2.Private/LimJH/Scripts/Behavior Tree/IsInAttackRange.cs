using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class IsInAttackRange : BaseCondition
{
    public override TaskStatus OnUpdate()
    {
        if (mob == null)
        {
            Debug.LogWarning("몬스터가 존재하지 않습니다!");
            return TaskStatus.Failure;
        }

        if (mob.Dist <= mob.Stat.InAttackRange) // InAttackRange
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}