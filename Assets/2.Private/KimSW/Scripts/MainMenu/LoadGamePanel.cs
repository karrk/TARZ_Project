using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;
using System.IO;

public class LoadGamePanel : FadePanel, IOpenCloseMenu
{
    [Inject]
    MainSceneUI mainUI;
    [Inject]
    LobbyData lobbyData;

    [SerializeField] TMP_Text[] loadText;

    [SerializeField] bool[] isSave;

    private void Awake()
    {
        SetLoadText();
    }


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
   
    public void LoadData(int num)
    {
        if (isSave[num])
        {
            lobbyData.LoadData(num);
            mainUI.ChangeScene();
        }
        else
        {
            Debug.Log("세이브 파일 없음");
        }
    }

    public void SetLoadText()
    {
    #if UNITY_EDITOR
            string path = $"{Application.dataPath}/2.Private/KimSW/Json";
#else
            string path = Application.persistentDataPath; 
#endif

        for (int i = 0; i < loadText.Length; i++)
        {
            if (File.Exists($"{path}/Save{i+1}.json") == false)
            {
                loadText[i].text = "Empty";
            }
            else
            {
                loadText[i].text = $"세이브{i+1}";
                isSave[i] = true;
            }
        }

      

    }

}
