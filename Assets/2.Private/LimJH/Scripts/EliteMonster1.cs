using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class EliteMonster1 : BaseMonster
{
    public bool canJumpAttack = true;
    public float jumpAttackCoolTime;


    private void Update()
    {
        base.Update();

        if (behaviorTree != null && behaviorTree.GetVariable("canJumpAttack") != null)
        {
            behaviorTree.SetVariableValue("canJumpAttack", canJumpAttack);
        }
    }
}
