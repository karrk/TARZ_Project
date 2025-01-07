using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class EquipmentSelectPanel : MonoBehaviour, IOpenCloseMenu
{
    [Inject] InGameUI inGameUI;

    [SerializeField] GameObject firstSelected;

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
            Debug.Log("골드 획득 세팅");

            return;
        }


        List<NewEquipment> newEquipments = new List<NewEquipment>();

        for (int i = 0; i < 3; i++)
        {
            NewEquipment equipment = inGameUI.EquipmentManager.GetEquipment();

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
                else
                {
                    i--;
                    continue;
                }

               
            }

        }

        foreach (NewEquipment equipment in newEquipments)
        {
            Debug.Log("id " + equipment.id);
        }
    }
}
