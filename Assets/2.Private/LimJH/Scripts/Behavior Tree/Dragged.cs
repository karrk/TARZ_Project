using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Dragged : Action
{
    public SharedGameObject targetObject;   // 몬스터를 모을 포인트
    public float gatherSpeed = 2f;          // 모이는 속도
    public float gatherRadius = 1f;         // 목표 지점에 도달했다고 간주하는 거리

    public override TaskStatus OnUpdate()
    {
        if (targetObject.Value == null)
        {
            return TaskStatus.Failure;
        }

        // 목표 지점으로 이동
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetObject.Value.transform.position,
            gatherSpeed * Time.deltaTime
        );

        // 목표 지점에 도달했는지 확인
        if (Vector3.Distance(transform.position, targetObject.Value.transform.position) <= gatherRadius)
        {
            return TaskStatus.Success; // 목표 지점에 도달하면 성공
        }

        return TaskStatus.Running; // 목표 지점으로 이동 중
    }
}