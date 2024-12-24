using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using Zenject;

public class BaseMonster : MonoBehaviour
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

    public void TakeDamage(float damage)
    {
        health -= (damage * damageReducation);
        Debug.Log($"Health: {health}");
        SkillManager.UpdateGauge();
    }

    public void TakeDamageNotFillGauge(float damage)
    {
        health -= (damage * damageReducation);
        Debug.Log($"Health: {health}");
    }


    protected void Update()
    {
        if (behaviorTree != null && behaviorTree.GetVariable("health") != null)
        {
            behaviorTree.SetVariableValue("health", health);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Garbage garbage = other.GetComponent<Garbage>();
        if(IsInLayerMask(other.gameObject, garbageLayer) && garbage.IsProjectile == true)
        {
            // 몬스터의 체력을 감소
            TakeDamage(1);
        }
    }

    public bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return (layerMask.value & (1 << obj.layer)) > 0;
    }
}
