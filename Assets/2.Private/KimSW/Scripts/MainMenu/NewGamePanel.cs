using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class NewGamePanel : FadePanel, IOpenCloseMenu
{
    [Inject]
    MainSceneUI mainUI;


  


    public void CloseUIPanel()
    {
        FadeOutUI();
        mainUI.CurrentMenu = mainUI.MainMenuPanel;
        mainUI.CurrentMenu.OpenUIPanel();
        
    }

    public void OpenUIPanel()
    {
        gameObject.SetActive(true);
        FadeInUI();
    }
}
