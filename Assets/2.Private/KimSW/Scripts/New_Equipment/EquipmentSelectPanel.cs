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

        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    public void CloseUIPanel()
    {
        inGameUI.CurrentMenu = inGameUI.StatusBarPanel;
        inGameUI.CurrentMenu.OpenUIPanel();
        gameObject.SetActive(false);
    }
}
