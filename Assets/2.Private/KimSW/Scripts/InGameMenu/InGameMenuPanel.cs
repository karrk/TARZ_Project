using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class InGameMenuPanel : MonoBehaviour, IOpenCloseMenu
{
    [Inject]
    InGameUI inGameUI;


    [SerializeField] Button selectedButton;

    [SerializeField] Button exitChoiceButton;

    [SerializeField] GameObject exitChoicePanel;


    public void ResumeGame()
    {
        // 게임 진행
        CloseUIPanel();

    }

    public void OnOptionMenu()
    {
        // 옵션 메뉴 활성화
        OffUIPanel();
        inGameUI.CurrentMenu = inGameUI.OptionPanel;
        inGameUI.CurrentMenu.OpenUIPanel();

    }

    public void OnKeyMenual()
    {
        // 조작법 활성화
        OffUIPanel();
        inGameUI.CurrentMenu = inGameUI.MenualPanel;
        inGameUI.CurrentMenu.OpenUIPanel();

    }

    public void ExitGameScene()
    { 
        exitChoicePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(exitChoiceButton.gameObject);
    }

    public void ConfirmExitPanel()
    {
        // 로비로 나가기
    }

    public void CancelExitPanel()
    {
        exitChoicePanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
    }

    public void OpenUIPanel()
    {
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);

    }

    public void CloseUIPanel()
    {
        gameObject.SetActive(false);
        exitChoicePanel.SetActive(false);
        inGameUI.CurrentMenu = inGameUI.StatusBarPanel;
        inGameUI.CurrentMenu.OpenUIPanel();

    }

    public void OffUIPanel()
    {
        gameObject.SetActive(false);
    }
}
