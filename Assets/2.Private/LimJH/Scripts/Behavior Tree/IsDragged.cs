using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsDragged : BaseCondition
{
    private bool Test = false;

    public override TaskStatus OnUpdate()
    {
        E_SkillType type = mob.SkillType;

        if(type == E_SkillType.MeleeSkill1 /*&& Test == false*/)
        {
            //Test = true;
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }

        return mob.SkillType == E_SkillType.MeleeSkill1 
            ? TaskStatus.Success : TaskStatus.Failure;
    }
}