using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class EquipmentSelectPanel : MonoBehaviour, IOpenCloseMenu
{
    [Inject] InGameUI inGameUI;

    [SerializeField] GameObject firstSelected;

    [SerializeField] EquipmentSelectButton[] buttons;
    [SerializeField] EquipmentSlot[] slots;

    private void Awake()
    {
        buttons = GetComponentsInChildren<EquipmentSelectButton>(true);
        slots = GetComponentsInChildren<EquipmentSlot>(true);
    }

    public void OpenUIPanel()
    {
        inGameUI.StatusBarPanel.OffUIPanel();
        gameObject.SetActive(true);

        SetChoice();

        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    public void CloseUIPanel()
    {
        inGameUI.CurrentMenu = inGameUI.StatusBarPanel;
        inGameUI.CurrentMenu.OpenUIPanel();
        gameObject.SetActive(false);
    }

    public void SetChoice()
    {
        // 선택지가 없을때
        if (inGameUI.EquipmentManager.newEquipments.Count == 0){

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetGold();
            }

            return;
        }

        // 확률 적용
        int[] rarityProbability = new int[3];

        // 티어 별 남은 장비 개수
        int[] rarityCount = new int[(int)RarityTier.SIZE];

        for(int i = 0; i < inGameUI.EquipmentManager.newEquipments.Count; i++)
        {
            rarityCount[(int)inGameUI.EquipmentManager.newEquipments[i].rarityTier]++;
          
        }

       


        for(int i = 0; i < rarityProbability.Length; i++)
        {
            rarityProbability[i] = inGameUI.EquipmentManager.GetProbability();

        }

      
        List<NewEquipment> newEquipments = new List<NewEquipment>();

        for (int i = 0; i < 3; i++)
        {
            NewEquipment equipment = inGameUI.EquipmentManager.GetEquipment();


            //레어도 체크
            if (rarityProbability[i] != (int)equipment.rarityTier)
            {
                i--;
                continue;
            }

            //중복체크
            bool overlap = false;

        

            foreach (NewEquipment equipmentItem in newEquipments)
            {
                if (equipmentItem.id == equipment.id)
                {
                    overlap = true;
                    break;
                }

               

            }


            if (!overlap)
            {
                newEquipments.Add(equipment);
            }
            else
            {
                // 선택지가 3개 미만일시 중복이여도 추가
                if(inGameUI.EquipmentManager.newEquipments.Count < 3)
                {
                    newEquipments.Add(equipment);
                }
                // 해당 티어의 남은 장비가 3개 미만일시 중복이여도 추가
                else if (rarityCount[(int)equipment.rarityTier] < 3)
                {
                    newEquipments.Add(equipment);
                }

                // 중복
                else
                {
                    i--;
                    continue;
                }

               
            }

        }

        for (int i = 0;i < buttons.Length; i++)
        {
            buttons[i].SetInfo(newEquipments[i]);
        }

      
    }

    public void SetSlot(int num)
    {
        
        slots[num].SetSlot(inGameUI.EquipmentManager.equipped[num]);
        
    }

    public void ChangeSlot(int num)
    {
        slots[num].ChangeSlot(inGameUI.EquipmentManager.equipped[num]);
    }

    public void LevelUpSlot(int num, int level)
    {
        slots[num].LevelUpSlot(level);
    }
}
