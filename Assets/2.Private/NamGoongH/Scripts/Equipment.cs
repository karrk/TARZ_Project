using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipment
{
    public string name;
    public int grade; // 등급 (e.g., 1=Common, 2=Rare, 3=Epic)
    public int attackPower;
    public int defense;
    public int health;

    public Equipment(string name, int grade, int attackPower, int defense, int health)
    {
        this.name = name;
        this.grade = grade;
        this.attackPower = attackPower;
        this.defense = defense;
        this.health = health;
    }

    // 랜덤 속성 생성
    public static Equipment GenerateRandomEquipment()
    {
        string[] names = { "Sword", "Shield", "Helm", "Boots" };
        int grade = Random.Range(1, 4); // Common(1) ~ Epic(3)
        int attack = Random.Range(1, 10) * grade;
        int defense = Random.Range(1, 10) * grade;
        int health = Random.Range(5, 20) * grade;

        return new Equipment(
            names[Random.Range(0, names.Length)],
            grade,
            attack,
            defense,
            health
        );
    }
}