using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class BackpackPanel : MonoBehaviour, ISlotPanel
{

    private List<UISlot> backpackSlotList;

    [Inject]
    InGameUI gameUI;

    [Inject]
    ItemInventory inventory;

    private void Awake()
    {
        SetSlot();
    }



    public void SetSelectCursor()
    {
        EventSystem.current.SetSelectedGameObject(backpackSlotList[0].slotButton.gameObject);
        SlotSelectCallback(0);
    }

    public void SetSlot()
    {
        backpackSlotList = GetComponentsInChildren<UISlot>(true).ToList();

        for (int i = 0; i < backpackSlotList.Count; i++)
        {
            backpackSlotList[i].SlotNumber = i;
            int index = i;
            backpackSlotList[i].slotButton.onClick.AddListener(() => RemoveSprite(index));

        }

    }

    public void SetSprite(int num, Sprite sprite)
    {
        backpackSlotList[num].SetSlotImage(sprite);

        SlotSelectCallback(num);
    }

    public void RemoveSprite(int num)
    {
        if (inventory.Items[num] is null)
        {
            return;
        }

        // ¿Â∫Ò ¿Â¬¯
        gameUI.InventoryPanel.equipmentPanel.SetSprite((int)inventory.Items[num].itemType, backpackSlotList[num].GetSprite());

        backpackSlotList[num].RemoveSlotImage();
        inventory.RemoveItem(num);

        SlotSelectCallback(num);
    }

    public void SlotSelectCallback(int slotNumber)
    {
      
        if (inventory.Items[slotNumber] is null)
        {
            gameUI.ItemInformationPanel.SetDefaultEquippedInformation();
            gameUI.ItemInformationPanel.SetDefaultItemInformation();
        }
        else
        {
            gameUI.ItemInformationPanel.SetSelectItemInformation(inventory.Items[slotNumber]);

            int typeNumber = (int)inventory.Items[slotNumber].itemType;
            if (inventory.Equipments[typeNumber] is null)
            {
                gameUI.ItemInformationPanel.SetDefaultEquippedInformation();
            }
            else
            {
                gameUI.ItemInformationPanel.SetEquippedItemInformation(inventory.Equipments[typeNumber]);
            }

          
        }
     

        
    }
}
