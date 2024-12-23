using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidBossMonster : BaseMonster
{
    public float halfHealth;                  // 최대 체력의 절반 값

    private void Start()
    {
        halfHealth = base.health / 2;         // 최대 체력의 절반 계산

        // behaviorTree에 halfHealth 값을 설정
        if (behaviorTree != null && behaviorTree.GetVariable("halfHealth") != null)
        {
            behaviorTree.SetVariableValue("halfHealth", halfHealth);
        }
    }

    private void Update()
    {
        base.Update();

        // 테스트용: Space 키로 피해를 받는 상황을 테스트
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }

        // 피해 감소를 적용할 때 체력이 절반 이하로 떨어지면 피해 감소를 90%로 적용
        if (base.health <= halfHealth && base.damageReducation == 1f)
        {
            ApplyDamageReduction(0.1f);  // 90% 피해 감소
        }
    }

    // 피해 감소 비율을 적용하는 메소드
    private void ApplyDamageReduction(float reduction)
    {
        base.damageReducation = reduction;
    }

    // 기둥 상태가 다 부서졌을 때 원상복귀 처리
    public void RestoreDamageReduction()
    {
        base.damageReducation = 1f;  // 피해 감소를 원상복귀 (100%)
    }
}
