using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentTest : MonoBehaviour
{
    private PlayerStatsTest playerStats;

    private void Start()
    {
        playerStats = GetComponent<PlayerStatsTest>();

        // 슬롯에 맞는 장비를 랜덤 생성하여 장착
        Equipment headGear = Equipment.GenerateEquipment(E_EquipmentsType.Head, 1);
        playerStats.EquipItem(headGear);
        PrintEquipmentData(headGear);

        Equipment chestArmor = Equipment.GenerateEquipment(E_EquipmentsType.Chest, 1);
        playerStats.EquipItem(chestArmor);
        PrintEquipmentData(chestArmor);

        Equipment boots = Equipment.GenerateEquipment(E_EquipmentsType.Boots, 1);
        playerStats.EquipItem(boots);
        PrintEquipmentData(boots);
    }

    public void PrintEquipmentData(Equipment equipment)
    {
        Debug.Log($"Name : {equipment.name}");
        Debug.Log($"Type : {equipment.type}");
        Debug.Log($"Grade : {equipment.grade}");
        foreach (var stat in equipment.stats)
        {
            Debug.Log($"Stats : {stat.statType} - {stat.statValue}");
        }
    }
}
