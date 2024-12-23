using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsWithindash : Conditional
{
    public SharedGameObject targetObject;   // 타겟 오브젝트
    public SharedGameObject selfObject;     // 자기 자신 오브젝트
    public float rangeThreshold = 10f;

    public override TaskStatus OnUpdate()
    {
        if (targetObject.Value == null || selfObject.Value == null)
        {
            return TaskStatus.Failure;
        }

        // 셀프와 타겟 간의 거리 계산
        float distance = Vector3.Distance(selfObject.Value.transform.position, targetObject.Value.transform.position);

        if (distance <= rangeThreshold)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}