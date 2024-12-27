using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class OptionPanel : MonoBehaviour, IOpenCloseMenu
{
    [Inject]
    InGameUI inGameUI;

    [SerializeField] Button selectedButton;

    public void OpenUIPanel()
    {
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
    }

    public void CloseUIPanel()
    {
        gameObject.SetActive(false);
        inGameUI.CurrentMenu = inGameUI.InGameMenuPanel;
        inGameUI.CurrentMenu.OpenUIPanel();
    }

   
}
