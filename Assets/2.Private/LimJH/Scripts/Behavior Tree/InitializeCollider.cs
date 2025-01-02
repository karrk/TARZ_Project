using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class InitializeCollider : Action
{
    public SharedGameObject selfObject;

	public override TaskStatus OnUpdate()
	{
        if (selfObject != null && selfObject.Value != null)
        {

            // Collider 비활성화
            Collider collider = selfObject.Value.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            return TaskStatus.Success;
        }
        return TaskStatus.Running;
	}
}