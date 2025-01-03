using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BossJumpAttack : Action
{
	public SharedVector3 targetPos; // 플레이어 평면위치
	public SharedFloat height;


	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}