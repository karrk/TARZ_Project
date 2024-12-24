using BehaviorDesigner.Runtime.Tasks.Unity.UnityLight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_StatType
{
    CriticalChance,         // 크리티컬 확률
    CriticalDamage,         // 크리티컬 데미지
    AttackPower,            // 공격력
    SkillAttackPower,       // 스킬 공격력
    ElementaAttackPower,    // 속성 공격력
    BasicAttackPower,       // 기본 공격력
    Luck,                   // 행운
    ThrowableItemCapacity,  // 던지는 물건 보유량
    ExperienceGain,         // 경험치 획득량
    ManaAbsorption,         // 마나 흡수
    StaminaRecoveryRate,    // 스테미너 회복 속도
    MaxStamina,             // 최대 스테미너
    MaxHealth,              // 최대 생명력
    MovementSpeed,          // 이동 속도
    Size
}

[System.Serializable]
public class Equipment
{
    public E_EquipmentsType type;                   // 장비 타입
    public string name;                             // 장비 이름
    public int grade;                               // 등급 (e.g., 1=Common, 2=Rare, 3=Epic)
    public List<Stat> stats;                        // 스탯 통합 관리

    // 생성자
    public Equipment(E_EquipmentsType type, string name, int grade, List<Stat> stats)
    {
        this.type = type;
        this.name = name;
        this.grade = grade;
        this.stats = stats;
    }

    public static Equipment GenerateEquipment(E_EquipmentsType type, int grade)
    {
        // 스탯 리스트 생성
        List<Stat> stats = new List<Stat>();

        // 기본 스탯 설정
        Stat primaryStat = GeneratePrimaryStat(type, grade);
        stats.Add(primaryStat);

        // 부가 스탯 설정
        List<Stat> subStats = GenerateSubStats(grade);
        if (subStats != null)
        {
            foreach (Stat stat in subStats)
            {
                stats.Add(stat);
            }
        }

        return new Equipment(type, $"{type} Item", grade, stats);
    }

    private static Stat GeneratePrimaryStat(E_EquipmentsType type, int grade)
    {
        E_StatType primaryStatType;
        float primaryStatValue;

        switch (type)
        {
            // 머리 : 크리티컬 데미지
            case E_EquipmentsType.Head:
                primaryStatType = E_StatType.CriticalDamage;
                primaryStatValue = grade * 1f;
                break;
            // 상의 : 최대 생명력
            case E_EquipmentsType.Chest:
                primaryStatType = E_StatType.MaxHealth;
                primaryStatValue = grade * 10f;
                break;
            // 눈장식 : 크리티컬 확률
            case E_EquipmentsType.Glasses:
                primaryStatType = E_StatType.CriticalChance;
                primaryStatValue = grade * 5f;
                break;
            // 장갑 : 각종 공격력중 무작위
            case E_EquipmentsType.Arm:
                primaryStatType = (E_StatType)Random.Range((int)E_StatType.AttackPower, (int)E_StatType.BasicAttackPower + 1);
                primaryStatValue = grade * 10f;
                break;
            // 하의 : 최대 스테미너
            case E_EquipmentsType.Leg:
                primaryStatType = E_StatType.MaxStamina;
                primaryStatValue = grade * 10f;
                break;
            // 귀장식 : 최대 물건 보유량
            case E_EquipmentsType.Earing:
                primaryStatType = E_StatType.ThrowableItemCapacity;
                primaryStatValue = grade * 5f;
                break;
            // 반지 : 마나 흡수량
            case E_EquipmentsType.Ring:
                primaryStatType = E_StatType.ManaAbsorption;
                primaryStatValue = grade * 10f;
                break;
            // 신발 : 이동 속도
            case E_EquipmentsType.Boots:
                primaryStatType = E_StatType.MovementSpeed;
                primaryStatValue = grade * 2f - 1f;
                break;
            // 목장식 : 스테미너 회복량
            case E_EquipmentsType.Necklace:
                primaryStatType = E_StatType.StaminaRecoveryRate;
                primaryStatValue = grade * 10f;
                break;
            // 기본값
            default:
                primaryStatType = E_StatType.Luck;
                primaryStatValue = 0f;
                break;
        }
        
        return new Stat(primaryStatType, primaryStatValue);
    }

    private static List<Stat> GenerateSubStats(int grade)
    {
        List<Stat> subStats = new List<Stat>();

        int subStatCount = Random.Range(0, 3); // 0~2개의 부가 스탯

        for (int i = 0; i < subStatCount; i++)
        {
            E_StatType statType = (E_StatType)Random.Range(0, (int)E_StatType.Size);
            float statValue = Random.Range(1, grade + 1); // 등급에 따라 값 범위 증가
            switch (statType)
            {
                case E_StatType.CriticalChance:
                    switch (grade)
                    {
                        case 1:
                            statValue = 1f;
                            break;
                        case 2:
                            statValue = 5f;
                            break;
                        case 3:
                            statValue = 10f;
                            break;
                        default:
                            statValue = 0;
                            break;
                    }
                    break;
                case E_StatType.CriticalDamage:
                    statValue = 1f * statValue;
                    break;
                case E_StatType.MovementSpeed:
                    statValue = 2f * statValue - 1f;
                    break;
                case E_StatType.ThrowableItemCapacity:
                    statValue = 5f * statValue;
                    break;
                default:
                    statValue = 10f * statValue;
                    break;
            }

            subStats.Add(new Stat(statType, statValue));
        }

        return subStats;
    }
}

[System.Serializable]
public class Stat
{
    public E_StatType statType;     // 스탯 종류
    public float statValue;         // 스탯 값

    public Stat(E_StatType statType, float statValue)
    {
        this.statType = statType;
        this.statValue = statValue;
    }
}