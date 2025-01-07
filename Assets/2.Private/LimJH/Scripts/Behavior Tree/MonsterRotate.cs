using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MonsterRotate : BaseAction
{
    //public SharedGameObject selfObject;
    //public SharedGameObject targetObject;
    //public float rotationSpeed = 5f;
    //public float maxRotationTime = 2f; // 최대 회전 시간

    private float elapsedTime = 0f;

    public override void OnStart()
    {
        base.OnStart();
        elapsedTime = 0f; // 타이머 초기화
    }

    public override TaskStatus OnUpdate()
    {
        if (mob == null)
        {
            Debug.LogWarning("Self or Target 오브젝트가 없습니다.");
            return TaskStatus.Failure;
        }

        // 현재 위치와 목표 위치
        //Transform selfTransform = selfObject.Value.transform;
        //Transform targetTransform = targetObject.Value.transform;

        // 목표 방향 계산
        Vector3 directionToTarget = (mob.PlayerPos - transform.position).normalized;
        directionToTarget.y = 0; // 수평 방향만 회전하도록 설정

        // 회전 수행
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation =
            Quaternion.Slerp(transform.rotation, targetRotation, mob.Stat.RotateSpeed * Time.deltaTime);

        // 목표를 충분히 바라보고 있으면 성공 반환
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);
        if (angleToTarget < 5f) // 5도 이하로 차이가 나면 성공
        {
            return TaskStatus.Success;
        }

        // 시간 초과 체크
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= mob.Stat.MaxRotateTime)
        {
            Debug.Log("회전 시간 테스트");
            return TaskStatus.Success;
        }

        return TaskStatus.Running; // 계속 회전 중
    }
}