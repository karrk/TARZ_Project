using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanAttackHGH : Conditional
{
	public SharedBool summonLightning;
	public SharedFloat lightningTimer;
	public float n2;
	public override TaskStatus OnUpdate()
	{
		if(summonLightning.Value == true && lightningTimer.Value >= n2)
            return TaskStatus.Success;

        return TaskStatus.Failure;

    }
}