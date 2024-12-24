using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    private Item[] equippedItems; // 현재 장착된 아이템
    private Dictionary<E_StatType, float> totalStats; // 합산된 스탯

    public Dictionary<E_StatType, float> TotalStats => totalStats;

    private void Awake()
    {
        equippedItems = new Item[(int)E_EquipmentsType.Size];
        totalStats = new Dictionary<E_StatType, float>();
    }

    // 장비 장착
    public bool EquipItem(Item item)
    {
        if (item == null || item.itemType == null) return false;

        int slotIndex = (int)item.itemType;

        // 기존 장비 해제
        if (equippedItems[slotIndex] != null)
        {
            UnEquipItem(slotIndex);
        }

        // 장비 장착
        equippedItems[slotIndex] = item;
        UpdateTotalStats();

        return true;
    }

    // 장비 해제
    public bool UnEquipItem(int slotIndex)
    {
        if (equippedItems[slotIndex] == null) return false;

        equippedItems[slotIndex] = null;
        UpdateTotalStats();

        return true;
    }

    // 스탯 합산 계산
    private void UpdateTotalStats()
    {
        totalStats.Clear();

        foreach (var item in equippedItems)
        {
            if (item == null) continue;

            foreach (var stat in item.stats)
            {
                if (!totalStats.ContainsKey(stat.statType))
                {
                    totalStats[stat.statType] = 0;
                }

                totalStats[stat.statType] += stat.statValue;
            }
        }
    }

    // 스탯 값 가져오기
    public float GetStat(E_StatType statType)
    {
        return totalStats.TryGetValue(statType, out float value) ? value : 0f;
    }
}