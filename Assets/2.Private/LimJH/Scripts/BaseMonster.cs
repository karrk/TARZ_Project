using UnityEngine;
using Zenject;
using System;
using UnityEngine.AI;

public class BaseMonster : MonoBehaviour, IDamagable, IPushable, IPooledObject
{
    [SerializeField]
    private ProjectInstaller.BaseMonsterStat stat;
    public ProjectInstaller.BaseMonsterStat Stat => stat;

    private ProjectPlayer player;
    [SerializeField] private E_Monster type;

    #region Injects

    [Inject] private ProjectInstaller.BaseMonsterStat originStat;
    [Inject] PlayerStats playerStats;
    [Inject] private PoolManager manager;

    #endregion

    #region Refs

    [SerializeField] private MonsterReference reference;
    public MonsterReference Reference => reference;

    [SerializeField] private DraggedOption drag;
    public DraggedOption Drag => drag;

    [SerializeField] private PushOption push;
    public PushOption Pushed => push;

    [SerializeField] private KnockDownOption knock;
    public KnockDownOption Knock => knock;

    #endregion

    #region Props

    public bool IsOnDamaged { get; private set; }
    public float Dist => Vector3.Distance(transform.position, player.transform.position);
    public Vector3 PlayerPos => player.transform.position;
    public Vector3 SkillPos { get; private set; }
    public E_SkillType SkillType { get; private set; }

    public Enum MyType => type;

    public GameObject MyObj => this.gameObject;

    #endregion

    public int attackCount; // 기믹 ?? 용도?

    public void Init(ProjectPlayer player)
    {
        Reference.Nav.enabled = true;
        originStat.SendToCopyStats<ProjectInstaller.BaseMonsterStat>(ref stat);
        this.player = player;
    }

    private void OnDisable()
    {
        Reference.Nav.enabled = false;
    }

    public void ResetDamageState()
    {
        IsOnDamaged = false;
    }

    /*public bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return (layerMask.value & (1 << obj.layer)) > 0;
    }*/

    public void TakeHit(float value, bool chargable = false)
    {
        if(chargable == true)
        {
            playerStats.ChargeMana();
        }

        stat.Health -= value * stat.DamageReducation;
        Debug.Log($"Health: {stat.Health}");

        IsOnDamaged = true;
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
            float finalDamage = stat.Damage;
            IDamagable damagable = target.GetComponent<IDamagable>();
            if(damagable != null)
            {
                damagable.TakeHit(finalDamage, false);
            }
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
        if (player != null)
        {
            PerformAttack(player.gameObject);
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

        if (angleToTarget < stat.Angle / 2) // 내각 체크
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= stat.AttackRange) // 거리 체크
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
        Gizmos.DrawWireSphere(transform.position, stat.AttackRange);

        // 공격 각도를 표시하기 위한 전방 벡터
        Vector3 forward = transform.forward * stat.AttackRange;

        // 각도의 왼쪽과 오른쪽 끝점을 계산
        Vector3 leftBoundary = Quaternion.Euler(0, -1* stat.Angle / 2, 0) * forward;
        Vector3 rightBoundary = Quaternion.Euler(0, stat.Angle / 2, 0) * forward;

        // 전방과 각도 경계를 선으로 표시
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + forward);
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }

    public void Push(Vector3 pos, E_SkillType skillType)
    {
        SkillPos = pos;
        this.SkillType = skillType;

        //Debug.Log($"{gameObject.name} pushed by {skillType}");
    }

    public void ResetSkillType()
    {
        SkillType = E_SkillType.None;
        SkillPos = Vector3.down;
    }

    protected virtual void Update()
    {
    }

    public void Return()
    {
        manager.Return(this);
    }
}

[Serializable]
public class MonsterReference
{
    public Animator Anim;
    public Collider Coll;
    public Rigidbody Rb;
    public NavMeshAgent Nav; 
}

[Serializable]
public class DraggedOption
{
    public float MaxDuration;
    public float GatherSpeed;
    public float GatherRad;
}

[Serializable]
public class PushOption
{
    public float MaxDuration;
    public float PushDist;
    public float PushSpeed;
}

[Serializable]
public class KnockDownOption
{
    public float DownDration;
}
