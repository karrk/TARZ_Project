using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsTakeDamage : BaseCondition
{
    public override TaskStatus OnUpdate()
	{
		if (mob.IsOnDamaged == true)
		{
			mob.ResetDamageState();
            return TaskStatus.Success;
        }

		return TaskStatus.Failure;
	}
}