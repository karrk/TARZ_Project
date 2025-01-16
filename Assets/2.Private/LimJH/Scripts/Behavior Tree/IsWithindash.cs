using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsWithinDash : BaseCondition
{
    public override TaskStatus OnUpdate()
    {
        if (mob.player == null || mob == null)
        {
            return TaskStatus.Failure;
        }

        // 셀프와 타겟 간의 거리 계산
        float distance = Vector3.Distance(mob.transform.position, mob.PlayerPos);

        if (distance <= 20f)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}