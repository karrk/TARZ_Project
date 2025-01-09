using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsInJumpAttack : BaseCondition
{

    public override TaskStatus OnUpdate()
    {
        if (mob == null)
        {
            Debug.LogWarning("몬스터가 존재하지 않습니다!");
            return TaskStatus.Failure;
        }

        /*float distance =
            Vector3.Distance(selfObject.Value.transform.position,
            targetObject.Value.transform.position);*/

        //Debug.Log(distance);
        
        if (mob.Dist <= mob.Stat.InAttackRange && mob.Stat.canJumpAttack)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}