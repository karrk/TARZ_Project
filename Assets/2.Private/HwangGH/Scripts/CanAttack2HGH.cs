using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanAttack2HGH : Conditional
{
	public SharedTransform myselfPos;
	public SharedGameObject targetPos;
	public SharedFloat distance;
	public SharedFloat DetectionRange;

    public override TaskStatus OnUpdate()
	{
        distance.Value = Vector3.Distance(myselfPos.Value.position, targetPos.Value.transform.position);
		if(distance.Value <= DetectionRange.Value)
            return TaskStatus.Success;

		return TaskStatus.Failure;
    }
}