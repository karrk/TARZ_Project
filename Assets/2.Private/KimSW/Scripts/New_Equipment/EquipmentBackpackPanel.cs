using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class EquipmentBackpackPanel : MonoBehaviour, IOpenCloseMenu
{
    [Inject]
    InGameUI inGameUI;

    [SerializeField] GameObject slotButton;
    [SerializeField] GameObject chip;
    [SerializeField] TMP_Text blueChipName;
    [SerializeField] TMP_Text blueChipExplain;

    public BlueChip blueChip;

    [SerializeField] bool isSlot;

    public void OpenUIPanel()
    {
        inGameUI.StatusBarPanel.OffUIPanel();
        SetChipInfo();
        chip.SetActive(true);
        inGameUI.EquipmentSelectPanel.slotsPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(chip);
    }

    public void CloseUIPanel()
    {
        inGameUI.CurrentMenu = inGameUI.StatusBarPanel;
        inGameUI.CurrentMenu.OpenUIPanel();

        chip.SetActive(false);
        inGameUI.EquipmentSelectPanel.slotsPanel.SetActive(false);
    }

    public void SetChipInfo()
    {
        if (blueChip)
        {
            blueChipName.text = blueChip.blueChipName;
            blueChipExplain.text = blueChip.description;
        }
    }

    public void ChangeSelect()
    {
        if (isSlot)
        {
            EventSystem.current.SetSelectedGameObject(chip);
            isSlot = false;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(slotButton);
            isSlot = true;
        }
       
       
    }
}
