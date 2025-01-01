using UnityEngine;
using BehaviorDesigner.Runtime;
using Zenject;

public class BaseMonster : MonoBehaviour, IDamagable
{
    public float health;
    public float damageReducation = 1f;
    public float damage;
    public int attackCount;

    public float angle;
    public float attackRange;

    public BehaviorTree behaviorTree;

    private LayerMask garbageLayer;

    [Inject] ProjectPlayer player;
    [Inject] SkillManager SkillManager;

    private void Awake()
    {
        behaviorTree = GetComponent<BehaviorTree>(); // BehaviorTree 컴포넌트를 찾음
    }

    private void OnEnable()
    {
        if (behaviorTree != null)
        {
            behaviorTree.SetVariableValue("selfObject", this.gameObject);

            // "targetObject"에 플레이어 설정
            behaviorTree.SetVariableValue("targetObject", player.gameObject);
        }
        else
        {
            Debug.Log("비트리 없음");
        }
    }

    //public void TakeDamage(float damage)
    //{
    //    health -= (damage * damageReducation);
    //    Debug.Log($"Health: {health}");
    //    SkillManager.UpdateGauge();
    //}

    //public void TakeDamageNotFillGauge(float damage)
    //{
    //    health -= (damage * damageReducation);
    //    Debug.Log($"Health: {health}");
    //}


    protected virtual void Update()
    {
        if (behaviorTree != null && behaviorTree.GetVariable("health") != null)
        {
            behaviorTree.SetVariableValue("health", health);
        }
        if (behaviorTree.GetVariable("attackCount") != null)
        {
            behaviorTree.SetVariableValue("attackCount", attackCount);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            health--;
        }
    }


    public bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return (layerMask.value & (1 << obj.layer)) > 0;
    }

    public void TakeHit(float value, bool chargable = false)
    {
        if(chargable == true)
        {
            SkillManager.UpdateSkillGauge();
        }

        this.health -= value * damageReducation;
        Debug.Log($"Health: {health}");
    }

    public void PerformAttack(GameObject target)
    {
        if (target == null)
        {
            Debug.LogWarning("타겟이 설정되지 않았습니다.");
            return;
        }

        if (!target.TryGetComponent<ProjectPlayer>(out var player))
        {
            Debug.LogWarning($"{target.name}은 공격 가능한 대상이 아닙니다.");
            return;
        }



        // 타겟이 내각 및 거리 조건을 만족하는지 확인
        if (GetAngleHit(target.transform))
        {
            float finalDamage = damage;
            player.TakeDamage(finalDamage);
            attackCount++;
            Debug.Log($"{finalDamage}의 데미지를 {target.name}에게 주었습니다.");
        }
        else
        {
            Debug.Log($"{target.name}은 공격 범위 밖에 있습니다.");
        }
    }

    public void EndAttack()
    {
        // 애니메이션 이벤트에 의해 호출되는 메서드
        var target = behaviorTree.GetVariable("targetObject").GetValue() as GameObject;

        if (target != null)
        {
            PerformAttack(target);
        }
        else
        {
            Debug.LogWarning("타겟이 설정되지 않았습니다.");
        }
    }

    public bool GetAngleHit(Transform target)
    {
        // 내각 및 거리 계산
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

        if (angleToTarget < angle / 2) // 내각 체크
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= attackRange) // 거리 체크
            {
                return true; // 타격 성공
            }
        }

        return false; // 타격 실패
    }

    private void OnDrawGizmos()
    {
        // 공격 범위 원을 그리기
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // 공격 각도를 표시하기 위한 전방 벡터
        Vector3 forward = transform.forward * attackRange;

        // 각도의 왼쪽과 오른쪽 끝점을 계산
        Vector3 leftBoundary = Quaternion.Euler(0, -angle / 2, 0) * forward;
        Vector3 rightBoundary = Quaternion.Euler(0, angle / 2, 0) * forward;

        // 전방과 각도 경계를 선으로 표시
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + forward);
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}
