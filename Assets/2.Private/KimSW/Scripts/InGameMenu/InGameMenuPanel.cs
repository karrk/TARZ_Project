using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class InGameMenuPanel : FadePanel
{
    [Inject]
    InGameUI inGameUI;

    private void Awake()
    {
        SetComponent();
    }
  

    public void ResumeGame()
    {
        // 게임 진행
        inGameUI.OffInGameMenu();

    }

    public void OnOptionMenu()
    {
        // 옵션 메뉴 활성화
    }

    public void TutorialEnd()
    {
        // 튜토리얼 종료
    }

    public void ExitGameScene()
    {
        // 게임씬 나가기
    }



 
}
