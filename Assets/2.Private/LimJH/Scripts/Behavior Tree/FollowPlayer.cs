using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class FollowPlayer : Action
{
    public SharedGameObject selfObject; // 몬스터 오브젝트
    public SharedGameObject targetObject; // 추적 대상 (Behavior Designer에서 설정 가능)
    public SharedFloat speed; // 이동 속도
    public SharedFloat stoppingDistance;

    private NavMeshAgent agent;

    public override void OnStart()
    {
        // NavMeshAgent 컴포넌트를 가져옴
        if (selfObject.Value != null)
        {
            agent = selfObject.Value.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.stoppingDistance = stoppingDistance.Value;
                agent.speed = speed.Value; // 이동 속도 설정
            }
            else
            {
                Debug.LogError($"{selfObject.Value.name}에 NavMeshAgent가 없습니다.");
            }
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (selfObject.Value == null || targetObject.Value == null)
        {
            Debug.LogWarning("SelfObject or TargetObject is null!");
            return TaskStatus.Failure;
        }

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent를 찾을 수 없습니다.");
            return TaskStatus.Failure;
        }

        // 대상 위치 설정
        agent.SetDestination(targetObject.Value.transform.position);

        // 이동 완료 여부 확인
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 0.1f)
        {
            return TaskStatus.Success; // 목표 지점에 도달
        }

        return TaskStatus.Running; // 아직 이동 중
    }
}