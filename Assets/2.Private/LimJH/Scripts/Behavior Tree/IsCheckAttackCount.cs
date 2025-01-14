using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsCheckAttackCount : BaseCondition
{
    //public SharedInt attackCount;
    //public SharedInt specialAttackCount;
    //public SharedBool isDelay;



    public override TaskStatus OnUpdate()
    {
        if (mob.Stat.isSpecialAttackDelay)
        {
            return TaskStatus.Failure;
        }

        // 공격 횟수가 필요 공격 횟수 이상일 때는 특수 공격을 수행
        return mob.attackCount < mob.specialAttackCount? TaskStatus.Success : TaskStatus.Failure;
    }
}