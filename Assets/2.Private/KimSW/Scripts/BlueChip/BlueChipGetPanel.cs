using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using Zenject.Asteroids;

public class BlueChipGetPanel : MonoBehaviour, IOpenCloseMenu
{
    [Inject]
    InGameUI inGameUI;

   
    [SerializeField] Image sprite;
    [SerializeField] TMP_Text bluechipName;
    [SerializeField] TMP_Text bluechipExplain;

    public void CloseUIPanel()
    {
        gameObject.SetActive(false);

        inGameUI.CurrentMenu = inGameUI.StatusBarPanel;
        inGameUI.CurrentMenu.OpenUIPanel();


    }

    public void OpenUIPanel()
    {


        gameObject.SetActive(true);
    }

    public void SetInfo(BlueChip chip){
        bluechipName.text = chip.blueChipName;
        bluechipExplain.text = chip.description;
}
}
