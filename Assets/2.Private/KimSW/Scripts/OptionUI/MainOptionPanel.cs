using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class MainOptionPanel : MonoBehaviour, IOpenCloseMenu
{
    [Inject]
    MainSceneUI mainUI;

    [SerializeField] Button defaultSelectedButton;

    [SerializeField] OptionDetailPanel optionDetailPanel;
 

    public void OpenUIPanel()
    {
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultSelectedButton.gameObject);
    }

    public void CloseUIPanel()
    {
        gameObject.SetActive(false);
        mainUI.CurrentMenu = mainUI.MainMenuPanel;
        mainUI.CurrentMenu.OpenUIPanel();
    }

    public void OnOptionSaveButton()
    {
        EventSystem.current.SetSelectedGameObject(defaultSelectedButton.gameObject);
    }

   

}
