using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Knockdown : Action
{
	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		//Push와 유사한 형태 Push 처럼 밀리며 애니메이션 진행 후 스턴(?)

		return TaskStatus.Success;
	}
}