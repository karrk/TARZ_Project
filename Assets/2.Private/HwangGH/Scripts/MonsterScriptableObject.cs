using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "Monster", menuName = "Monster Scriptable Object")]
public class MonsterScriptableObject : ScriptableObject
{
    // 몬스터 이름 혹은 ID
    public string monsterName;

    // 몬스터 체력
    public int monsterHp;

    // 몬스터 공격력
    public float monsterPower;

    // 몬스터 공격속도
    public float attackSpeed;

    // 몬스터 이동속도
    public float MoveSpeed;

    // 몬스터의 투사체 속도
    public float ProjectileSpeed;

    // 몬스터의 상태이상 저항
    public float StatusResistance;

    // 몬스터의 피격이상 저항
    public float HitResistance;

    // 몬스터의 공격 사거리
    public float AttackRange;

    // 몬스터의 탐시 사거리
    public float DetectionRange;

    // 몬스터의 스킬 조건 시간
    public float SkillConditionTime;

    // 몬스터의 스킬 조건 횟수
    public float SkillConditionCount;

    // 몬스터의 스킬 조건 거리
    public float SkillConditionRange;

    // 몬스터의 그로기 시간
    public float GroggyTime;

    // 몬스터의 기믹 파훼 피격 횟수
    public float GimmickBreakHitCount;

    // 몬스터의 스킬1 시전확률
    public float Skill1CastProbability;

    // 몬스터의 스킬2 시전확률
    public float Skill2CastProbability;

    // 몬스터의 스킬3 시전확률
    public float Skill3CastProbability;

    // 몬스터의 광폭스킬 조건 횟수
    public float BerserkSkillConditionCount;

    // 몬스터의 광폭 공격속도
    public float BerserkAttackSpeed;

    // 몬스터의 광폭 스킬1 시전확률
    public float BerserkSkill1CastProbability;

    // 몬스터의 광폭 스킬2 시전확률
    public float BerserkSkill2CastProbability;

    // 몬스터의 광폭 스킬3 시전확률
    public float BerserkSkill3CastProbability;
}
