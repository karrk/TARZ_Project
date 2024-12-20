using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class EquipmentPanel : AnimatedUI, ISlotPanel
{
    private List<UISlot> equipmentSlotList;

    public List<UISlot> EquipmentSlotList { get { return equipmentSlotList; } }

    [Inject]
    InGameUI gameUI;

    [Inject]
    ItemInventory inventory;

  
    private void Awake()
    {
        SetSlot();
        rotationOffset = transform.rotation;
    }

    public void SetSlot()
    {
        equipmentSlotList = GetComponentsInChildren<UISlot>(true).ToList();

        for (int i = 0; i < equipmentSlotList.Count; i++)
        {
            equipmentSlotList[i].SlotNumber = i;
            int index = i;
            equipmentSlotList[i].slotButton.onClick.AddListener(() => RemoveSprite(index));
          
        }
    }

  
    public void RemoveSprite(int num)
    {
        if (inventory.Equipments[num] is null)
        {
            return;
        }
        equipmentSlotList[num].RemoveSlotImage();
        inventory.RemoveEquipments(num);

        SlotSelectCallback(num);
    }

    public void SetSprite(int num, Sprite sprite)
    {
        equipmentSlotList[num].SetSlotImage(sprite);
    }

    public void SlotSelectCallback(int slotNumber)
    {
        if (inventory.Equipments[slotNumber] is null)
        {
            gameUI.ItemInformationPanel.SetDefaultEquippedInformation();
            gameUI.ItemInformationPanel.SetDefaultItemInformation();

        }
        else
        {
            gameUI.ItemInformationPanel.SetDefaultItemInformation();
            gameUI.ItemInformationPanel.SetEquippedItemInformation(inventory.Equipments[slotNumber]);
        }
        
    }

    public Vector3 GetImagePosition(int num)
    {
        return equipmentSlotList[num].transform.position;
    }

    public UISlot GetSlot(int num)
    {
        return equipmentSlotList[num];
    }
}
