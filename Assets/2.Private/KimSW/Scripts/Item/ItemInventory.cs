using System;
using UnityEngine;
using Zenject;

public enum E_EquipmentsType
{
    Head, Chest, Glasses, Arm, Leg, Earing, Ring, Boots, Necklace, Size
}

public class ItemInventory : IInitializable
{
    [Inject] private ProjectInstaller.InventorySetting setting;

    //[Inject] private InGameUI inGameUI;

    [Inject] private PlayerEquipment playerEquipment;

    private Equipment[] items;
    private Equipment[] equipments;

    public Equipment[] Items { get { return items; } }
    public Equipment[] Equipments { get { return equipments; } }

    int hasItemCount;

    //[SerializeField] int inventorySize;

    public event Action<int, Sprite> OnGetItem;
    public event Action OnChangeStatusInfo;

    public void Initialize()
    {
        items = new Equipment[setting.ItemInventoryCount];
        equipments = new Equipment[(int)E_EquipmentsType.Size];
    }

    public void GetItem()
    {

        if (hasItemCount < setting.ItemInventoryCount)
        {
            Equipment item = Equipment.GenerateEquipment((E_EquipmentsType)(UnityEngine.Random.Range(0, (int)E_EquipmentsType.Size)), UnityEngine.Random.Range(1, 4));
            //  Equipment item = new Equipment(itemInfos[Random.Range(0, itemInfos.Length)]);


            hasItemCount++;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] is null)
                {
                    items[i] = item;

                    //inGameUI.InventoryPanel.GetItem(i, setting.equipmentSprite.spriteType[(int)item.type].sprite[item.grade - 1]);
                    OnGetItem?.Invoke(i, setting.equipmentSprite.spriteType[(int)item.type].sprite[item.grade - 1]);

                    return;
                }

            }


        }

    }





    /// <summary>
    /// 장비 장착 시 호출 되는 함수
    /// </summary>
    public void EquipItem(int num)
    {

        playerEquipment.EquipItem(items[num]);
        equipments[(int)items[num].type] = items[num];

        hasItemCount--;
        items[num] = null;

        //inGameUI.StatusInformationPanel.UpdateStatusInfo();
        OnChangeStatusInfo?.Invoke();
    }
    /// <summary>
    /// 장비 해제 시 호출 되는 함수
    /// </summary>
    public void RemoveEquipments(int num)
    {
        playerEquipment.UnequipItem(num);
        equipments[num] = null;
        //inGameUI.StatusInformationPanel.UpdateStatusInfo();
        OnChangeStatusInfo?.Invoke();
    }


    public void RemoveBackpackItems(int num)
    {
        hasItemCount--;
        items[num] = null;

        //inGameUI.StatusInformationPanel.UpdateStatusInfo();
        OnChangeStatusInfo?.Invoke();
    }

    
}
