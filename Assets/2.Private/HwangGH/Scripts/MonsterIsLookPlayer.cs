using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MonsterIsLookPlayer : Conditional
{
	private PlayerController player;

	public override TaskStatus OnUpdate()
	{



		return TaskStatus.Success;
	}
}