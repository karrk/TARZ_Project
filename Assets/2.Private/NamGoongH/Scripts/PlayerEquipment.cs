using System;
using System.Collections.Generic;
using Zenject;

public class PlayerEquipment : IInitializable
{
    private Equipment[] equippedItems; // 현재 장착된 아이템
    private Dictionary<E_StatType, float> totalStats; // 합산된 스탯
    public Dictionary<E_StatType, Action> ChangeObserver; 

    public Dictionary<E_StatType, float> TotalStats { get { return totalStats; } }

    public void Initialize()
    {
        equippedItems = new Equipment[(int)E_EquipmentsType.Size];
        totalStats = new Dictionary<E_StatType, float>();

        ChangeObserver = new Dictionary<E_StatType, Action>();
    }

    public void AddActionOnChangedEquip(E_StatType type, Action action)
    {
        if (ChangeObserver.ContainsKey(type) == false)
            ChangeObserver.Add(type, null);

        ChangeObserver[type] += action;
    }

    private void NotifyChange(E_StatType type)
    {
        ChangeObserver[type]?.Invoke();
    }

    // 장비 장착
    public bool EquipItem(Equipment equipment)
    {
        if (equipment == null) return false;

        int slotIndex = (int)equipment.type;

        // 기존 장비 해제
        if (equippedItems[slotIndex] != null)
        {
            UnequipItem(slotIndex);
        }

        // 장비 장착
        equippedItems[slotIndex] = equipment;
        UpdateTotalStats();

        return true;
    }

    // 장비 해제
    public bool UnequipItem(int slotIndex)
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

        foreach (var equipment in equippedItems)
        {
            if (equipment == null) continue;

            foreach (var stat in equipment.stats)
            {
                if (!totalStats.ContainsKey(stat.statType))
                {
                    totalStats[stat.statType] = 0;
                }

                totalStats[stat.statType] += stat.statValue;
                NotifyChange(stat.statType);
            }
        }
    }

    // 스탯 값 가져오기
    public float GetStat(E_StatType statType)
    {
        return totalStats.TryGetValue(statType, out float value) ? value : 0f;
    }
}