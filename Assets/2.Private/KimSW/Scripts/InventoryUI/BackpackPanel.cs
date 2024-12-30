using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class BackpackPanel : AnimatedUI, ISlotPanel
{

    private List<UISlot> backpackSlotList;
    private UISlot currentEquipmentSlot;


    [Inject]
    InGameUI gameUI;

    [Inject]
    ItemInventory inventory;


    [SerializeField] Animator uiAnimator;

    int currentNumber;

    bool dropFlag;

    private void Awake()
    {
        SetSlot();

        SetMoveOffset();

     

    }

    private void Start()
    {
        currentEquipmentSlot = gameUI.InventoryPanel.equipmentPanel.GetSlot(0);
        gameUI.InventorySetPanel.gameObject.SetActive(false);
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

    public void DropItem(bool check)
    {
        dropFlag=check;
    }

    public void SetSprite(int num, Sprite sprite)
    {
        backpackSlotList[num].SetSlotImage(sprite);
        SlotSelectCallback(currentNumber);
    }

    public void EquipmentAnimation(int num)
    {
        uiAnimator.SetTrigger("PlayUIAni");

        ThrowSlotUI(backpackSlotList[num],
         gameUI.InventoryPanel.equipmentPanel.GetSlot((int)inventory.Items[num].type),
         gameUI.InventoryPanel.equipmentPanel);
    }

 
    public void RemoveSprite(int num)
    {
        if (inventory.Items[num] is null)
        {
            return;
        }

        // 장비 장착
        if (dropFlag == false)
        {
            EquipmentAnimation(num);

            inventory.EquipItem(num);

        }
        else
        {
            backpackSlotList[num].RemoveSlotImage();
            inventory.RemoveBackpackItems(num);
         
        }


        SlotSelectCallback(num);
    }

    public void SlotSelectCallback(int slotNumber)
    {
        currentNumber = slotNumber;
        if (inventory.Items[slotNumber] is null)
        {
            gameUI.ItemInformationPanel.SetDefaultEquippedInformation();
            gameUI.ItemInformationPanel.SetDefaultItemInformation();

            currentEquipmentSlot.SetDefaultColor();
        }
        else
        {
            gameUI.ItemInformationPanel.SetSelectItemInformation(inventory.Items[slotNumber]);

            int typeNumber = (int)inventory.Items[slotNumber].type;

            // 일치 하는 장비칸 포커스
            currentEquipmentSlot.SetDefaultColor();
            currentEquipmentSlot = gameUI.InventoryPanel.equipmentPanel.GetSlot(typeNumber);
            currentEquipmentSlot.slotButton.image.color = Color.yellow;

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
