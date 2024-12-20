using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidBossMonster : BaseMonster
{
    public float halfHealth;

    private void Start()
    {
        halfHealth = base.health / 2;
        if (behaviorTree != null && behaviorTree.GetVariable("halfHealth") != null)
        {
            behaviorTree.SetVariableValue("halfHealth", halfHealth);
        }
    }

    private void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }
}
