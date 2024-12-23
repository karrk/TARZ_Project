using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipment
{
    public E_EquipmentsType type;                   // 장비 타입
    public string name;                             // 장비 이름
    public int grade;                               // 등급 (e.g., 1=Common, 2=Rare, 3=Epic)
    public float primaryStat;                       // 주요 스탯
    public Dictionary<string, float> subStats;      // 부가 스탯

    // 생성자
    public Equipment(E_EquipmentsType type, string name, int grade, float primaryStat, Dictionary<string, float> subStats)
    {
        this.type = type;
        this.name = name;
        this.grade = grade;
        this.primaryStat = primaryStat;
        this.subStats = subStats;
    }

    public static Equipment GenerateEquipment(E_EquipmentsType type, int grade)
    {
        // 기본 스탯 설정
        float primaryStat = GetPrimaryStat(type, grade);

        // 부가 스탯 설정
        Dictionary<string, float> subStats = GenerateSubStats(grade);

        return new Equipment(type, type.ToString(), grade, primaryStat, subStats);
    }

    private static float GetPrimaryStat(E_EquipmentsType type, int grade)
    {
        switch (type)
        {
            case E_EquipmentsType.Head: return grade * 10f; // 크리티컬 데미지
            case E_EquipmentsType.Chest: return grade * 10f; // 최대 생명력
            case E_EquipmentsType.Glasses: return grade * 5f; // 크리티컬 확률
            case E_EquipmentsType.Arm: return grade * 10f; // 공격력
            case E_EquipmentsType.Leg: return grade * 10f; // 스테미너
            case E_EquipmentsType.Earing: return grade * 5f; // 최대 물건 보유량
            case E_EquipmentsType.Ring: return grade * 10f; // 마나흡수
            case E_EquipmentsType.Boots: return grade * 2f - 1; // 이동속도
            case E_EquipmentsType.Necklace: return grade * 10f; // 스테미너 회복
            default: return 0f;
        }
    }

    private static Dictionary<string, float> GenerateSubStats(int grade)
    {
        Dictionary<string, float> subStats = new Dictionary<string, float>();

        string[] possibleSubStats = {
            "크리티컬 확률", "크리티컬 데미지", "공격력", "스킬 공격력", "속성 공격력",
            "기본 공격력", "행운", "던지는 물건 보유량", "경험치 획득량",
            "공격시 마나 흡수", "스테미너 회복 속도", "최대 스테미너", "최대 생명력", "이동속도"
        };

        int subStatCount = Random.Range(0, 3); // 0~2개의 부가 스탯
        for (int i = 0; i < subStatCount; i++)
        {
            string statName = possibleSubStats[Random.Range(0, possibleSubStats.Length)];
            float statValue = Random.Range(1f, grade * 10f); // 등급에 따라 값 범위 증가
            if (!subStats.ContainsKey(statName))
            {
                subStats[statName] = statValue;
            }
        }

        return subStats;
    }
}