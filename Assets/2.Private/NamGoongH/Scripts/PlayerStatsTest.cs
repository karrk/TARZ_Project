using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsTest : MonoBehaviour
{
    public Dictionary<E_EquipmentsType, Equipment> equippedItems;

    void Start()
    {
        equippedItems = new Dictionary<E_EquipmentsType, Equipment>();
        foreach (E_EquipmentsType type in Enum.GetValues(typeof(E_EquipmentsType)))
        {
            if (type != E_EquipmentsType.Size)
                equippedItems[type] = null;
        }
    }

    public void EquipItem(Equipment newItem)
    {
        if (equippedItems[newItem.type] != null)
        {
            Debug.Log($"Unequipping {equippedItems[newItem.type].name}");
        }

        equippedItems[newItem.type] = newItem;
        Debug.Log($"Equipped {newItem.name} in {newItem.type}");
        RecalculateStats();
    }

    private void RecalculateStats()
    {
        // 기본 스탯과 장비 스탯 계산
        Debug.Log("Recalculating stats...");
    }
}
