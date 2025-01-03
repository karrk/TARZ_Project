using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ResetAttackType : Action
{
    public SharedInt skillType;

    public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{

		skillType.Value = 0;

		return TaskStatus.Success;
	}
}