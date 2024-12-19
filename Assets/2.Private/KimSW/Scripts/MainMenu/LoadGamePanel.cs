using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;
public class LoadGamePanel : FadePanel, IOpenCloseMenu
{
    [Inject]
    MainSceneUI mainUI;



    private void OnDisable()
    {
        mainUI.CurrentMenu = mainUI.MainMenuPanel;
        mainUI.CurrentMenu.OpenUIPanel();

    }


    public void CloseUIPanel()
    {
        FadeOutUI();
    }

    public void OpenUIPanel()
    {
        gameObject.SetActive(true);
        FadeInUI();
    }
   

}
