using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using Zenject;

public class BaseMonster : MonoBehaviour, IDamagable
{
    public float health;

    public float damageReducation = 1f;

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

    private void OnTriggerEnter(Collider other) // 하님께, 스킬을 통한 데미지를 받을때, 데미지 받는
        // 방식을 통합시키기 위해 과정 변경을 요청합니다 - 몬스터가 투사체를 판정하는 방식이 아닌
        // 투사체로부터 데미지값을 몬스터에게 전달하는 방식으로
    {
        Garbage garbage = other.GetComponent<Garbage>();
        if(IsInLayerMask(other.gameObject, garbageLayer) && garbage.IsProjectile == true)
        {
            // 몬스터의 체력을 감소
            //TakeDamage(1);
            TakeHit(1);
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

    public void EndAttack()
    {
        Debug.Log("EndAttack");
    }
}
