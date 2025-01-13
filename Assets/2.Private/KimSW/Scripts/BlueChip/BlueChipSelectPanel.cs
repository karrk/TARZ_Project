using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class BlueChipSelectPanel : MonoBehaviour, IOpenCloseMenu
{
    [Inject]
    InGameUI inGameUI;
    [SerializeField] Button selectedButton;

    public InteractBlueChip chip;

    public void OpenUIPanel()
    {
        inGameUI.EquipmentGetPanel.gameObject.SetActive(false);

        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
        Time.timeScale = 0f;
    }

    public void CloseUIPanel()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        
        inGameUI.CurrentMenu = inGameUI.StatusBarPanel;
        inGameUI.CurrentMenu.OpenUIPanel();
        chip.RemoveInstance();


    }
    public void ChangeButton()
    {
        CloseUIPanel();
    }

    public void CancelButton()
    {
        CloseUIPanel();
    }

}
