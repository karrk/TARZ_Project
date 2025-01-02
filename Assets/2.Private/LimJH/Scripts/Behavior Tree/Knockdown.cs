using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Knockdown : Action
{
    public SharedGameObject targetObject;
    public SharedVector3 playerVector;
    public SharedInt skillType;

    public float pushDistance = 5f;         // 밀려날 거리
    public float pushSpeed = 2f;            // 밀려나는 속도

    private Vector3 targetPosition;         // 몬스터가 밀려날 목표 위치

    public override void OnStart()
    {

        // 플레이어 위치와 몬스터 위치를 기준으로 방향 계산
        Vector3 monsterPosition = transform.position;//selfObject로 수정(?)
        Vector3 direction = (monsterPosition - playerVector.Value).normalized;
        //Vector3 direction = (monsterPosition - targetObject.Value.transform.position).normalized;

        // 목표 위치 계산
        targetPosition = monsterPosition + direction * pushDistance;
    }

    public override TaskStatus OnUpdate()
    {
        if (targetObject.Value == null)
        {
            return TaskStatus.Failure;
        }

        // 몬스터를 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, pushSpeed * Time.deltaTime);

        // 목표 지점에 도달했는지 확인
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            skillType.Value = 0;
            return TaskStatus.Success;
        }

        return TaskStatus.Running; // 목표 위치에 도달할 때까지 실행
    }
}