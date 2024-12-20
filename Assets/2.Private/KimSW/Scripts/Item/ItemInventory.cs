using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using static UnityEditor.Progress;

public enum E_EquipmentsType
{
    Head, Chest, Glasses, Arm, Leg, Earing, Ring, Boots, Necklace, Size
}

public class ItemInventory : MonoBehaviour
{
    [Inject]
    InGameUI inGameUI;

    [SerializeField] ItemInfo[] itemInfos;

    private Item[] items;
    private Item[] equipments;

    public Item[] Items {  get { return items; } }
    public Item[] Equipments { get { return equipments; } }

    int hasItemCount;


    [SerializeField] int inventorySize;




    private void Awake()
    {
        items = new Item[inventorySize];
        equipments = new Item[(int)E_EquipmentsType.Size];

    }

    public void GetItem()
    {
        if (hasItemCount < inventorySize)
        {

            Item item = new Item(itemInfos[Random.Range(0, itemInfos.Length)]);
            hasItemCount++;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] is null)
                {
                    items[i] = item;

                    inGameUI.InventoryPanel.GetItem(i, item.sprite);

                    return;
                }
               
            }


        }

    }

  

    public void RemoveItem(int num)
    {
        equipments[(int)items[num].itemType] = items[num];
        hasItemCount--;
        items[num] = null;
     
    }

    public void RemoveEquipments(int num)
    {
        equipments[num] = null;
    }
}
