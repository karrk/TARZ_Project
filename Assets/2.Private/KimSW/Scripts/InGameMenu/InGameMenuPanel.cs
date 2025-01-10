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

    public void TutorialEnd()
    {
        // 튜토리얼 종료
    }

    public void ExitGameScene()
    {
        // 게임씬 나가기
    }

    public void OpenUIPanel()
    {
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);

        inGameUI.EquipmentSelectPanel.slotsPanel.SetActive(true);
    }

    public void CloseUIPanel()
    {
        gameObject.SetActive(false);
        inGameUI.CurrentMenu = inGameUI.StatusBarPanel;
        inGameUI.CurrentMenu.OpenUIPanel();

        inGameUI.EquipmentSelectPanel.slotsPanel.SetActive(false);
    }

    public void OffUIPanel()
    {
        gameObject.SetActive(false);
    }
}
