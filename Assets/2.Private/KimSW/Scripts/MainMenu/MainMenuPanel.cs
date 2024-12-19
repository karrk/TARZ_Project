using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class MainMenuPanel : FadePanel, IOpenCloseMenu
{
    [Inject]
    MainSceneUI mainUI;


    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(selectButton.gameObject);
    }



    private void OnDisable()
    {

        if (mainUI.CurrentMenu.Equals(this) == false)
        {
            mainUI.CurrentMenu.OpenUIPanel();
        }
    }

    public void LoadGame()
    {
        FadeOutUI();
        mainUI.CurrentMenu = mainUI.LoadGamePanel;
       
        // 계속하기 활성화 
    }

    public void NewGame()
    {
        FadeOutUI();
        mainUI.CurrentMenu = mainUI.NewGamePanel;
       
        // 새로하기 활성화
    }

    public void OnOptionMenu()
    {
        // 옵션 메뉴 활성화
    }

    public void ExitGame()
    {
        // 게임 나가기
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }



    public void CloseUIPanel()
    {
        
    }

 

    public void OpenUIPanel()
    {
        gameObject.SetActive(true);
        FadeInUI();
    }
}
