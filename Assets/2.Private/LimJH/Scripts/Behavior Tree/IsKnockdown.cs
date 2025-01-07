using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsKnockdown : BaseCondition
{
    public override TaskStatus OnUpdate()
    {
        return mob.SkillType == E_SkillType.LongRangeSkill5 ? 
            TaskStatus.Success : TaskStatus.Failure;
    }
}