using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class PlayDieEffect : BaseAction
{
	public override TaskStatus OnUpdate()
	{
        GameObject.Instantiate(bossMob.bossMobDie, transform.position, Quaternion.identity);

        return TaskStatus.Success;
	}
}