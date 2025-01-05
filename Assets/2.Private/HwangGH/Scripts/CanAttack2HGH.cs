using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Unity.VisualScripting;

public class CanAttack2HGH : Conditional
{
	public SharedTransform myselfPos;
	public SharedTransform targetPos;
	public SharedFloat distance;
	public SharedFloat DetectionRange;
    public BaseBossMonster bossMonster;

    public override void OnAwake()
    {
        myselfPos.Value = GameObject.FindGameObjectWithTag("Monster").transform;
        targetPos.Value = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override TaskStatus OnUpdate()
	{
        distance.Value = Vector3.Distance(myselfPos.Value.position, targetPos.Value.transform.position);
		if(distance.Value <= DetectionRange.Value)
            return TaskStatus.Success;

		return TaskStatus.Failure;
    }
}