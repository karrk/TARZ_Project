using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsWithinRanged : BaseCondition
{
    //public SharedGameObject targetObject;   // 타겟 오브젝트
    //public SharedGameObject selfObject;     // 자기 자신 오브젝트
    //public float ranged = 10f;      // 원거리 공격 범위

    public override TaskStatus OnUpdate()
    {
        if (mob.player == null || mob == null)
        {
            return TaskStatus.Failure;
        }

        // 셀프와 타겟 간의 거리 계산
        float distance = Vector3.Distance(mob.transform.position, mob.PlayerPos);

        if (distance > mob.Stat.AttackRange)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}