using UniRx;
using UnityEngine;
using Zenject;

public enum E_EquipmentsType
{
    Head, Chest, Glasses, Arm, Leg, Earing, Ring, Boots, Necklace, Size
}

public class ItemInventory : MonoBehaviour
{
    [Inject]
    InGameUI inGameUI;

    [Inject]
    PlayerEquipment playerEquipment;

    [SerializeField] EquipmentSprite equipmentSprite;


    private Equipment[] items;
    private Equipment[] equipments;

    public Equipment[] Items { get { return items; } }
    public Equipment[] Equipments { get { return equipments; } }

 
    int hasItemCount;


    [SerializeField] int inventorySize;




    private void Awake()
    {
        items = new Equipment[inventorySize];
        equipments = new Equipment[(int)E_EquipmentsType.Size];

    }

    public void GetItem()
    {




        if (hasItemCount < inventorySize)
        {
            Equipment item = Equipment.GenerateEquipment((E_EquipmentsType)(Random.Range(0, (int)E_EquipmentsType.Size)), Random.Range(1, 4));
            //  Equipment item = new Equipment(itemInfos[Random.Range(0, itemInfos.Length)]);


            hasItemCount++;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] is null)
                {
                    items[i] = item;

                    inGameUI.InventoryPanel.GetItem(i, equipmentSprite.spriteType[(int)item.type].sprite[item.grade - 1]);

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

        inGameUI.StatusInformationPanel.UpdateStatusInfo();
    }
    /// <summary>
    /// 장비 해제 시 호출 되는 함수
    /// </summary>
    public void RemoveEquipments(int num)
    {
        playerEquipment.UnequipItem(num);
        equipments[num] = null;
        inGameUI.StatusInformationPanel.UpdateStatusInfo();
    }


    public void RemoveBackpackItems(int num)
    {
        hasItemCount--;
        items[num] = null;

        inGameUI.StatusInformationPanel.UpdateStatusInfo();
    }
}
