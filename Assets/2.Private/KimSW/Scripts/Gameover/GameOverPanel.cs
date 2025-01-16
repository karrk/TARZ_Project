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
    StaticEquipment staticEquipment;

    [Inject]
    StaticBluechip bluechip;
    
    [Inject]
    LobbyData lobbyData;

    public CancellationTokenSource cancell = new CancellationTokenSource();

    [SerializeField] GameObject selectedButton;

    public void OpenUIPanel()
    {
        gameObject.SetActive(true);

        SetSelected().Forget();
      

        ResetData();
    }

    void ResetData()
    {
        staticEquipment.firstInit = false;
        bluechip.ResetBlueChip();
        lobbyData.SaveData();
    }

    public void CloseUIPanel()
    {
        

    }

    public void GoToLobby()
    {
        // 로비 이동
    }

    async UniTaskVoid SetSelected()
    {
        
        await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: cancell.Token);
        EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);


    }

    private void OnDestroy()
    {

        cancell.Cancel();
        cancell.Dispose();
    }

}
