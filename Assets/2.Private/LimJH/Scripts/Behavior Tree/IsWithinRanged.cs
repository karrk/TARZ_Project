using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsWithinRanged : Conditional
{
    public SharedGameObject targetObject;   // 타겟 오브젝트
    public SharedGameObject selfObject;     // 자기 자신 오브젝트
    public float ranged = 10f;      // 원거리 공격 범위

    public override TaskStatus OnUpdate()
    {
        if (targetObject.Value == null || selfObject.Value == null)
        {
            return TaskStatus.Failure;
        }

        // 셀프와 타겟 간의 거리 계산
        float distance = Vector3.Distance(selfObject.Value.transform.position, targetObject.Value.transform.position);

        if (distance > ranged)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}