using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class MonsterRotate : BaseCondition
{
    private float elapsedTime = 0f;
    private Vector3 directionToTarget;
    private Quaternion targetRotation;
    private float angleToTarget;

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

        // 목표 방향 계산
        directionToTarget = (mob.PlayerPos - transform.position).normalized;
        directionToTarget.y = 0; // 수평 방향만 회전하도록 설정

        // 회전 수행
        targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = 
            Quaternion.Slerp(transform.rotation, targetRotation, mob.Stat.RotateSpeed * Time.deltaTime);

        // 목표를 충분히 바라보고 있으면 성공 반환
        angleToTarget = Vector3.Angle(transform.forward, directionToTarget);
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