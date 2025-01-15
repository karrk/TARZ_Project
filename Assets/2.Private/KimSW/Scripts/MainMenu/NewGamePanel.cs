using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using System.IO;
using UnityEngine.UI;

public class NewGamePanel : FadePanel, IOpenCloseMenu
{
    [Inject]
    MainSceneUI mainUI;

    [Inject]
    LobbyData lobbyData;

    

    [SerializeField] TMP_Text[] saveText;

    [SerializeField] GameObject confirmPanel;
    [SerializeField] GameObject modifySeletedButton;

    [SerializeField] bool[] isSave;

    [SerializeField] int tempNum;

    private void Awake()
    {
        SetLoadText();
    }


    public void CloseUIPanel()
    {
        confirmPanel.gameObject.SetActive(false);
        FadeOutUI();
        mainUI.CurrentMenu = mainUI.MainMenuPanel;
        mainUI.CurrentMenu.OpenUIPanel();
        
    }

    public void OpenUIPanel()
    {
        gameObject.SetActive(true);
        FadeInUI();
    }

    public void OnModifyPanel(int num)
    {
        if (isSave[num])
        {
            confirmPanel.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(modifySeletedButton);
            tempNum = num;
        }
        else
        {
            NewSaveFile(num);
        }
    }

    public void NewSaveFile(int num)
    {
        lobbyData.saveNumber = num+1;
        lobbyData.NewSaveData();

        mainUI.ChangeScene();

    }

    public void CancelModify()
    {
        confirmPanel.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(selectButton.gameObject);
    }

    public void ConfirmModify()
    {
        confirmPanel.gameObject.SetActive(false);
        NewSaveFile(tempNum);
    }

    public void SetLoadText()
    {
#if UNITY_EDITOR
        string path = $"{Application.dataPath}/2.Private/KimSW/Json";
#else
            string path = Application.persistentDataPath; 
#endif

        for (int i = 0; i < saveText.Length; i++)
        {
            if (File.Exists($"{path}/Save{i + 1}.json") == false)
            {
                saveText[i].text = "Empty";
            }
            else
            {
                saveText[i].text = $"세이브{i + 1}";
                isSave[i] = true;
            }
        }



    }

}
