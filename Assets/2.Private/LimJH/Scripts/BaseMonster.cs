using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using Zenject;

public class BaseMonster : MonoBehaviour, IDamagable
{
    public float health;
    public float damageReducation = 1f;
    public float damage;

    public BehaviorTree behaviorTree;

    private LayerMask garbageLayer;

    [Inject] SkillManager SkillManager;

    private void Awake()
    {
        behaviorTree = GetComponent<BehaviorTree>(); // BehaviorTree 컴포넌트를 찾음
    }

    private void Start()
    {
        garbageLayer = LayerMask.GetMask("Garbage");
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


    protected void Update()
    {
        if (behaviorTree != null && behaviorTree.GetVariable("health") != null)
        {
            behaviorTree.SetVariableValue("health", health);
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
            SkillManager.UpdateGauge();
        }

        this.health -= value * damageReducation;
        Debug.Log($"Health: {health}");
    }

    public void PerformAttack(GameObject target)
    {
        if (target.TryGetComponent<ProjectPlayer>(out var player))
        {
            float finalDamage = damage;
            player.TakeDamage(finalDamage);
            Debug.Log($"{finalDamage}의 데미지를 {target.name}에게 주었습니다.");
        }
        else
        {
            Debug.LogWarning($"{target.name}은 공격 가능한 대상이 아닙니다.");
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
}
