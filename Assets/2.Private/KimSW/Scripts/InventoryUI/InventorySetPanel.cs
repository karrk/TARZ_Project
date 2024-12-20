using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InventorySetPanel : MonoBehaviour, IOpenCloseMenu
{
    [Inject]
    InGameUI inGameUI;

    [SerializeField] AnimatedUI[] animatedUIs;
 

    public void OpenUIPanel()
    {
        inGameUI.PlayerStatusBarPanel.gameObject.SetActive(false);
        gameObject.SetActive(true);

        for (int i = 0; i < animatedUIs.Length; i++)
        {
            animatedUIs[i].MoveOnUI();
        }

        inGameUI.InventoryPanel.backpackPanel.SetSelectCursor();


    }

    public void CloseUIPanel()
    {
        for (int i = 0; i < animatedUIs.Length; i++)
        {
            animatedUIs[i].MoveOffUI();
        }
        inGameUI.CurrentMenu = inGameUI.PlayerStatusBarPanel;
        inGameUI.CurrentMenu.OpenUIPanel();
    }
}
