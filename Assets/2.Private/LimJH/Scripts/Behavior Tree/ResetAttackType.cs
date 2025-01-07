using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ResetAttackType : BaseAction
{
	public override TaskStatus OnUpdate()
	{
		mob.ResetSkillType();
		return TaskStatus.Success;
	}
}