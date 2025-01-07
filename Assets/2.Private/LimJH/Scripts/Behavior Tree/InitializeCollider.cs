using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class InitializeCollider : BaseCondition
{
    private Collider coll;

    public override void OnStart()
    {
        base.OnStart();
        coll = mob.GetComponent<Collider>();
    }

    public override TaskStatus OnUpdate()
	{
        if (mob != null)
        {
            coll.enabled = false;

            return TaskStatus.Success;
        }
        return TaskStatus.Running;
	}
}