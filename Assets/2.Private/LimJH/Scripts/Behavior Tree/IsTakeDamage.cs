using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsTakeDamage : Conditional
{
	public SharedGameObject selfObject;
	public SharedFloat health;
	private float curHealth;

    public override void OnStart()
    {
		curHealth = health.Value;
    }

    public override TaskStatus OnUpdate()
	{

		if (curHealth != health.Value)
		{
            curHealth = health.Value;
            return TaskStatus.Success;
        }

		return TaskStatus.Failure;
	}
}