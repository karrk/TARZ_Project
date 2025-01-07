using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsPush : BaseCondition
{
    public override TaskStatus OnUpdate()
    {
        return mob.SkillType == E_SkillType.MeleeSkill2 ? 
            TaskStatus.Success : TaskStatus.Failure;
    }
}