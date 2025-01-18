using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class GameOverPanel : MonoBehaviour, IOpenCloseMenu
{
  
    
    [Inject]
    LobbyData lobbyData;

    public CancellationTokenSource cancell = new CancellationTokenSource();

    [SerializeField] GameObject selectedButton;

    [SerializeField] TMP_Text gameoverText;

    public void OpenUIPanel()
    {
        gameObject.SetActive(true);

        SetSelected().Forget();

        lobbyData.SaveData();
       
    }



    public void CloseUIPanel()
    {
        

    }

    public void SetGameoverText(bool isWin)
    {
        if (isWin)
        {
            gameoverText.text = "Clear";
        }
        else
        {
            gameoverText.text = "Game Over";
        }
    }

    public void GoToLobby()
    {
        // 로비 이동
    }

    async UniTaskVoid SetSelected()
    {
        
        await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: cancell.Token);
        EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;

    }

    private void OnDestroy()
    {

        cancell.Cancel();
        cancell.Dispose();
    }

}
