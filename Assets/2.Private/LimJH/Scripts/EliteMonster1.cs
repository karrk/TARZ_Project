using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class EliteMonster1 : MonoBehaviour
{
    public float health;
    public bool canJumpAttack = true;
    public float jumpAttackCoolTime;

    private BehaviorTree behaviorTree;

    private void Awake()
    {
        behaviorTree = GetComponent<BehaviorTree>(); // BehaviorTree 컴포넌트를 찾음
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Health: {health}");
    }

    private void Update()
    {
        if (behaviorTree != null && behaviorTree.GetVariable("health") != null)
        {
            behaviorTree.SetVariableValue("health", health);
        }

        if (behaviorTree != null && behaviorTree.GetVariable("canJumpAttack") != null)
        {
            behaviorTree.SetVariableValue("canJumpAttack", canJumpAttack);
        }
    }
}
